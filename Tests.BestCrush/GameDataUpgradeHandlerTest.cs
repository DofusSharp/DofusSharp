using BestCrush.Domain;
using BestCrush.Domain.Models;
using BestCrush.Domain.Services.Upgrades;
using DofusSharp.Dofocus.ApiClients;
using DofusSharp.Dofocus.ApiClients.Models.Common;
using DofusSharp.Dofocus.ApiClients.Models.Runes;
using DofusSharp.DofusDb.ApiClients;
using DofusSharp.DofusDb.ApiClients.Models.Characteristics;
using DofusSharp.DofusDb.ApiClients.Models.Common;
using DofusSharp.DofusDb.ApiClients.Models.Items;
using DofusSharp.DofusDb.ApiClients.Models.Jobs;
using DofusSharp.DofusDb.ApiClients.Search;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.BestCrush.Utils;

namespace Tests.BestCrush;

public class GameDataUpgradeHandlerTest : IDisposable
{
    static readonly string[] EquipmentTypeIds =
        ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "16", "17", "18", "121", "311", "19", "20", "21", "22", "82", "114", "151", "271"];

    const string RuneTypeId = "78";

    readonly TestDatabase _testDatabase;
    readonly BestCrushDbContext _context;
    readonly Mock<IDofusDbTableClient<DofusDbCharacteristic>> _dofusDbCharacteristicsClientMock;
    readonly Mock<IDofusDbTableClient<DofusDbItem>> _dofusDbItemsClientMock;
    readonly Mock<IDofusDbTableClient<DofusDbRecipe>> _dofusDbRecipesClientMock;
    readonly Mock<IDofocusRunesClient> _dofocusRunesClientMock;
    readonly GameDataUpgradeHandler _handler;

    public GameDataUpgradeHandlerTest()
    {
        _testDatabase = new TestDatabase();
        _context = _testDatabase.CreateContext();

        Mock<IDofusDbClientsFactory> clientsFactory = new();
        _dofusDbCharacteristicsClientMock = CommonMocks.TableClient<DofusDbCharacteristic>();
        clientsFactory.Setup(f => f.Characteristics()).Returns(_dofusDbCharacteristicsClientMock.Object);
        _dofusDbItemsClientMock = CommonMocks.TableClient<DofusDbItem>();
        clientsFactory.Setup(f => f.Items()).Returns(_dofusDbItemsClientMock.Object);
        _dofusDbRecipesClientMock = CommonMocks.TableClient<DofusDbRecipe>();
        clientsFactory.Setup(f => f.Recipes()).Returns(_dofusDbRecipesClientMock.Object);

        Mock<IDofocusClientFactory> dofocusClientFactory = new();
        _dofocusRunesClientMock = new Mock<IDofocusRunesClient>();
        _dofocusRunesClientMock.Setup(c => c.GetRunesAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);
        dofocusClientFactory.Setup(f => f.Runes()).Returns(_dofocusRunesClientMock.Object);

        IDofusDbQueryProvider queryProvider = DofusDbQuery.Create(clientsFactory.Object);
        _handler = new GameDataUpgradeHandler(_context, queryProvider, dofocusClientFactory.Object, Mock.Of<ILogger<GameDataUpgradeHandler>>());
    }

    public void Dispose() => _testDatabase.Dispose();

    [Fact]
    public async Task ShouldNotUpgrade_WhenVersionIsTheSame()
    {
        _context.Upgrades.Add(new Upgrade { Kind = UpgradeKind.DofusDb, NewVersion = "1.2.3", UpgradeDate = DateTime.Now });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        _dofusDbCharacteristicsClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        _dofusDbItemsClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()), Times.Never);
        _dofusDbRecipesClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ShouldUpgrade_WhenNoUpgradeExists()
    {
        await _handler.UpgradeAsync(new Version(1, 2, 3));

        _dofusDbCharacteristicsClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));
        _dofusDbItemsClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));
        _dofusDbRecipesClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));
    }

    [Theory]
    [InlineData("1.2.1")]
    [InlineData("1.2.4")]
    public async Task ShouldUpgrade_WhenVersionIsDifferent(string currentVersion)
    {
        _context.Upgrades.Add(new Upgrade { Kind = UpgradeKind.DofusDb, NewVersion = currentVersion, UpgradeDate = DateTime.Now });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        _dofusDbCharacteristicsClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));
        _dofusDbItemsClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));
        _dofusDbRecipesClientMock.Verify(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task ShouldSearchForEquipmentsByTypeIds()
    {
        await _handler.UpgradeAsync(new Version(1, 2, 3));
        _dofusDbItemsClientMock.Verify(q => q.SearchAsync(
                                           It.Is<DofusDbSearchQuery>(sq => sq
                                                                         .Predicates.OfType<DofusDbSearchPredicate.In>()
                                                                         .Any(p => p.Field == "typeId" && p.Value.SequenceEqual(EquipmentTypeIds))
                                           ),
                                           It.IsAny<CancellationToken>()
                                       )
        );
    }

    [Fact]
    public async Task ShouldSearchForRunesByTypeId()
    {
        await _handler.UpgradeAsync(new Version(1, 2, 3));
        _dofusDbItemsClientMock.Verify(q => q.SearchAsync(
                                           It.Is<DofusDbSearchQuery>(sq => sq.Predicates.OfType<DofusDbSearchPredicate.Eq>().Any(p => p.Field == "typeId" && p.Value == RuneTypeId)),
                                           It.IsAny<CancellationToken>()
                                       )
        );
    }

    [Fact]
    public async Task ShouldRegisterEquipment_WhenEmpty()
    {
        _dofusDbCharacteristicsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbCharacteristic>
                {
                    Data =
                    [
                        new DofusDbCharacteristic { Id = 147, Keyword = "actionPoints" },
                        new DofusDbCharacteristic { Id = 258, Keyword = "fireElementResistPercent" }
                    ],
                    Total = 2, Limit = 2, Skip = 0
                }
            );

        _dofusDbRecipesClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbRecipe>
                {
                    Data =
                    [
                        new DofusDbRecipe
                        {
                            Id = 147,
                            ResultId = 1,
                            Ingredients =
                            [
                                new DofusDbItem
                                {
                                    Id = 987,
                                    IconId = 258,
                                    Level = 357,
                                    Name = new DofusDbMultiLangString { Fr = "INGREDIENT_NAME" }
                                }
                            ],
                            Quantities = [5]
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 1,
                            TypeId = EquipmentType.Boots.ToDofusDbItemTypeId(),
                            IconId = 147,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "ITEM_NAME" },
                            Effects = [new DofusDbItemEffect { Characteristic = 147, From = 4, To = 6 }, new DofusDbItemEffect { Characteristic = 258, From = 2, To = 8 }],
                            HasRecipe = true,
                            RecipeIds = [147]
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Equipment[] equipments = await _context.Equipments.ToArrayAsync();
        Equipment? equipment = equipments.Should().ContainSingle().Which;

        Resource[] resources = await _context.Resources.ToArrayAsync();
        Resource? resource = resources.Should().ContainSingle().Which;

        await Verify(((IItem[])[equipment, resource]).OrderBy(i => i.DofusDbId));
    }

    [Fact]
    public async Task ShouldUpdateEquipment_WhenExistsAlready()
    {
        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 1,
                            TypeId = EquipmentType.Boots.ToDofusDbItemTypeId(),
                            IconId = 147,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "ITEM_NAME" }
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        Equipment dbEquipment = new(1)
        {
            DofusDbIconId = 11111,
            Level = 22222,
            Name = "OLD_NAME",
            Type = EquipmentType.Belt
        };

        _context.Equipments.Add(dbEquipment);

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Equipment[] equipments = await _context.Equipments.ToArrayAsync();
        Equipment? equipment = equipments.Should().ContainSingle().Which;

        await Verify(equipment);
    }

    [Fact]
    public async Task ShouldUpdateEquipmentCharacteristics_WhenExistsAlready()
    {
        _dofusDbCharacteristicsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbCharacteristic>
                {
                    Data =
                    [
                        new DofusDbCharacteristic { Id = 147, Keyword = "actionPoints" },
                        new DofusDbCharacteristic { Id = 258, Keyword = "fireElementResistPercent" }
                    ],
                    Total = 2, Limit = 2, Skip = 0
                }
            );

        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 1,
                            TypeId = EquipmentType.Boots.ToDofusDbItemTypeId(),
                            IconId = 147,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "ITEM_NAME" },
                            Effects = [new DofusDbItemEffect { Characteristic = 147, From = 4, To = 6 }, new DofusDbItemEffect { Characteristic = 258, From = 2, To = 8 }]
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        Equipment dbEquipment = new(1);
        dbEquipment.Characteristics.Add(new ItemCharacteristicLine(dbEquipment, Characteristic.DamageFlatAir, 3, 6));
        dbEquipment.Characteristics.Add(new ItemCharacteristicLine(dbEquipment, Characteristic.Ap, 1, 2));

        _context.Equipments.Add(dbEquipment);

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Equipment[] equipments = await _context.Equipments.ToArrayAsync();
        Equipment? equipment = equipments.Should().ContainSingle().Which;

        await Verify(equipment);
    }

    [Fact]
    public async Task ShouldUpdateEquipmentRecipe_WhenExistsAlready()
    {
        _dofusDbRecipesClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbRecipe>
                {
                    Data =
                    [
                        new DofusDbRecipe
                        {
                            Id = 147,
                            ResultId = 1,
                            Ingredients =
                            [
                                new DofusDbItem
                                {
                                    Id = 987,
                                    IconId = 258,
                                    Level = 357,
                                    Name = new DofusDbMultiLangString { Fr = "INGREDIENT_NAME1" }
                                },
                                new DofusDbItem
                                {
                                    Id = 321,
                                    IconId = 369,
                                    Level = 495,
                                    Name = new DofusDbMultiLangString { Fr = "INGREDIENT_NAME2" }
                                }
                            ],
                            Quantities = [5, 8]
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 1,
                            TypeId = EquipmentType.Boots.ToDofusDbItemTypeId(),
                            IconId = 147,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "ITEM_NAME" },
                            Effects = [new DofusDbItemEffect { Characteristic = 147, From = 4, To = 6 }, new DofusDbItemEffect { Characteristic = 258, From = 2, To = 8 }],
                            HasRecipe = true,
                            RecipeIds = [147]
                        }

                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        Resource dbResource1 = new(654) { DofusDbIconId = 111111, Level = 222222, Name = "OLD_INGREDIENT_NAME1" };
        Resource dbResource2 = new(987) { DofusDbIconId = 333333, Level = 444444, Name = "OLD_INGREDIENT_NAME" };
        Equipment dbEquipment = new(1);
        dbEquipment.Recipe.Add(new RecipeEntry(dbEquipment, dbResource1, 2));
        dbEquipment.Recipe.Add(new RecipeEntry(dbEquipment, dbResource2, 4));

        _context.Resources.Add(dbResource1);
        _context.Resources.Add(dbResource2);
        _context.Equipments.Add(dbEquipment);

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Equipment[] equipments = await _context.Equipments.ToArrayAsync();
        Equipment? equipment = equipments.Should().ContainSingle().Which;

        Resource[] resources = await _context.Resources.ToArrayAsync();
        resources.Should().HaveCount(2);

        await Verify(((IItem[])[equipment, ..resources]).OrderBy(i => i.DofusDbId).ToArray());
    }

    [Fact]
    public async Task ShouldRemoveEquipmentAndResources_WhenEquipmentIsRemoved()
    {
        Resource dbResource = new(987) { DofusDbIconId = 333333, Level = 444444, Name = "OLD_INGREDIENT_NAME" };
        Equipment dbEquipment = new(1)
        {
            DofusDbIconId = 11111,
            Level = 22222,
            Name = "OLD_NAME",
            Type = EquipmentType.Belt
        };
        dbEquipment.Characteristics.Add(new ItemCharacteristicLine(dbEquipment, Characteristic.Ap, 1, 2));
        dbEquipment.Recipe.Add(new RecipeEntry(dbEquipment, dbResource, 4));

        _context.Resources.Add(dbResource);
        _context.Equipments.Add(dbEquipment);

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Equipment[] equipments = await _context.Equipments.ToArrayAsync();
        equipments.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldRemoveResource_WhenEquipmentRecipeIsRemoved()
    {
        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 1,
                            TypeId = EquipmentType.Boots.ToDofusDbItemTypeId(),
                            IconId = 147,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "ITEM_NAME" }
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        Resource dbResource = new(987);
        Equipment dbEquipment = new(1);
        dbEquipment.Recipe.Add(new RecipeEntry(dbEquipment, dbResource, 4));

        _context.Resources.Add(dbResource);
        _context.Equipments.Add(dbEquipment);

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Resource[] resources = await _context.Resources.ToArrayAsync();
        resources.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldNotRemoveResource_WhenOtherEquipmentRecipeUsesIt()
    {
        _dofusDbRecipesClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbRecipe>
                {
                    Data =
                    [
                        new DofusDbRecipe
                        {
                            Id = 147,
                            ResultId = 1,
                            Ingredients =
                            [
                                new DofusDbItem
                                {
                                    Id = 987,
                                    IconId = 258,
                                    Level = 357,
                                    Name = new DofusDbMultiLangString { Fr = "INGREDIENT_NAME" }
                                }
                            ],
                            Quantities = [5]
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 1,
                            TypeId = EquipmentType.Boots.ToDofusDbItemTypeId(),
                            IconId = 147,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "ITEM_NAME" },
                            HasRecipe = true,
                            RecipeIds = [147]
                        }
                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );
        Resource dbResource = new(987);
        Equipment dbEquipment1 = new(1);
        dbEquipment1.Recipe.Add(new RecipeEntry(dbEquipment1, dbResource, 4));
        Equipment dbEquipment2 = new(2);
        dbEquipment2.Recipe.Add(new RecipeEntry(dbEquipment2, dbResource, 5));

        _context.Resources.Add(dbResource);
        _context.Equipments.Add(dbEquipment1);
        _context.Equipments.Add(dbEquipment2);

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Resource[] resources = await _context.Resources.ToArrayAsync();
        Resource? resource = resources.Should().ContainSingle().Which;

        await Verify(resource);
    }

    [Fact]
    public async Task ShouldRegisterRune_WhenEmpty()
    {
        _dofocusRunesClientMock
            .Setup(c => c.GetRunesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                [
                    new DofocusRune
                    {
                        Id = 123,
                        Name = new DofocusMultiLangString { Fr = "RUNE_NAME" },
                        CharacteristicId = 147,
                        CharacteristicName = new DofocusMultiLangString(),
                        Value = 0,
                        Weight = 0,
                        LatestPrices = []
                    }
                ]
            );

        _dofusDbCharacteristicsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbCharacteristic> { Data = [new DofusDbCharacteristic { Id = 147, Keyword = "actionPoints" }], Total = 1, Limit = 1, Skip = 0 }
            );

        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 123,
                            IconId = 269,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "RUNE_NAME" }
                        }

                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Rune[] runes = await _context.Runes.ToArrayAsync();
        Rune? rune = runes.Should().ContainSingle().Which;

        await Verify(rune);
    }

    [Fact]
    public async Task ShouldUpdateRune_WhenExistsAlready()
    {
        _dofocusRunesClientMock
            .Setup(c => c.GetRunesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                [
                    new DofocusRune
                    {
                        Id = 123,
                        Name = new DofocusMultiLangString { Fr = "RUNE_NAME" },
                        CharacteristicId = 147,
                        CharacteristicName = new DofocusMultiLangString(),
                        Value = 0,
                        Weight = 0,
                        LatestPrices = []
                    }
                ]
            );

        _dofusDbCharacteristicsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbCharacteristic> { Data = [new DofusDbCharacteristic { Id = 147, Keyword = "actionPoints" }], Total = 1, Limit = 1, Skip = 0 }
            );

        _dofusDbItemsClientMock
            .Setup(q => q.SearchAsync(It.IsAny<DofusDbSearchQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                new DofusDbSearchResult<DofusDbItem>
                {
                    Data =
                    [
                        new DofusDbItem
                        {
                            Id = 123,
                            IconId = 269,
                            Level = 159,
                            Name = new DofusDbMultiLangString { Fr = "RUNE_NAME" }
                        }

                    ],
                    Total = 1, Limit = 1, Skip = 0
                }
            );

        _context.Runes.Add(new Rune(123) { Characteristic = Characteristic.ApReduction, DofusDbIconId = 111111, Level = 2222222, Name = "OLD_NAME" });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Rune[] runes = await _context.Runes.ToArrayAsync();
        Rune? rune = runes.Should().ContainSingle().Which;

        await Verify(rune);
    }

    [Fact]
    public async Task ShouldRemoveRune_WhenRuneIsRemove()
    {
        _context.Runes.Add(new Rune(123) { Characteristic = Characteristic.ApReduction, DofusDbIconId = 111111, Level = 2222222, Name = "OLD_NAME" });
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        await _handler.UpgradeAsync(new Version(1, 2, 3));

        Rune[] runes = await _context.Runes.ToArrayAsync();
        runes.Should().BeEmpty();
    }
}
