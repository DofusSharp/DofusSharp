using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Spells;

namespace DofusSharp.DofusDb.ApiClients;

public class DofusDbQueryProvider(DofusDbClientsFactory factory)
{
    public DofusDbQuery<DofusDbCharacteristic> Characteristics() => new(factory.Characteristics());
    
    public DofusDbQuery<DofusDbItem> Items() => new(factory.Items());
    public DofusDbQuery<DofusDbItemType> ItemTypes() => new(factory.ItemTypes());
    public DofusDbQuery<DofusDbItemSuperType> ItemSuperTypes() => new(factory.ItemSuperTypes());
    public DofusDbQuery<DofusDbItemSet> ItemSets() => new(factory.ItemSets());

    public DofusDbQuery<DofusDbJob> Jobs() => new(factory.Jobs());
    public DofusDbQuery<DofusDbRecipe> Recipes() => new(factory.Recipes());
    public DofusDbQuery<DofusDbSkill> Skills() => new(factory.Skills());

    public DofusDbQuery<DofusDbSpell> Spells() => new(factory.Spells());
    public DofusDbQuery<DofusDbSpellLevel> SpellLevels() => new(factory.SpellLevels());
    public DofusDbQuery<DofusDbSpellState> SpellStates() => new(factory.SpellStates());
    public DofusDbQuery<DofusDbSpellVariant> SpellVariants() => new(factory.SpellVariants());

    public DofusDbQuery<DofusDbWorld> Worlds() => new(factory.Worlds());
    public DofusDbQuery<DofusDbSuperArea> SuperAreas() => new(factory.SuperAreas());
    public DofusDbQuery<DofusDbArea> Areas() => new(factory.Areas());
    public DofusDbQuery<DofusDbSubArea> SubAreas() => new(factory.SubAreas());
    public DofusDbQuery<DofusDbMap> Maps() => new(factory.Maps());
    public DofusDbQuery<DofusDbMapPosition> MapPositions() => new(factory.MapPositions());
    public DofusDbQuery<DofusDbDungeon> Dungeons() => new(factory.Dungeons());
}
