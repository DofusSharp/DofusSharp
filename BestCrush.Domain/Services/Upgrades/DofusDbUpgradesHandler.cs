using BestCrush.Domain.Models;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BestCrush.Domain.Services.Upgrades;

public class DofusDbUpgradesHandler(BestCrushDbContext dbContext, IDofusDbQueryProvider dofusDbQueryProvider, ILogger<DofusDbUpgradesHandler> logger)
{
    public async Task UpgradeAsync(Version newVersion, ProgressSync<ProgressMessage>? progress = null, CancellationToken cancellationToken = default)
    {
        Upgrade? lastUpgrade = await dbContext.Upgrades.Where(u => u.Kind == UpgradeKind.DofusDb).OrderByDescending(u => u.UpgradeDate).FirstOrDefaultAsync(cancellationToken);
        Version? oldVersion = Version.TryParse(lastUpgrade?.NewVersion, out Version? version) ? version : null;

        if (oldVersion == newVersion)
        {
            logger.LogInformation("DofusDB data is up to date. Version: {Version}.", newVersion);
            return;
        }

        logger.LogInformation("Running upgrade from version {OldVersion} to {NewVersion}...", oldVersion, newVersion);

        IDofusDbQuery<DofusDbCharacteristic> characteristicsQuery = dofusDbQueryProvider.Characteristics();
        DofusDbCharacteristic[] characteristics = await characteristicsQuery
            .ExecuteAsync(progress?.DeriveSubtask(0, 10).ToStepProgress("Récupération des caractéristiques"), cancellationToken)
            .ToArrayAsync(cancellationToken);
        Dictionary<long, DofusDbCharacteristic> characteristicsDict = characteristics.Where(c => c.Id.HasValue).ToDictionary(c => c.Id!.Value, c => c);

        await CreateOrUpdateEquipments(characteristicsDict, progress?.DeriveSubtask(10, 55), cancellationToken);
        await CreateOrUpdateRunesAsync(characteristicsDict, progress?.DeriveSubtask(55, 100), cancellationToken);

        Upgrade newUpgrade = new() { Kind = UpgradeKind.DofusDb, OldVersion = oldVersion?.ToString(), NewVersion = newVersion.ToString(), UpgradeDate = DateTime.Now };
        dbContext.Upgrades.Add(newUpgrade);

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully upgraded DofusDB data to version {NewVersion}.", newVersion);
    }

    async Task CreateOrUpdateEquipments(
        Dictionary<long, DofusDbCharacteristic> characteristicsDict,
        ProgressSync<ProgressMessage>? progress = null,
        CancellationToken cancellationToken = default
    )
    {
        IDofusDbQuery<DofusDbRecipe> recipesQuery = dofusDbQueryProvider.Recipes();
        DofusDbRecipe[] recipes = await recipesQuery
            .ExecuteAsync(progress?.DeriveSubtask(10, 30).ToStepProgress("Récupération des recettes"), cancellationToken)
            .ToArrayAsync(cancellationToken);
        Dictionary<long, DofusDbRecipe> recipesDict = recipes.Where(r => r.ResultId.HasValue).ToDictionary(c => c.ResultId!.Value, c => c);

        long[] equipmentTypes = Enum.GetValues<EquipmentType>().Select(t => t.ToDofusDbItemTypeId()).ToArray();
        DofusDbItem[] equipments = await dofusDbQueryProvider
            .Items()
            .Where(i => equipmentTypes.Contains(i.TypeId!.Value))
            .ExecuteAsync(progress?.DeriveSubtask(30, 50).ToStepProgress("Récupération des équipements"), cancellationToken)
            .ToArrayAsync(cancellationToken);

        ProgressSync<ProgressMessage>? updateProgress = progress?.DeriveSubtask(50, 100);
        for (int index = 0; index < equipments.Length; index++)
        {
            DofusDbItem dofusDbItem = equipments[index];
            if (!dofusDbItem.Id.HasValue)
            {
                logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                continue;
            }

            Equipment? equipment = dbContext.Equipments.OfType<Equipment>().SingleOrDefault(i => i.DofusDbId == dofusDbItem.Id.Value);
            if (equipment is null)
            {
                equipment = CreateEquipment(dofusDbItem, characteristicsDict, recipesDict);
                if (equipment is null)
                {
                    logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                    continue;
                }

                dbContext.Equipments.Add(equipment);
            }
            else
            {
                UpdateEquipment(dofusDbItem, equipment, characteristicsDict, recipesDict);
            }

            updateProgress?.ReportStep("Mise à jour des équipements", index, equipments.Length);
        }
        updateProgress?.ReportStep("Mise à jour des équipements", equipments.Length, equipments.Length);
    }

