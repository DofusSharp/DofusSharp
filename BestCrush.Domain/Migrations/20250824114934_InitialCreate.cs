using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestCrush.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentVersions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DofusDbVersion = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentVersions", x => x.Id);
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
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DofusDbId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
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
                name: "ItemCharacteristicLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Characteristic = table.Column<int>(type: "INTEGER", nullable: false),
                    From = table.Column<int>(type: "INTEGER", nullable: false),
                    To = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCharacteristicLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCharacteristicLine_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCharacteristicLine_ItemId",
                table: "ItemCharacteristicLine",
                column: "ItemId");

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
                name: "CurrentVersions");

            migrationBuilder.DropTable(
                name: "ItemCharacteristicLine");

            migrationBuilder.DropTable(
                name: "ItemCoefficientRecords");

            migrationBuilder.DropTable(
                name: "ItemPriceRecords");

            migrationBuilder.DropTable(
                name: "RunePriceRecords");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
