using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Giveaways.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Giveaways",
                columns: table => new
                {
                    MessageId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    ChannelId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    GuildId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Prize = table.Column<string>(type: "TEXT", nullable: false),
                    MaxWinners = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giveaways", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "GiveawayParticipants",
                columns: table => new
                {
                    UserId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    GiveawayId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    IsWinner = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiveawayParticipants", x => new { x.UserId, x.GiveawayId });
                    table.ForeignKey(
                        name: "FK_GiveawayParticipants_Giveaways_GiveawayId",
                        column: x => x.GiveawayId,
                        principalTable: "Giveaways",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GiveawayParticipants_GiveawayId",
                table: "GiveawayParticipants",
                column: "GiveawayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiveawayParticipants");

            migrationBuilder.DropTable(
                name: "Giveaways");
        }
    }
}
