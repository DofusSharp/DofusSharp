// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Runtime.CompilerServices;
using dofusdb.Commands;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Achievements;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Models.Maps;
using DofusSharp.DofusDb.ApiClients.Models.Monsters;
using DofusSharp.DofusDb.ApiClients.Models.Mounts;
using DofusSharp.DofusDb.ApiClients.Models.Npcs;
using DofusSharp.DofusDb.ApiClients.Models.Ornaments;
using DofusSharp.DofusDb.ApiClients.Models.Servers;
using DofusSharp.DofusDb.ApiClients.Models.Spells;
using DofusSharp.DofusDb.ApiClients.Models.Titles;
using Spectre.Console;

AnsiConsole.Console = AnsiConsole.Create(new AnsiConsoleSettings { Out = new AnsiConsoleOutput(Console.Error) });

CancellationTokenSource cts = new();
Console.CancelKeyPress += (_, eventArgs) =>
{
    cts.Cancel();
    eventArgs.Cancel = true;
    AnsiConsole.MarkupLine("[dim]Received INT signal, stopping...[/]");
};

Uri referrer = new("https://github.com/DofusSharp/DofusSharp/tree/main/dofusdb");
#if DEBUG
Uri defaultUrl = DofusDbClient.BetaUri;
#else
Uri defaultUrl = DofusDbClient.ProductionUri;
#endif

