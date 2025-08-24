using BestCrush.Domain.Models;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using Microsoft.Extensions.Logging;

namespace BestCrush.Domain.Services;

public class DofusDbDataService(
    BestCrushDbContext context,
    DofusDbQueryProvider dofusDbQueryProvider,
    DofusDbClientsFactory dofusDbClientsFactory,
    ILogger<DofusDbDataService> logger
)
{
    public async Task PrepareLocalDatabaseAsync()
    {
        CurrentVersion? currentVersionEntity = context.CurrentVersions.FirstOrDefault();
        logger.LogInformation("Local version: DofusDB={Version}", currentVersionEntity?.DofusDbVersion ?? "NULL");

        Version version = await DofusDbClient.Production().Version().GetVersionAsync();
        logger.LogInformation("Remote DofusDB version: {Version}", version);

        if (currentVersionEntity is not null && Version.TryParse(currentVersionEntity.DofusDbVersion, out Version? currentVersion))
        {
            if (currentVersion != version)
            {
                await UpgradeAsync(currentVersionEntity, currentVersion, version);
            }
        }
        else
        {
            await InitializeAsync(version);
        }

        await context.SaveChangesAsync();
    }

    async Task InitializeAsync(Version version)
    {
        logger.LogInformation("Fetching DofusDB data for version {NewVersion}...", version);

        await CreateOrUpdateEquipments();
        CurrentVersion currentVersion = new(version.ToString());
        context.Add(currentVersion);

        logger.LogInformation("DofusDB data fetched.");
    }

    async Task UpgradeAsync(CurrentVersion currentVersionEntity, Version currentVersion, Version newVersion)
    {
        logger.LogInformation("Upgrading DofusDB data from version {CurrentVersion} to {NewVersion}...", currentVersion, newVersion);

        await CreateOrUpdateEquipments();
        currentVersionEntity.DofusDbVersion = newVersion.ToString();

        logger.LogInformation("DofusDB data upgraded.");
    }

    async Task CreateOrUpdateEquipments()
    {
        DofusDbQuery<DofusDbCharacteristic> characteristicsQuery = dofusDbQueryProvider.Characteristics();
        DofusDbCharacteristic[] characteristics = await characteristicsQuery.ExecuteAsync().ToArrayAsync();
        Dictionary<long, DofusDbCharacteristic> characteristicsDict = characteristics.Where(c => c.Id.HasValue).ToDictionary(c => c.Id!.Value, c => c);

        DofusDbQuery<DofusDbRecipe> recipesQuery = dofusDbQueryProvider.Recipes();
        DofusDbRecipe[] recipes = await recipesQuery.ExecuteAsync().ToArrayAsync();
        Dictionary<long, DofusDbRecipe> recipesDict = recipes.Where(r => r.ResultId.HasValue).ToDictionary(c => c.ResultId!.Value, c => c);

        long[] equipmentTypes = Enum.GetValues<EquipmentType>().Select(t => t.ToDofusDbItemTypeId()).ToArray();
        DofusDbItem[] equipments = await dofusDbQueryProvider.Items().Where(i => equipmentTypes.Contains(i.TypeId.Value)).ExecuteAsync().ToArrayAsync();
        foreach (DofusDbItem dofusDbItem in equipments)
        {
            if (!dofusDbItem.Id.HasValue)
            {
                logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                continue;
            }

            Equipment? equipment = context.Equipments.OfType<Equipment>().SingleOrDefault(i => i.DofusDbId == dofusDbItem.Id.Value);
            if (equipment is null)
            {
                equipment = await CreateEquipmentAsync(dofusDbItem, characteristicsDict, recipesDict);
                if (equipment is null)
                {
                    logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                    continue;
                }

                context.Equipments.Add(equipment);
            }
            else
            {
                await UpdateEquipmentAsync(dofusDbItem, equipment, characteristicsDict, recipesDict);
            }
        }
    }

    async Task<Equipment?> CreateEquipmentAsync(DofusDbItem dofusDbItem, Dictionary<long, DofusDbCharacteristic> characteristics, Dictionary<long, DofusDbRecipe> recipes)
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Equipment equipment = new(dofusDbItem.Id.Value);
        await UpdateEquipmentAsync(dofusDbItem, equipment, characteristics, recipes);

        return equipment;
    }

    async Task UpdateEquipmentAsync(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbCharacteristic> characteristics, Dictionary<long, DofusDbRecipe> recipes)
    {
        equipment.DofusDbIconId = dofusDbItem.IconId;
        equipment.Level = dofusDbItem.Level ?? 0;
        equipment.Name = dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???";
        equipment.Type = EquipmentTypeExtensions.EquipmentTypeFromDofusDbTypeId(dofusDbItem.TypeId ?? 0) ?? EquipmentType.MagicWeapon;
        UpdateCharacteristics(dofusDbItem, equipment, characteristics);
        await UpdateRecipeAsync(dofusDbItem, equipment, recipes);
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

    async Task UpdateRecipeAsync(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbRecipe> recipes)
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
                    context.Remove(itemToRemove);
                }

                for (int index = 0; index < recipe.Ingredients.Count; index++)
                {
                    DofusDbItem ingredient = recipe.Ingredients[index];
                    int quantity = recipe.Quantities[index];

                    Resource? item = UpdateOrCreateItem(ingredient);
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

    Resource? UpdateOrCreateItem(DofusDbItem dofusDbItem)
    {
        Resource? item = context.Resources.SingleOrDefault(i => i.DofusDbId == dofusDbItem.Id);
        if (item is null)
        {
            return CreateItem(dofusDbItem);
        }

        UpdateResource(dofusDbItem, item);
        return item;
    }

    static Resource? CreateItem(DofusDbItem dofusDbItem)
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
        resource.Name = dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???";
    }
}
