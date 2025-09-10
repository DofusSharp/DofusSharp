// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.CommandLine.Parsing;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.Cli.Commands;

Uri referrer = new("https://github.com/ismailbennani/DofusSharp/tree/main/DofusSharp.DofusDb.Cli");
#if DEBUG
IDofusDbClientsFactory factory = DofusDbClient.Beta(referrer);
#else
IDofusDbClientsFactory factory = DofusDbClient.Production(referrer);
#endif

RootCommand rootCommand = new("A command line interface for DofusDB.")
{
    new TableClientCommand<DofusDbServer>("servers", "Servers", factory.Servers()).CreateCommand(),
    new TableClientCommand<DofusDbCharacteristic>("characteristics", "Characteristics", factory.Characteristics()).CreateCommand(),
    new TableClientCommand<DofusDbItem>("items", "Items", factory.Items()).CreateCommand(),
    new TableClientCommand<DofusDbItemType>("item-types", "Item Types", factory.ItemTypes()).CreateCommand(),
    new TableClientCommand<DofusDbItemSuperType>("item-super-types", "Item Super Types", factory.ItemSuperTypes()).CreateCommand(),
    new TableClientCommand<DofusDbItemSet>("item-sets", "Item Sets", factory.ItemSets()).CreateCommand(),
    new TableClientCommand<DofusDbJob>("jobs", "Jobs", factory.Jobs()).CreateCommand(),
    new TableClientCommand<DofusDbRecipe>("recipes", "Recipes", factory.Recipes()).CreateCommand(),
    new TableClientCommand<DofusDbSkill>("skills", "Skills", factory.Skills()).CreateCommand(),
    new TableClientCommand<DofusDbSpell>("spells", "Spells", factory.Spells()).CreateCommand(),
    new TableClientCommand<DofusDbSpellLevel>("spell-levels", "Spell Levels", factory.SpellLevels()).CreateCommand(),
    new TableClientCommand<DofusDbSpellState>("spell-states", "Spell States", factory.SpellStates()).CreateCommand(),
    new TableClientCommand<DofusDbSpellVariant>("spell-variants", "Spell Variants", factory.SpellVariants()).CreateCommand(),
    new TableClientCommand<DofusDbMonster>("monsters", "Monsters", factory.Monsters()).CreateCommand(),
    new TableClientCommand<DofusDbMonsterRace>("monster-races", "Monster Races", factory.MonsterRaces()).CreateCommand(),
    new TableClientCommand<DofusDbMonsterSuperRace>("monster-super-races", "Monster Super Races", factory.MonsterSuperRaces()).CreateCommand(),
    new TableClientCommand<DofusDbWorld>("worlds", "Worlds", factory.Worlds()).CreateCommand(),
    new TableClientCommand<DofusDbSuperArea>("super-areas", "Super Areas", factory.SuperAreas()).CreateCommand(),
    new TableClientCommand<DofusDbArea>("areas", "Areas", factory.Areas()).CreateCommand(),
    new TableClientCommand<DofusDbSubArea>("sub-areas", "Sub Areas", factory.SubAreas()).CreateCommand(),
    new TableClientCommand<DofusDbMap>("maps", "Maps", factory.Maps()).CreateCommand(),
    new TableClientCommand<DofusDbMapPosition>("map-positions", "Map Positions", factory.MapPositions()).CreateCommand(),
    new TableClientCommand<DofusDbDungeon>("dungeons", "Dungeons", factory.Dungeons()).CreateCommand()
};

ParseResult parseResult = rootCommand.Parse(args);

if (parseResult.Errors.Count != 0)
{
    foreach (ParseError parseError in parseResult.Errors)
    {
        Console.Error.WriteLine(parseError.Message);
    }
    return 1;
}

return await parseResult.InvokeAsync();