    Equipment? CreateEquipment(DofusDbItem dofusDbItem, Dictionary<long, DofusDbCharacteristic> characteristics, Dictionary<long, DofusDbRecipe> recipes)
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Equipment equipment = new(dofusDbItem.Id.Value);
        UpdateEquipment(dofusDbItem, equipment, characteristics, recipes);

        return equipment;
    }

    void UpdateEquipment(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbCharacteristic> characteristics, Dictionary<long, DofusDbRecipe> recipes)
    {
        equipment.DofusDbIconId = dofusDbItem.IconId;
        equipment.Level = dofusDbItem.Level ?? 0;
        equipment.Name = dofusDbItem.Name?.Fr ?? "???";
        equipment.Type = EquipmentTypeExtensions.EquipmentTypeFromDofusDbTypeId(dofusDbItem.TypeId ?? 0) ?? EquipmentType.MagicWeapon;
        UpdateCharacteristics(dofusDbItem, equipment, characteristics);
        UpdateRecipe(dofusDbItem, equipment, recipes);
    }

    static void UpdateCharacteristics(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbCharacteristic> characteristics)
    {
        if (dofusDbItem.Effects is null)
        {
            return;
        }

        foreach (DofusDbItemEffect effect in dofusDbItem.Effects)
        {
            if (!effect.Characteristic.HasValue)
            {
                continue;
            }

            DofusDbCharacteristic? dofusDbCharacteristic = characteristics.GetValueOrDefault(effect.Characteristic.Value);
            if (dofusDbCharacteristic?.Keyword is null)
            {
                continue;
            }

            Characteristic? characteristic = CharacteristicExtensions.CharacteristicFromDofusDbKeyword(dofusDbCharacteristic.Keyword);
            if (!characteristic.HasValue)
            {
                continue;
            }

            ItemCharacteristicLine? line = equipment.Characteristics.SingleOrDefault(c => c.Characteristic == characteristic);
            if (line is null)
            {
                equipment.Characteristics.Add(new ItemCharacteristicLine(characteristic.Value, effect.From ?? 0, effect.To is null or 0 ? effect.From ?? 0 : effect.To.Value));
            }
            else
            {
                line.From = effect.From ?? 0;
                line.To = effect.To is null or 0 ? effect.From ?? 0 : effect.To.Value;
            }
        }
    }

    void UpdateRecipe(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbRecipe> recipes)
    {
        if (dofusDbItem.HasRecipe != true)
        {
            return;
        }

        try
        {
            if (!recipes.TryGetValue(equipment.DofusDbId, out DofusDbRecipe? recipe))
            {
                return;
            }

            if (recipe.Ingredients is not null && recipe.Quantities is not null)
            {
                List<RecipeEntry> itemsToRemoveFromRecipe = equipment.Recipe.Where(recipeItem => recipe.Ingredients.All(i => recipeItem.Resource.DofusDbId != i.Id)).ToList();
                foreach (RecipeEntry itemToRemove in itemsToRemoveFromRecipe)
                {
                    dbContext.Remove(itemToRemove);
                }

                for (int index = 0; index < recipe.Ingredients.Count; index++)
                {
                    DofusDbItem ingredient = recipe.Ingredients[index];
                    int quantity = recipe.Quantities[index];

                    Resource? item = UpdateOrCreateResource(ingredient);
                    if (item is null)
                    {
                        continue;
                    }

                    RecipeEntry? recipeEntry = equipment.Recipe.SingleOrDefault(r => r.Resource == item);
                    if (recipeEntry is null)
                    {
                        equipment.Recipe.Add(new RecipeEntry(item, quantity));
                    }
                    else
                    {
                        recipeEntry.Count = quantity;
                    }
                }
            }
        }
        catch (Exception exn)
        {
            logger.LogError(exn, "An error occured while loading recipe of item {Name} ({Id}).", equipment.Name, equipment.DofusDbId);
        }
    }

    Resource? UpdateOrCreateResource(DofusDbItem dofusDbItem)
    {
        Resource? item = dbContext.Resources.SingleOrDefault(i => i.DofusDbId == dofusDbItem.Id);
        if (item is null)
        {
            return CreateResource(dofusDbItem);
        }

        UpdateResource(dofusDbItem, item);
        return item;
    }

    static Resource? CreateResource(DofusDbItem dofusDbItem)
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Resource resource = new(dofusDbItem.Id.Value);
        UpdateResource(dofusDbItem, resource);

        return resource;
    }

    static void UpdateResource(DofusDbItem dofusDbItem, Resource resource)
    {
        resource.DofusDbIconId = dofusDbItem.IconId;
        resource.Level = dofusDbItem.Level ?? 0;
        resource.Name = dofusDbItem.Name?.Fr ?? "???";
    }

    async Task CreateOrUpdateRunesAsync(
        Dictionary<long, DofusDbCharacteristic> characteristicsDict,
        ProgressSync<ProgressMessage>? progress = null,
        CancellationToken cancellationToken = default
    )
    {
        DofocusRunesClient dofocusClient = DofocusClient.Runes();
        IReadOnlyCollection<DofocusRune> dofocusRunes = await dofocusClient.GetRunesAsync(cancellationToken);

        Dictionary<long, DofusDbItem> dofusDbRunes = await dofusDbQueryProvider
            .Items()
            .Where(i => i.TypeId == 78)
            .ExecuteAsync(progress?.DeriveSubtask(0, 50).ToStepProgress("Récupération des runes"), cancellationToken)
            .Where(i => i.Id.HasValue)
            .ToDictionaryAsync(i => i.Id!.Value, i => i, cancellationToken: cancellationToken);

        ProgressSync<ProgressMessage>? updateProgress = progress?.DeriveSubtask(50, 100);
        int index = 0;
        foreach (DofocusRune dofocusRune in dofocusRunes)
        {
            if (dofocusRune.CharacteristicId is null)
            {
                continue;
            }

            DofusDbItem? dofusDbRune = dofusDbRunes.GetValueOrDefault(dofocusRune.Id);
            if (dofusDbRune is null)
            {
                continue;
            }

            DofusDbCharacteristic? dofusDbCharacteristic = characteristicsDict.GetValueOrDefault(dofocusRune.CharacteristicId.Value);
            if (dofusDbCharacteristic is null)
            {
                continue;
            }

            Characteristic? characteristic = CharacteristicExtensions.CharacteristicFromDofusDbKeyword(dofusDbCharacteristic.Keyword ?? "");
            if (characteristic is null)
            {
                continue;
            }

            Rune? rune = dbContext.Runes.SingleOrDefault(r => r.DofusDbId == dofocusRune.Id);
            if (rune is null)
            {
                rune = CreateRune(dofusDbRune);
                if (rune is null)
                {
                    continue;
                }

                dbContext.Runes.Add(rune);
            }
            else
            {
                UpdateRune(dofusDbRune, rune);
            }

            rune.Characteristic = characteristic.Value;

            updateProgress?.ReportStep("Mise à jour des runes", index, dofocusRunes.Count);
            index++;
        }
        updateProgress?.ReportStep("Mise à jour des runes", dofocusRunes.Count, dofocusRunes.Count);
    }

    static Rune? CreateRune(DofusDbItem dofusDbItem)
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Rune resource = new(dofusDbItem.Id.Value);
        UpdateRune(dofusDbItem, resource);

        return resource;
    }

    static void UpdateRune(DofusDbItem dofusDbItem, Rune rune)
    {
        rune.DofusDbIconId = dofusDbItem.IconId;
        rune.Level = dofusDbItem.Level ?? 0;
        rune.Name = dofusDbItem.Name?.Fr ?? "???";
    }
}
