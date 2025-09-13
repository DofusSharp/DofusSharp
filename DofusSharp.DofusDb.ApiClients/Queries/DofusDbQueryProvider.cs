using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;

namespace DofusSharp.DofusDb.ApiClients.Queries;

class DofusDbQueryProvider(IDofusDbClientsFactory factory) : IDofusDbQueryProvider
{
    public IDofusDbQuery<DofusDbServer> Servers() => new DofusDbQuery<DofusDbServer>(factory.Servers());

    public IDofusDbQuery<DofusDbCharacteristic> Characteristics() => new DofusDbQuery<DofusDbCharacteristic>(factory.Characteristics());

    public IDofusDbQuery<DofusDbItem> Items() => new DofusDbQuery<DofusDbItem>(factory.Items());
    public IDofusDbQuery<DofusDbItemType> ItemTypes() => new DofusDbQuery<DofusDbItemType>(factory.ItemTypes());
    public IDofusDbQuery<DofusDbItemSuperType> ItemSuperTypes() => new DofusDbQuery<DofusDbItemSuperType>(factory.ItemSuperTypes());
    public IDofusDbQuery<DofusDbItemSet> ItemSets() => new DofusDbQuery<DofusDbItemSet>(factory.ItemSets());

    public IDofusDbQuery<DofusDbJob> Jobs() => new DofusDbQuery<DofusDbJob>(factory.Jobs());
    public IDofusDbQuery<DofusDbRecipe> Recipes() => new DofusDbQuery<DofusDbRecipe>(factory.Recipes());
    public IDofusDbQuery<DofusDbSkill> Skills() => new DofusDbQuery<DofusDbSkill>(factory.Skills());

    public IDofusDbQuery<DofusDbSpell> Spells() => new DofusDbQuery<DofusDbSpell>(factory.Spells());
    public IDofusDbQuery<DofusDbSpellLevel> SpellLevels() => new DofusDbQuery<DofusDbSpellLevel>(factory.SpellLevels());
    public IDofusDbQuery<DofusDbSpellState> SpellStates() => new DofusDbQuery<DofusDbSpellState>(factory.SpellStates());
    public IDofusDbQuery<DofusDbSpellVariant> SpellVariants() => new DofusDbQuery<DofusDbSpellVariant>(factory.SpellVariants());

    public IDofusDbQuery<DofusDbMonster> Monsters() => new DofusDbQuery<DofusDbMonster>(factory.Monsters());
    public IDofusDbQuery<DofusDbMonsterRace> MonsterRaces() => new DofusDbQuery<DofusDbMonsterRace>(factory.MonsterRaces());
    public IDofusDbQuery<DofusDbMonsterSuperRace> MonsterSuperRaces() => new DofusDbQuery<DofusDbMonsterSuperRace>(factory.MonsterSuperRaces());

    public IDofusDbQuery<DofusDbWorld> Worlds() => new DofusDbQuery<DofusDbWorld>(factory.Worlds());
    public IDofusDbQuery<DofusDbSuperArea> SuperAreas() => new DofusDbQuery<DofusDbSuperArea>(factory.SuperAreas());
    public IDofusDbQuery<DofusDbArea> Areas() => new DofusDbQuery<DofusDbArea>(factory.Areas());
    public IDofusDbQuery<DofusDbSubArea> SubAreas() => new DofusDbQuery<DofusDbSubArea>(factory.SubAreas());
    public IDofusDbQuery<DofusDbMap> Maps() => new DofusDbQuery<DofusDbMap>(factory.Maps());
    public IDofusDbQuery<DofusDbMapPosition> MapPositions() => new DofusDbQuery<DofusDbMapPosition>(factory.MapPositions());
    public IDofusDbQuery<DofusDbDungeon> Dungeons() => new DofusDbQuery<DofusDbDungeon>(factory.Dungeons());

    public IDofusDbQuery<DofusDbTitle> Titles() => new DofusDbQuery<DofusDbTitle>(factory.Titles());
}
