using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFTAuctionService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NFTAuctions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservePrice = table.Column<int>(type: "integer", nullable: false),
                    Seller = table.Column<string>(type: "text", nullable: true),
                    Winner = table.Column<string>(type: "text", nullable: true),
                    SoldPrice = table.Column<int>(type: "integer", nullable: true),
                    CurrentHighestBid = table.Column<int>(type: "integer", nullable: true),
                    NFTAuctionEndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFTAuctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NFTAuctionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Collection = table.Column<string>(type: "text", nullable: true),
                    IndexInCollection = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Artist = table.Column<string>(type: "text", nullable: true),
                    ContentUrl = table.Column<string>(type: "text", nullable: true),
                    NFTAuctionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NFTAuctionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NFTAuctionItems_NFTAuctions_NFTAuctionId",
                        column: x => x.NFTAuctionId,
                        principalTable: "NFTAuctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NFTAuctionItems_NFTAuctionId",
                table: "NFTAuctionItems",
                column: "NFTAuctionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NFTAuctionItems");

            migrationBuilder.DropTable(
                name: "NFTAuctions");
        }
    }
}
