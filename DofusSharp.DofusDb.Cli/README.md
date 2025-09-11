# [DofusDB](https://dofusdb.fr) CLI Tool

## Overview

Command line interface (CLI) tool that exposes the API clients from the [DofusSharp.DofusDb.ApiClients](../DofusSharp.DofusDb.ApiClients) library. It allows users to interact with DofusDB resources and perform queries directly from the terminal.

## Features

- Query DofusDB items, recipes, runes, and other resources
- Fetch item stats and details
- Search and filter items by criteria

## Installation

### Using dotnet tool (recommended)
```shell
dotnet tool install --global dofusdb
```

### Using binaries

Download the latest release from the [GitHub releases page](https://github.com/DofusSharp/DofusSharp/releases/latest).

## Usage

There are different types of commands for different types of resources.

### Game version

```
Description:
  Get the version of the game corresponding to the data.

Utilisation :
  dofusdb game-version [options]

Options :
  -?, -h, --help  Show help and usage information
```

### Table data

Resources endpoints that query tables of data (such as items, recipes, runes, etc.) have the following commands.

#### `list`

```
Description:
  List all available items.

Utilisation :
  dofusdb items list [options]

Options :
  --limit <limit>        Number of results to get. This might lead to multiple requests if the limit exceeds the API's maximum page size.
  --skip <skip>          Number of results to skip.
  --select <select>      Comma separated list of fields to include in the results. If not specified, all fields are included.
  --sort <sort>          Comma separated list of fields to sorts the results by. Prefix with '-' for descending order.
  --filter <filter>      Comma separated list of predicates to filter the results by. Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values. 
                         Example: --filter "level>=10,name=Excalibur"
  -o, --output <output>  File to write the JSON output to. If not specified, the output will be written to the console.
  --pretty-print         Pretty print the JSON output.
  -?, -h, --help         Show help and usage information
```

##### Pagination

Results can be paginated using the `--limit` and `--skip` options.

Example:
```shell
    dofusdb items list --limit 50 --skip 100
```

##### Column selection

Results can be limited to specific fields using the `--select` option.

Example:
```shell
    dofusdb items list --select id,name,level
```

##### Sorting

Results can be sorted by one or more fields using the `--sort` option. Prefix a field with `-` for descending order.

Example:
```shell
    dofusdb items list --sort level,-name
```

##### Filtering

Results can be filtered using the `--filter` option. Each predicate is made of the name of the field, an operator (`=`, `!=`, `<`, `<=`, `>`, `>=`) and the value.

Example:
```shell
    dofusdb items list --filter "level>=10,name=Excalibur"
```

Additionally, equality and inequality operators accept multiple values separated by `|`, which transform them into IN and Not IN operators.

#### `get`

Fetch a single resource by its unique identifier.

```
Description:
  Get items by id.

Utilisation :
  dofusdb items get <id> [options]

Arguments :
  <id>  Unique identifier of the resource.

Options :
  -o, --output <output>  File to write the JSON output to. If not specified, the output will be written to the console.
  --pretty-print         Pretty print the JSON output.
  -?, -h, --help         Show help and usage information
```

Example:
```shell
    dofusdb items get 123
```

#### `count`

Get the total count of resources that would be returned by the `list` command with the same filters applied.

```
Description:
  Count items.

Utilisation :
  dofusdb items count [options]

Options :
  --filter <filter>  Comma separated list of predicates to filter the results by. Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values. Example: 
                     --filter "level>=10,name=Excalibur"
  -?, -h, --help     Show help and usage information
```

Example:
```shell
    dofusdb items count --filter "level>=10"
```

#### `build-query`

Build the search query URL for the specified parameters without executing the request.

```
Description:
  Build the search query for items.

Utilisation :
  dofusdb items build-query [options]

Options :
  --limit <limit>    Number of results to get. This might lead to multiple requests if the limit exceeds the API's maximum page size.
  --skip <skip>      Number of results to skip.
  --select <select>  Comma separated list of fields to include in the results. If not specified, all fields are included.
  --sort <sort>      Comma separated list of fields to sorts the results by. Prefix with '-' for descending order.
  --filter <filter>  Comma separated list of predicates to filter the results by. Each predicate is made of the name of the field, an operator (=, !=, <, <=, >, >=) and the value. Multiple values can be separated by '|' for '=' operator (in) and for '!=' operator (not in) to match any of the values. Example: 
                     --filter "level>=10,name=Excalibur"
  --base <base>      Base URL to use when building the query URL. [default: https://api.beta.dofusdb.fr/items/]
  -?, -h, --help     Show help and usage information
```

Example:
```shell
    dofusdb items build-query --filter "level>=10" --sort level
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests for bug fixes, features, or documentation improvements.

## License

This project is licensed under the terms of the [MIT License](../LICENSE.md).
