using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.Dofocus.ApiClients.Models.Items;
using FluentAssertions;

namespace Tests.EndToEnd.Dofocus.ApiClients;

public class ItemsClientTest
{
    [Fact]
    public async Task ItemsClient_Should_GetItem()
    {
        DofocusItemsClient client = DofocusClient.Items();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        DofocusItem result = await client.GetItemAsync(14097);

        result.Should()
            .BeEquivalentTo(
                new
                {
                    Id = 14097,
                    Name = new DofocusMultiLangString
                    {
                        Fr = "Amulette du Nocturlabe",
                        En = "Nocturnowl Amulet",
                        Es = "Amuleto de nocturlabiúho"
                    },
                    Type = new DofocusType
                    {
                        Id = 1,
                        Name = new DofocusMultiLangString
                        {
                            Fr = "Amulette",
                            En = "Amulet",
                            Es = "Amuleto"
                        }
                    },
                    SuperType = new DofocusSuperType
                    {
                        Id = 1,
                        Name = new DofocusMultiLangString
                        {
                            Fr = "Amulette",
                            En = "Amulet",
                            Es = "Amuleto"
                        }
                    },
                    ImageUrl = "https://api.dofusdb.fr/img/items/1236.png",
                    Level = 200,
                    Characteristics = new List<DofocusItemCharacteristic>
                    {
                        new()
                        {
                            Id = 11,
                            Name = new DofocusMultiLangString { Fr = "Vitalité", En = "Vitality", Es = "Vitalidad" },
                            From = 301,
                            To = 350
                        },
                        new()
                        {
                            Id = 10,
                            Name = new DofocusMultiLangString { Fr = "Force", En = "Strength", Es = "Fuerza" },
                            From = 71,
                            To = 100
                        },
                        new()
                        {
                            Id = 12,
                            Name = new DofocusMultiLangString { Fr = "Sagesse", En = "Wisdom", Es = "Sabiduría" },
                            From = 31,
                            To = 40
                        },
                        new()
                        {
                            Id = 18,
                            Name = new DofocusMultiLangString { Fr = "Coups Critiques (%)", En = "Critical Hits (%)", Es = "Golpes Críticos (%)" },
                            From = -5,
                            To = -7
                        },
                        new()
                        {
                            Id = 1,
                            Name = new DofocusMultiLangString { Fr = "Points d'action (PA)", En = "Action Points (AP)", Es = "Puntos de Acción (PA)" },
                            From = 1,
                            To = 0
                        },
                        new()
                        {
                            Id = 48,
                            Name = new DofocusMultiLangString { Fr = "Prospection", En = "Prospecting", Es = "Prospección" },
                            From = 16,
                            To = 20
                        },
                        new()
                        {
                            Id = 88,
                            Name = new DofocusMultiLangString { Fr = "Dommages Terre (fixe)", En = "Earth Damage (fixed)", Es = "Daños de Tierra (fijos)" },
                            From = 11,
                            To = 15
                        },
                        new()
                        {
                            Id = 56,
                            Name = new DofocusMultiLangString { Fr = "Résistances Eau (fixe)", En = "Water Resistance (fixed)", Es = "Resistencia a Agua (fija)" },
                            From = 16,
                            To = 20
                        },
                        new()
                        {
                            Id = 36,
                            Name = new DofocusMultiLangString { Fr = "Résistances Air (%)", En = "Air Resistance (%)", Es = "Resistencia a Aire (%)" },
                            From = 7,
                            To = 10
                        },
                        new()
                        {
                            Id = 86,
                            Name = new DofocusMultiLangString { Fr = "Dommages Critiques", En = "Critical Damage", Es = "Daños Críticos" },
                            From = 11,
                            To = 15
                        }
                    }
                },
                opt => opt.ExcludingMissingMembers()
            );
    }

    [Fact]
    public async Task ItemsClient_Should_GetItems()
    {
        DofocusItemsClient client = DofocusClient.Items();

        // we don't want to assert results here because they might change with each update, we just want to ensure that all the items are parsed correctly
        // which means that no exception is thrown during the search
        Func<Task> action = () => client.GetItemsAsync();

        await action.Should().NotThrowAsync();
    }
}