RootCommand rootCommand = new(
    """
    A command-line interface for querying the DofusDB API.
    Each subcommand targets a different resource type. Use the help option on a subcommand to see the available operations for that resource.
    Want to learn more? Visit us on GitHub: https://github.com/DofusSharp/DofusSharp/tree/main/dofusdb.
    """
)
{
    Options = { CommonOptions.QuietOption, CommonOptions.DebugOption },
    Subcommands =
    {
        // @formatter:max_line_length 9999
        new GameVersionCommand(uri => GetFactory(uri).Version(), defaultUrl).CreateCommand(),
        new GameCriterionCommand(uri => GetFactory(uri).Criterion(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbAchievement>("achievements", "Achievements", uri => GetFactory(uri).Achievements(), defaultUrl).CreateCommand(),
        new ImageClientCommand<long>("achievement-images", "Achievements images", uri => GetFactory(uri).AchievementImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbAchievementCategory>("achievement-categories", "Achievement Categories", uri => GetFactory(uri).AchievementCategories(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbAchievementObjective>("achievement-objectives", "Achievement Objectives", uri => GetFactory(uri).AchievementObjectives(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbServer>("servers", "Servers", uri => GetFactory(uri).Servers(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbCharacteristic>("characteristics", "Characteristics", uri => GetFactory(uri).Characteristics(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbItem>("items", "Items", uri => GetFactory(uri).Items(), defaultUrl).CreateCommand(),
        new ImageClientCommand<long>("item-images", "Item images", uri => GetFactory(uri).ItemImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbItemType>("item-types", "Item Types", uri => GetFactory(uri).ItemTypes(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbItemSuperType>("item-super-types", "Item Super Types", uri => GetFactory(uri).ItemSuperTypes(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbItemSet>("item-sets", "Item Sets", uri => GetFactory(uri).ItemSets(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbJob>("jobs", "Jobs", uri => GetFactory(uri).Jobs(), defaultUrl).CreateCommand(),
        new ImageClientCommand<long>("job-images", "Job images", uri => GetFactory(uri).JobImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbRecipe>("recipes", "Recipes", uri => GetFactory(uri).Recipes(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSkill>("skills", "Skills", uri => GetFactory(uri).Skills(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSpell>("spells", "Spells", uri => GetFactory(uri).Spells(), defaultUrl).CreateCommand(),
        new ImageClientCommand<long>("spell-images", "Spell images", uri => GetFactory(uri).SpellImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSpellLevel>("spell-levels", "Spell Levels", uri => GetFactory(uri).SpellLevels(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSpellState>("spell-states", "Spell States", uri => GetFactory(uri).SpellStates(), defaultUrl).CreateCommand(),
        new ImageClientCommand<string>("spell-state-images", "Spell State images", uri => GetFactory(uri).SpellStateImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSpellVariant>("spell-variants", "Spell Variants", uri => GetFactory(uri).SpellVariants(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMonster>("monsters", "Monsters", uri => GetFactory(uri).Monsters(), defaultUrl).CreateCommand(),
        new ImageClientCommand<long>("monster-images", "Monster images", uri => GetFactory(uri).MonsterImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMonsterRace>("monster-races", "Monster Races", uri => GetFactory(uri).MonsterRaces(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMonsterSuperRace>("monster-super-races", "Monster Super Races", uri => GetFactory(uri).MonsterSuperRaces(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMount>("mounts", "Mounts", uri => GetFactory(uri).Mounts(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMountFamily>("mount-families", "Mount Families", uri => GetFactory(uri).MountFamilies(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMountBehavior>("mount-behaviors", "MountBehaviors", uri => GetFactory(uri).MountBehaviors(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbNpc>("npcs", "Npcs", uri => GetFactory(uri).Npcs(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbNpcMessage>("npc-messages", "NpcMessages", uri => GetFactory(uri).NpcMessages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbWorld>("worlds", "Worlds", uri => GetFactory(uri).Worlds(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSuperArea>("super-areas", "Super Areas", uri => GetFactory(uri).SuperAreas(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbArea>("areas", "Areas", uri => GetFactory(uri).Areas(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbSubArea>("sub-areas", "Sub Areas", uri => GetFactory(uri).SubAreas(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMap>("maps", "Maps", uri => GetFactory(uri).Maps(), defaultUrl).CreateCommand(),
        new ScalableImageClientCommand<long>("map-images", "Map images", uri => GetFactory(uri).MapImages(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbMapPosition>("map-positions", "Map Positions", uri => GetFactory(uri).MapPositions(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbDungeon>("dungeons", "Dungeons", uri => GetFactory(uri).Dungeons(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbTitle>("titles", "Titles", uri => GetFactory(uri).Titles(), defaultUrl).CreateCommand(),
        new TableClientCommand<DofusDbOrnament>("ornaments", "Ornaments", uri => GetFactory(uri).Ornaments(), defaultUrl).CreateCommand(),
        new ImageClientCommand<long>("ornament-images", "Ornaments images", uri => GetFactory(uri).OrnamentImages(), defaultUrl).CreateCommand()
        // @formatter:max_line_length restore
    }
};

if (Environment.GetEnvironmentVariable("COLUMNS") is { } columnsStr && int.TryParse(columnsStr, out int columns) && columns > 0)
{
    if (rootCommand.Options.OfType<HelpOption>().FirstOrDefault() is { Action: HelpAction helpAction })
    {
        helpAction.MaxWidth = columns;
    }
}

ParseResult parseResult = rootCommand.Parse(args);

// ------ Parse error
if (parseResult.Action is ParseErrorAction parseErrorAction)
{
    parseErrorAction.ShowHelp = true;
    parseErrorAction.ShowTypoCorrections = true;
    parseErrorAction.Invoke(parseResult);
    return 1;
}

// ------ Other errors
if (parseResult.Errors.Count != 0)
{
    foreach (ParseError parseError in parseResult.Errors)
    {
        AnsiConsole.MarkupLine(parseError.Message);
    }
    AnsiConsole.WriteLine();

    await rootCommand.Parse("--help").InvokeAsync(cancellationToken: cts.Token);

    return 1;
}

// ------ Command found
bool debug = parseResult.CommandResult.GetValue(CommonOptions.DebugOption);

try
{
    return await parseResult.InvokeAsync(new InvocationConfiguration { EnableDefaultExceptionHandler = false }, cts.Token);
}
catch (TaskCanceledException exn)
{
    AnsiConsole.MarkupLine("[red]Operation canceled by user.[/]");
    if (debug)
    {
        AnsiConsole.WriteLine("Details:");
        WriteException(exn);
    }
    return 2;
}
catch (Exception exn)
{
    if (debug)
    {
        AnsiConsole.MarkupLine("[red]An unexpected error occurred, please open an issue at https://github.com/DofusSharp/DofusSharp/issues/new?template=bug_report.md.[/]");
        AnsiConsole.WriteLine("Details:");
        WriteException(exn);
    }
    else
    {
        AnsiConsole.MarkupLine("[red]An unexpected error occurred, use --debug for more details.[/]");
    }

    return 3;
}

IDofusDbClientsFactory GetFactory(Uri uri)
{
    return DofusDbClient.Create(uri, referrer);
}

void WriteException(Exception exn)
{
    if (RuntimeFeature.IsDynamicCodeSupported)
    {
        AnsiConsole.WriteException(exn, ExceptionFormats.ShortenEverything);
    }
    else
    {
        AnsiConsole.WriteLine(exn.ToString());
    }
}
