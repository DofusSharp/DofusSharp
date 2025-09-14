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
{{run:dofusdb --help}}
```

### Game version

```
{{run:dofusdb game-version --help}}
```

### Table data

```
{{run:dofusdb items --help}}
```

#### `list`

```
{{run:dofusdb items list --help}}
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
{{run:dofusdb items get --help}}
```

#### `count`

Get the total count of resources that would be returned by the `list` command with the same filters applied.

```
{{run:dofusdb items count --help}}
```

#### `build-query`

Build the search query URL for the specified parameters without executing the request.

```
{{run:dofusdb items build-query --help}}
```

### Image data

```
{{run:dofusdb item-images --help}}
```

#### `get`

```
{{run:dofusdb item-images get --help}}
```

### Scalable image data

```
{{run:dofusdb map-images --help}}
```

### `get`

```
{{run:dofusdb map-images get --help}}
```

### Criterion

```
{{run:dofusdb criterion --help}}
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests for bug fixes, features, or documentation improvements.

## License

This project is licensed under the terms of the [MIT License](../LICENSE.md).
