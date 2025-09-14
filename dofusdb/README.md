# [DofusDB](https://dofusdb.fr) CLI Tool

Fetch data from the DofusDB API.

## Overview

`dofusdb` is a command line interface (CLI) tool that exposes the API clients from the [DofusSharp.DofusDb.ApiClients](../DofusSharp.DofusDb.ApiClients) library. It allows users to interact with DofusDB resources and perform queries directly from the terminal.

## Features

- Query DofusDB items, recipes, runes, and other resources
- Fetch item stats and details
- Search and filter items by criteria

## Installation

### Using dotnet tool (recommended)

Download [.NET 10](https://get.dot.net/10), then install `dofusdb`:

```shell
dotnet tool install --global dofusdb
```

### Using binaries

Download the latest release from the [GitHub releases page](https://github.com/DofusSharp/DofusSharp/releases/latest).

## Suggestions

Completions for the `dofusdb` CLI commands are powered by the underlying implementation using [System.CommandLine](https://github.com/dotnet/command-line-api). This enables intelligent tab completion for commands, options, and arguments in supported shells.

To enable tab completion for your shell, follow the official Microsoft documentation:
[How to enable tab completion for .NET CLI tools](https://learn.microsoft.com/en-us/dotnet/standard/commandline/how-to-enable-tab-completion).

This will help you discover available commands and options more efficiently when using the CLI.

## Usage

```
Description:
  A command-line interface for querying the DofusDB API.
  Each subcommand targets a different resource type. Use the help option on a subcommand to see the available operations for that resource.
  Want to learn more? Visit us on GitHub: https://github.com/DofusSharp/DofusSharp/tree/main/dofusdb.

Utilisation :
  dofusdb [command] [options]

Options :
  -q, --quiet     Display less output
  -d, --debug     Show debugging output
  -?, -h, --help  Show help and usage information
  --version       Afficher les informations de version

Commandes :
  game-version            Get the version of the game corresponding to the data
  criterion <criterion>   Parse a criterion string into a JSON array with more information
  achievements            Achievements client
  achievement-images      Achievements images client
  achievement-categories  Achievement Categories client
  achievement-objectives  Achievement Objectives client
  servers                 Servers client
  characteristics         Characteristics client
  items                   Items client
  item-images             Item images client
  item-types              Item Types client
  item-super-types        Item Super Types client
  item-sets               Item Sets client
  jobs                    Jobs client
  job-images              Job images client
  recipes                 Recipes client
  skills                  Skills client
  spells                  Spells client
  spell-images            Spell images client
  spell-levels            Spell Levels client
  spell-states            Spell States client
  spell-state-images      Spell State images client
  spell-variants          Spell Variants client
  monsters                Monsters client
  monster-images          Monster images client
  monster-races           Monster Races client
  monster-super-races     Monster Super Races client
  mounts                  Mounts client
  mount-families          Mount Families client
  mount-behaviors         MountBehaviors client
  worlds                  Worlds client
  super-areas             Super Areas client
  areas                   Areas client
  sub-areas               Sub Areas client
  maps                    Maps client
  map-images              Map images client
  map-positions           Map Positions client
  dungeons                Dungeons client
  titles                  Titles client
  ornaments               Ornaments client
  ornament-images         Ornaments images client
```

### Game version

```
Description:
  Get the version of the game corresponding to the data

Utilisation :
  dofusdb game-version [options]

Options :
  --base <base>   Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help  Show help and usage information
  -q, --quiet     Display less output
  -d, --debug     Show debugging output
```

### Table data

```
Description:
  Items client

Utilisation :
  dofusdb items [command] [options]

Options :
  -?, -h, --help  Show help and usage information
  -q, --quiet     Display less output
  -d, --debug     Show debugging output

Commandes :
  list         List all items
  get <id>     Get items by id
  build-query  Build the search query for items
  count        Count items
```

#### `list`

```
Description:
  List all items

Utilisation :
  dofusdb items list [options]

Options :
  -a, --all              Fetch all available results, ignoring --limit. The --skip option is still honored when this option is set
  --limit <limit>        Maximum number of results to retrieve. If the value exceeds the API’s maximum page size, multiple requests will be performed [default: 10]
  --skip <skip>          Number of results to skip [default: 0]
  --select <select>      Comma separated list of fields to include in the results. If not specified, all fields are included [example: --select "id,name.fr,level"]
  --sort <sort>          Comma separated list of fields to sorts the results by. Prefix with '-' for descending order [example: --sort "-level,name.fr"]
  --filter <filter>      Comma separated list of predicates to filter the results by. Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values 
                         [example: --filter "level>=10,name.fr=Razielle|Goultard"]
  -o, --output <output>  File to write the JSON output to. If not specified, the output will be written to stdout
  --pretty-print         Pretty print the JSON output
  --base <base>          Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help         Show help and usage information
  -q, --quiet            Display less output
  -d, --debug            Show debugging output
```

##### Pagination

Results can be paginated using the `--limit` and `--skip` options.

```shell
    dofusdb items list --limit 50 --skip 100
```

##### Column selection

Results can be limited to specific fields using the `--select` option.

```shell
    dofusdb items list --select id,name,level
```

##### Sorting

Results can be sorted by one or more fields using the `--sort` option. Prefix a field with `-` for descending order.

```shell
    dofusdb items list --sort level,-name
```

##### Filtering

Results can be filtered using the `--filter` option. Each predicate is made of the name of the field, an operator (`=`, `!=`, `<`, `<=`, `>`, `>=`) and the value.

```shell
    dofusdb items list --filter "level>=10,name=Excalibur"
```

Additionally, equality and inequality operators accept multiple values separated by `|`, which transform them into IN and Not IN operators.

#### `get`

Fetch a single resource by its unique identifier.

```
Description:
  Get items by id

Utilisation :
  dofusdb items get <id> [options]

Arguments :
  <id>  Unique identifier of the resource

Options :
  -o, --output <output>  File to write the JSON output to. If not specified, the output will be written to stdout
  --pretty-print         Pretty print the JSON output
  --base <base>          Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help         Show help and usage information
  -q, --quiet            Display less output
  -d, --debug            Show debugging output
```

#### `count`

Get the total count of resources that would be returned by the `list` command with the same filters applied.

```
Description:
  Count items

Utilisation :
  dofusdb items count [options]

Options :
  --filter <filter>  Comma separated list of predicates to filter the results by. Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values 
                     [example: --filter "level>=10,name.fr=Razielle|Goultard"]
  --base <base>      Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help     Show help and usage information
  -q, --quiet        Display less output
  -d, --debug        Show debugging output
```

#### `build-query`

Build the search query URL for the specified parameters without executing the request.

```
Description:
  Build the search query for items

Utilisation :
  dofusdb items build-query [options]

Options :
  --limit <limit>    Maximum number of results to retrieve. If the value exceeds the API’s maximum page size, multiple requests will be performed [default: 10]
  --skip <skip>      Number of results to skip [default: 0]
  --select <select>  Comma separated list of fields to include in the results. If not specified, all fields are included [example: --select "id,name.fr,level"]
  --sort <sort>      Comma separated list of fields to sorts the results by. Prefix with '-' for descending order [example: --sort "-level,name.fr"]
  --filter <filter>  Comma separated list of predicates to filter the results by. Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values 
                     [example: --filter "level>=10,name.fr=Razielle|Goultard"]
  --base <base>      Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help     Show help and usage information
  -q, --quiet        Display less output
  -d, --debug        Show debugging output
```

### Image data

```
Description:
  Item images client

Utilisation :
  dofusdb item-images [command] [options]

Options :
  -?, -h, --help  Show help and usage information
  -q, --quiet     Display less output
  -d, --debug     Show debugging output

Commandes :
  get <id>  Get item images by id
```

#### `get`

```
Description:
  Get item images by id

Utilisation :
  dofusdb item-images get <id> [options]

Arguments :
  <id>  Unique identifier of the resource

Options :
  -o, --output <output>  File to write the JSON output to. If not specified, the output will be written to the console
  --base <base>          Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help         Show help and usage information
  -q, --quiet            Display less output
  -d, --debug            Show debugging output
```

### Scalable image data

```
Description:
  Map images client

Utilisation :
  dofusdb map-images [command] [options]

Options :
  -?, -h, --help  Show help and usage information
  -q, --quiet     Display less output
  -d, --debug     Show debugging output

Commandes :
  get <id>  Get map images by id
```

### `get`

```
Description:
  Get map images by id

Utilisation :
  dofusdb map-images get <id> [options]

Arguments :
  <id>  Unique identifier of the resource

Options :
  --scale <Full|Half|Quarter|ThreeQuarters>  Scale of the image to fetch [default: Full]
  -o, --output <output>                      File to write the JSON output to. If not specified, the output will be written to the console
  --base <base>                              Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help                             Show help and usage information
  -q, --quiet                                Display less output
  -d, --debug                                Show debugging output
```

### Criterion

```
Description:
  Parse a criterion string into a JSON array with more information

Utilisation :
  dofusdb criterion <criterion> [options]

Arguments :
  <criterion>  Criterion to parse

Options :
  --lang <En|Fr>         Language to request
  -o, --output <output>  File to write the JSON output to. If not specified, the output will be written to stdout
  --pretty-print         Pretty print the JSON output
  --base <base>          Base URL to use when building the query URL [default: https://api.beta.dofusdb.fr/]
  -?, -h, --help         Show help and usage information
  -q, --quiet            Display less output
  -d, --debug            Show debugging output
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests for bug fixes, features, or documentation improvements.

## License

This project is licensed under the terms of the [MIT License](../LICENSE.md).

