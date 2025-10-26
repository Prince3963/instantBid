using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace instantBid.Migrations
{
    /// <inheritdoc />
    public partial class updateAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Items",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Auctions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemsItemId",
                table: "Auctions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ItemsItemId",
                table: "Auctions",
                column: "ItemsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Items_ItemsItemId",
                table: "Auctions",
                column: "ItemsItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Items_ItemsItemId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_ItemsItemId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ItemsItemId",
                table: "Auctions");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Items",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
