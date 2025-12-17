using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace instantBid.Migrations
{
    /// <inheritdoc />
    public partial class AddWinnerInAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Auctions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WinnerUserId",
                table: "Auctions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WinningAmount",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Winners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WinningAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnouncedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuctionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Winners_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId");
                    table.ForeignKey(
                        name: "FK_Winners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_WinnerUserId",
                table: "Auctions",
                column: "WinnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Winners_AuctionId",
                table: "Winners",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Winners_UserId",
                table: "Winners",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_WinnerUserId",
                table: "Auctions",
                column: "WinnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_WinnerUserId",
                table: "Auctions");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinnerUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinnerUserId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinningAmount",
                table: "Auctions");
        }
    }
}
