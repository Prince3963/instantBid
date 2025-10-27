using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace instantBid.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Items_ItemsItemId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_ItemsItemId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ItemsItemId",
                table: "Auctions");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ItemId",
                table: "Auctions",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Items_ItemId",
                table: "Auctions",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Items_ItemId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_ItemId",
                table: "Auctions");

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
    }
}
