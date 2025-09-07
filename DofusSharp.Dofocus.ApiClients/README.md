# [Dofocus](https://dofocus.fr) API clients

This is a C# library for accessing Dofocus and DofusSharp APIs, providing models and clients for items, runes, servers, and related operations.

## Features

- Strongly-typed models for Dofocus items, runes, servers, and prices
- API clients for querying and updating data
- Request and response models for price and coefficient updates

## Installation

Add the NuGet package to your project:

```
dotnet add package DofusSharp.Dofocus.ApiClients
```

## Usage

```csharp
using DofusSharp.Dofocus.ApiClients;

IDofocusItemsClient client = DofocusClient.Production.Items();

// Fetch all items
IReadOnlyCollection<DofocusItemMinimal> items = await client.GetItemsAsync(); 

// Fetch details about one item, including its prices and coefficients for each server
long itemId = items.First().Id;
DofocusItem item = await client.GetItemByIdAsync(itemId); 
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests for bug fixes, features, or documentation improvements.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.