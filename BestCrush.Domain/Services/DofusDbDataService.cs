using BestCrush.Domain.Models;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using Microsoft.Extensions.Logging;

namespace BestCrush.Domain.Services;

public class DofusDbDataService(BestCrushDbContext context, ILogger<DofusDbDataService> logger)
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

        DofusDbQuery<DofusDbCharacteristic> query = DofusDbQuery.Production(Constants.Referrer).Characteristics();
        DofusDbCharacteristic[] characteristics = await query.ExecuteAsync().ToArrayAsync();
        Dictionary<long, DofusDbCharacteristic> characteristicsDict = characteristics.Where(c => c.Id.HasValue).ToDictionary(c => c.Id!.Value, c => c);

        long[] equipmentTypes = Enum.GetValues<EquipmentType>().Select(t => t.ToDofusDbItemTypeId()).ToArray();
        DofusDbItem[] equipments = await DofusDbQuery.Production(Constants.Referrer).Items().Where(i => equipmentTypes.Contains(i.TypeId.Value)).ExecuteAsync().ToArrayAsync();
        foreach (DofusDbItem dofusDbItem in equipments)
        {
            Item? item = CreateItem(dofusDbItem, characteristicsDict);
            if (item is null)
            {
                logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                continue;
            }

            context.Items.Add(item);
        }

        CurrentVersion currentVersion = new(version.ToString());
        context.Add(currentVersion);

        logger.LogInformation("DofusDB data fetched.");
    }

    async Task UpgradeAsync(CurrentVersion currentVersionEntity, Version currentVersion, Version newVersion)
    {
        logger.LogInformation("Upgrading DofusDB data from version {CurrentVersion} to {NewVersion}...", currentVersion, newVersion);

        DofusDbQuery<DofusDbCharacteristic> query = DofusDbQuery.Production(Constants.Referrer).Characteristics();
        DofusDbCharacteristic[] characteristics = await query.ExecuteAsync().ToArrayAsync();
        Dictionary<long, DofusDbCharacteristic> characteristicsDict = characteristics.Where(c => c.Id.HasValue).ToDictionary(c => c.Id!.Value, c => c);

        long[] equipmentTypes = Enum.GetValues<EquipmentType>().Select(t => t.ToDofusDbItemTypeId()).ToArray();
        DofusDbItem[] equipments = await DofusDbQuery.Production(Constants.Referrer).Items().Where(i => equipmentTypes.Contains(i.TypeId.Value)).ExecuteAsync().ToArrayAsync();
        foreach (DofusDbItem dofusDbItem in equipments)
        {
            if (!dofusDbItem.Id.HasValue)
            {
                logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                continue;
            }

            Item? item = context.Items.SingleOrDefault(i => i.DofusDbId == dofusDbItem.Id.Value);
            if (item is null)
            {
                item = CreateItem(dofusDbItem, characteristicsDict);
                if (item is null)
                {
                    logger.LogWarning("Could not map equipment {Name} ({Id}).", dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???", dofusDbItem.Id?.ToString() ?? "???");
                    continue;
                }

                context.Items.Add(item);
            }
            else
            {
                UpdateItem(dofusDbItem, item, characteristicsDict);
            }
        }

        currentVersionEntity.DofusDbVersion = newVersion.ToString();

        logger.LogInformation("DofusDB data upgraded.");
    }

    static Item? CreateItem(DofusDbItem item, Dictionary<long, DofusDbCharacteristic> characteristics)
    {
        if (item.Id is null)
        {
            return null;
        }

        List<ItemCharacteristicLine> itemCharacteristics = new();
        if (item.Effects is not null)
        {
            foreach (DofusDbItemEffect effect in item.Effects)
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

                itemCharacteristics.Add(new ItemCharacteristicLine(characteristic.Value, effect.From ?? 0, effect.To is null or 0 ? effect.From ?? 0 : effect.To.Value));
            }
        }

        return new Item(item.Id.Value, item.Name?.En ?? item.Name?.Fr ?? "???", itemCharacteristics);
    }

    static void UpdateItem(DofusDbItem dofusDbItem, Item item, Dictionary<long, DofusDbCharacteristic> characteristics)
    {
        item.Name = dofusDbItem.Name?.En ?? dofusDbItem.Name?.Fr ?? "???";

        if (dofusDbItem.Effects is not null)
        {
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

                ItemCharacteristicLine? line = item.Characteristics.SingleOrDefault(c => c.Characteristic == characteristic);
                if (line is null)
                {
                    item.Characteristics.Add(new ItemCharacteristicLine(characteristic.Value, effect.From ?? 0, effect.To is null or 0 ? effect.From ?? 0 : effect.To.Value));
                }
                else
                {
                    line.From = effect.From ?? 0;
                    line.To = effect.To is null or 0 ? effect.From ?? 0 : effect.To.Value;
                }
            }
        }
    }
}
