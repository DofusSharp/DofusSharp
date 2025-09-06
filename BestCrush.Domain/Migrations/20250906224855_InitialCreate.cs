using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCrush.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DofusDbId = table.Column<long>(type: "INTEGER", nullable: false),
                    DofusDbIconId = table.Column<long>(type: "INTEGER", nullable: true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCoefficientRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<long>(type: "INTEGER", nullable: false),
                    ServerName = table.Column<string>(type: "TEXT", nullable: false),
                    Coefficient = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCoefficientRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemPriceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemId = table.Column<long>(type: "INTEGER", nullable: false),
                    ServerName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPriceRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DofusDbId = table.Column<long>(type: "INTEGER", nullable: false),
                    DofusDbIconId = table.Column<long>(type: "INTEGER", nullable: true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunePriceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RuneId = table.Column<long>(type: "INTEGER", nullable: false),
                    ServerName = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunePriceRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Runes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DofusDbId = table.Column<long>(type: "INTEGER", nullable: false),
                    DofusDbIconId = table.Column<long>(type: "INTEGER", nullable: true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Characteristic = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upgrades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Kind = table.Column<int>(type: "INTEGER", nullable: false),
                    OldVersion = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    NewVersion = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    UpgradeDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upgrades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCharacteristicLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Characteristic = table.Column<int>(type: "INTEGER", nullable: false),
                    From = table.Column<int>(type: "INTEGER", nullable: false),
                    To = table.Column<int>(type: "INTEGER", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCharacteristicLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCharacteristicLine_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ResourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeEntry_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeEntry_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCharacteristicLine_EquipmentId",
                table: "ItemCharacteristicLine",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCoefficientRecords_ItemId",
                table: "ItemCoefficientRecords",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCoefficientRecords_ServerName",
                table: "ItemCoefficientRecords",
                column: "ServerName");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceRecords_ItemId",
                table: "ItemPriceRecords",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceRecords_ServerName",
                table: "ItemPriceRecords",
                column: "ServerName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEntry_EquipmentId",
                table: "RecipeEntry",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEntry_ResourceId",
                table: "RecipeEntry",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_RunePriceRecords_RuneId",
                table: "RunePriceRecords",
                column: "RuneId");

            migrationBuilder.CreateIndex(
                name: "IX_RunePriceRecords_ServerName",
                table: "RunePriceRecords",
                column: "ServerName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCharacteristicLine");

            migrationBuilder.DropTable(
                name: "ItemCoefficientRecords");

            migrationBuilder.DropTable(
                name: "ItemPriceRecords");

            migrationBuilder.DropTable(
                name: "RecipeEntry");

            migrationBuilder.DropTable(
                name: "RunePriceRecords");

            migrationBuilder.DropTable(
                name: "Runes");

            migrationBuilder.DropTable(
                name: "Upgrades");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
