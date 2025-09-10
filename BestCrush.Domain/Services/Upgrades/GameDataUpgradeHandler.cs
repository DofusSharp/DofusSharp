using BestCrush.Domain.Models;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProgressMessage = BestCrush.Domain.Models.ProgressMessage;

namespace BestCrush.Domain.Services.Upgrades;

public class GameDataUpgradeHandler(
    BestCrushDbContext dbContext,
    IDofusDbQueryProvider dofusDbQueryProvider,
    IDofocusClientFactory dofocusClientFactory,
    ILogger<GameDataUpgradeHandler> logger
)
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

        progress?.Report("Mise à jour des données du jeu.");

        await ClearTables(progress?.DeriveSubtask(0, 25), cancellationToken);

        (Dictionary<long, DofusDbCharacteristic> characteristicsDict, Dictionary<long, DofusDbRecipe> recipesDict, DofusDbItem[] equipments, DofusDbItem[] ingredients) =
            await FetchDataAsync(progress?.DeriveSubtask(25, 50), cancellationToken);

        CreateIngredients(ingredients, progress?.DeriveSubtask(50, 60));
        await dbContext.SaveChangesAsync(cancellationToken);

        await CreateEquipmentsAsync(characteristicsDict, recipesDict, equipments, progress?.DeriveSubtask(60, 80), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        await CreateRunesAsync(characteristicsDict, progress?.DeriveSubtask(80, 100), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        Upgrade newUpgrade = new() { Kind = UpgradeKind.DofusDb, OldVersion = oldVersion?.ToString(), NewVersion = newVersion.ToString(), UpgradeDate = DateTime.Now };
        dbContext.Upgrades.Add(newUpgrade);
        await dbContext.SaveChangesAsync(cancellationToken);

        progress?.Report("Les données du jeu ont été mises à jour.", 100, true);

        logger.LogInformation("Successfully upgraded DofusDB data to version {NewVersion}.", newVersion);
    }

    async Task ClearTables(ProgressSync<ProgressMessage>? progress, CancellationToken cancellationToken)
    {
        progress?.ReportStep("Suppression des anciennes données", 1, 6);
        await ClearTableAsync<ItemCharacteristicLine>(cancellationToken);
        progress?.ReportStep("Suppression des anciennes données", 2, 6);
        await ClearTableAsync<RecipeEntry>(cancellationToken);
        progress?.ReportStep("Suppression des anciennes données", 3, 6);
        await ClearTableAsync<Resource>(cancellationToken);
        progress?.ReportStep("Suppression des anciennes données", 4, 6);
        await ClearTableAsync<Equipment>(cancellationToken);
        progress?.ReportStep("Suppression des anciennes données", 5, 6);
        await ClearTableAsync<Rune>(cancellationToken);
        progress?.ReportStep("Suppression des anciennes données", 6, 6);
    }

    async Task ClearTableAsync<T>(CancellationToken cancellationToken = default)
    {
        string? tableName = dbContext.Model.FindEntityType(typeof(T))?.GetTableName();
        if (tableName is null)
        {
            throw new InvalidOperationException($"Could not find table name for entity type {typeof(T).FullName}");
        }

#pragma warning disable EF1002
        await dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName};", cancellationToken);
#pragma warning restore EF1002
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    async Task<(Dictionary<long, DofusDbCharacteristic> characteristicsDict, Dictionary<long, DofusDbRecipe> recipesDict, DofusDbItem[] equipments, DofusDbItem[] ingredients)>
        FetchDataAsync(ProgressSync<ProgressMessage>? progress = null, CancellationToken cancellationToken = default)
    {
        IDofusDbQuery<DofusDbCharacteristic> characteristicsQuery = dofusDbQueryProvider.Characteristics();
        DofusDbCharacteristic[] characteristics = await characteristicsQuery
            .ExecuteAsync(progress?.DeriveSubtask(0, 20).ToStepProgress("Récupération des caractéristiques"), cancellationToken)
            .ToArrayAsync(cancellationToken);
        Dictionary<long, DofusDbCharacteristic> characteristicsDict = characteristics.Where(c => c.Id.HasValue).ToDictionary(c => c.Id!.Value, c => c);

        IDofusDbQuery<DofusDbRecipe> recipesQuery = dofusDbQueryProvider.Recipes();
        DofusDbRecipe[] recipes = await recipesQuery
            .ExecuteAsync(progress?.DeriveSubtask(20, 60).ToStepProgress("Récupération des recettes"), cancellationToken)
            .ToArrayAsync(cancellationToken);
        Dictionary<long, DofusDbRecipe> recipesDict = recipes.Where(r => r.ResultId.HasValue).ToDictionary(c => c.ResultId!.Value, c => c);

        // see EquipmentType.ToDofusDbItemTypeId
        long[] equipmentTypes = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18, 121, 311, 19, 20, 21, 22, 82, 114, 151, 271];
        DofusDbItem[] equipments = await dofusDbQueryProvider
            .Items()
            .Where(i => equipmentTypes.Contains(i.TypeId!.Value))
            .ExecuteAsync(progress?.DeriveSubtask(60, 100).ToStepProgress("Récupération des équipements"), cancellationToken)
            .ToArrayAsync(cancellationToken);

        DofusDbItem[] ingredients = equipments
            .Select(e => recipesDict.GetValueOrDefault(e.Id!.Value))
            .OfType<DofusDbRecipe>()
            .SelectMany(r => r.Ingredients ?? [])
            .DistinctBy(i => i.Id!.Value)
            .ToArray();
        return (characteristicsDict, recipesDict, equipments, ingredients);
    }

    void CreateIngredients(DofusDbItem[] ingredients, ProgressSync<ProgressMessage>? progress)
    {
        ProgressSync<ProgressMessage>? ingredientsProgress = progress?.DeriveSubtask(50, 70);
        for (int index = 0; index < ingredients.Length; index++)
        {
            ingredientsProgress?.ReportStep($"Création des ingrédients {index}/{ingredients.Length}", index, ingredients.Length);
            DofusDbItem ingredient = ingredients[index];
            Resource? resource = CreateResource(ingredient);
            if (resource is null)
            {
                logger.LogWarning("Could not map ingredient {Name} ({Id}).", ingredient.Name?.Fr ?? "???", ingredient.Id?.ToString() ?? "???");
                continue;
            }

            dbContext.Resources.Add(resource);
        }

        ingredientsProgress?.ReportStep($"Création des ingrédients {ingredients.Length}/{ingredients.Length}", ingredients.Length, ingredients.Length);
    }

    async Task CreateEquipmentsAsync(
        Dictionary<long, DofusDbCharacteristic> characteristicsDict,
        Dictionary<long, DofusDbRecipe> recipesDict,
        DofusDbItem[] equipments,
        ProgressSync<ProgressMessage>? progress = null,
        CancellationToken cancellationToken = default
    )
    {
        ProgressSync<ProgressMessage>? equipmentsProgress = progress?.DeriveSubtask(70, 100);
        for (int index = 0; index < equipments.Length; index++)
        {
            equipmentsProgress?.ReportStep($"Création des équipements {index}/{equipments.Length}", index, equipments.Length);

            DofusDbItem dofusDbItem = equipments[index];
            if (!dofusDbItem.Id.HasValue)
            {
                logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                continue;
            }

            Equipment? equipment = await CreateEquipmentAsync(dofusDbItem, characteristicsDict, recipesDict, cancellationToken);
            if (equipment is null)
            {
                logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                continue;
            }
            dbContext.Equipments.Add(equipment);
        }

        equipmentsProgress?.ReportStep($"Création des équipements {equipments.Length}/{equipments.Length}", equipments.Length, equipments.Length);
    }

    async Task<Equipment?> CreateEquipmentAsync(
        DofusDbItem dofusDbItem,
        Dictionary<long, DofusDbCharacteristic> characteristics,
        Dictionary<long, DofusDbRecipe> recipes,
        CancellationToken cancellationToken = default
    )
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Equipment equipment = new(dofusDbItem.Id.Value)
        {
            DofusDbIconId = dofusDbItem.IconId,
            Level = dofusDbItem.Level ?? 0,
            Name = dofusDbItem.Name?.Fr ?? "???",
            Type = EquipmentTypeExtensions.EquipmentTypeFromDofusDbTypeId(dofusDbItem.TypeId ?? 0) ?? EquipmentType.MagicWeapon
        };

        CreateCharacteristicLines(dofusDbItem, equipment, characteristics);
        await CreateRecipeAsync(dofusDbItem, equipment, recipes, cancellationToken);

        return equipment;
    }

    static void CreateCharacteristicLines(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbCharacteristic> characteristics)
    {
        if (dofusDbItem.Effects is null)
        {
            return;
        }

        Dictionary<Characteristic, DofusDbItemEffect> itemCharacteristics = dofusDbItem
            .Effects.Select(e => e.Characteristic.HasValue ? (Characteristic: characteristics.GetValueOrDefault(e.Characteristic.Value), Effect: e) : (null, e))
            .Where(x => x.Characteristic?.Keyword is not null)
            .Select((Characteristic? Characteristic, DofusDbItemEffect Effect) (x) =>
                        (Characteristic: CharacteristicExtensions.CharacteristicFromDofusDbKeyword(x.Characteristic!.Keyword!), x.Effect)
            )
            .Where(x => x.Characteristic is not null)
            .ToDictionary(x => x.Characteristic!.Value, x => x.Effect);

        foreach ((Characteristic characteristic, DofusDbItemEffect effect) in itemCharacteristics)
        {
            equipment.Characteristics.Add(new ItemCharacteristicLine(equipment, characteristic, effect.From ?? 0, effect.To is null or 0 ? effect.From ?? 0 : effect.To.Value));
        }
    }

    async Task CreateRecipeAsync(DofusDbItem dofusDbItem, Equipment equipment, Dictionary<long, DofusDbRecipe> recipes, CancellationToken cancellationToken = default)
    {
        if (dofusDbItem.HasRecipe != true || !recipes.TryGetValue(equipment.DofusDbId, out DofusDbRecipe? recipe) || recipe.Ingredients is null || recipe.Quantities is null)
        {
            return;
        }

        for (int index = 0; index < recipe.Ingredients.Count; index++)
        {
            DofusDbItem ingredient = recipe.Ingredients[index];
            int quantity = recipe.Quantities[index];

            Resource? resource = await dbContext.Resources.SingleOrDefaultAsync(r => r.DofusDbId == ingredient.Id!.Value, cancellationToken);
            if (resource is null)
            {
                continue;
            }

            equipment.Recipe.Add(new RecipeEntry(equipment, resource, quantity));
        }
    }

    static Resource? CreateResource(DofusDbItem dofusDbItem)
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Resource resource = new(dofusDbItem.Id.Value)
        {
            DofusDbIconId = dofusDbItem.IconId,
            Level = dofusDbItem.Level ?? 0,
            Name = dofusDbItem.Name?.Fr ?? "???"
        };

        return resource;
    }

    async Task CreateRunesAsync(
        Dictionary<long, DofusDbCharacteristic> characteristicsDict,
        ProgressSync<ProgressMessage>? progress = null,
        CancellationToken cancellationToken = default
    )
    {
        IDofocusRunesClient dofocusClient = dofocusClientFactory.Runes();
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
            updateProgress?.ReportStep($"Mise à jour des runes {index}/{dofocusRunes.Count}", index, dofocusRunes.Count);

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

            Rune? rune = CreateRune(dofusDbRune);
            if (rune is null)
            {
                continue;
            }
            dbContext.Runes.Add(rune);

            rune.Characteristic = characteristic.Value;
            index++;
        }
        updateProgress?.ReportStep($"Mise à jour des runes {dofocusRunes.Count}/{dofocusRunes.Count}", dofocusRunes.Count, dofocusRunes.Count);
    }


    static Rune? CreateRune(DofusDbItem dofusDbItem)
    {
        if (dofusDbItem.Id is null)
        {
            return null;
        }

        Rune rune = new(dofusDbItem.Id.Value)
        {
            DofusDbIconId = dofusDbItem.IconId,
            Level = dofusDbItem.Level ?? 0,
            Name = dofusDbItem.Name?.Fr ?? "???"
        };

        return rune;
    }
}
