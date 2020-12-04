using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Carpfish.Data.Migrations
{
    public partial class AddVotesForLakeAndTrophy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatersCount",
                table: "Trophies");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Trophies");

            migrationBuilder.DropColumn(
                name: "RatersCount",
                table: "Lakes");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Lakes");

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LakeVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LakeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoteId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LakeVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LakeVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LakeVotes_Lakes_LakeId",
                        column: x => x.LakeId,
                        principalTable: "Lakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LakeVotes_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrophyVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrophyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VoteId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrophyVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrophyVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrophyVotes_Trophies_TrophyId",
                        column: x => x.TrophyId,
                        principalTable: "Trophies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrophyVotes_Vote_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Vote",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LakeVotes_LakeId",
                table: "LakeVotes",
                column: "LakeId");

            migrationBuilder.CreateIndex(
                name: "IX_LakeVotes_UserId",
                table: "LakeVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LakeVotes_VoteId",
                table: "LakeVotes",
                column: "VoteId");

            migrationBuilder.CreateIndex(
                name: "IX_TrophyVotes_TrophyId",
                table: "TrophyVotes",
                column: "TrophyId");

            migrationBuilder.CreateIndex(
                name: "IX_TrophyVotes_UserId",
                table: "TrophyVotes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrophyVotes_VoteId",
                table: "TrophyVotes",
                column: "VoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LakeVotes");

            migrationBuilder.DropTable(
                name: "TrophyVotes");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.AddColumn<int>(
                name: "RatersCount",
                table: "Trophies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Trophies",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RatersCount",
                table: "Lakes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Lakes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
