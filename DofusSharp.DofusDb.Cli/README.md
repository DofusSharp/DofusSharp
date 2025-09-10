# [DofusDB](https://dofusdb.fr) CLI tool

## Overview

Command line interface (CLI) tool that exposes the API clients from the [DofusSharp.DofusDb.ApiClients](../DofusSharp.DofusDb.ApiClients) library. It allows users to interact with DofusDB resources and perform queries directly from the terminal.

## Features

- Query DofusDB items, recipes, runes, and other resources
- Fetch item stats and details
- Search and filter items by criteria

## Installation

### Using dotnet tool (recommended)
```
dotnet tool install --global dofusdb
```

### Using binaries

Download the latest release from the [GitHub releases page](https://github.com/ismailbennani/DofusSharp/releases/latest).

## Usage

Fetch items with level between 20 and 50, of type 5, skip the first 10 results, limit to 100 results, and save output to `result.json`:
```
dofusdb items search --skip 10 --limit 100 --filter level 20..50 --filter typeId 5 --output result.json
```

- List available clients:
  ```
  dofusdb-cli --help
  ```
- List commands:
  ```
  dofusdb-cli items --help
  ```
- Get help for a specific commands:
  ```
  dofusdb-cli items search --help
  ```

## Contributing

Contributions are welcome! Please open issues or submit pull requests for bug fixes, features, or documentation improvements.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
