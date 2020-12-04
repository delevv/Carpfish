using Microsoft.EntityFrameworkCore.Migrations;

namespace Carpfish.Data.Migrations
{
    public partial class AddVoteDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LakeVotes_AspNetUsers_UserId",
                table: "LakeVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_LakeVotes_Vote_VoteId",
                table: "LakeVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TrophyVotes_AspNetUsers_UserId",
                table: "TrophyVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TrophyVotes_Vote_VoteId",
                table: "TrophyVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.RenameTable(
                name: "Vote",
                newName: "Votes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TrophyVotes",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_TrophyVotes_UserId",
                table: "TrophyVotes",
                newName: "IX_TrophyVotes_OwnerId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "LakeVotes",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_LakeVotes_UserId",
                table: "LakeVotes",
                newName: "IX_LakeVotes_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LakeVotes_AspNetUsers_OwnerId",
                table: "LakeVotes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LakeVotes_Votes_VoteId",
                table: "LakeVotes",
                column: "VoteId",
                principalTable: "Votes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrophyVotes_AspNetUsers_OwnerId",
                table: "TrophyVotes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrophyVotes_Votes_VoteId",
                table: "TrophyVotes",
                column: "VoteId",
                principalTable: "Votes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LakeVotes_AspNetUsers_OwnerId",
                table: "LakeVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_LakeVotes_Votes_VoteId",
                table: "LakeVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TrophyVotes_AspNetUsers_OwnerId",
                table: "TrophyVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_TrophyVotes_Votes_VoteId",
                table: "TrophyVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "Vote");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "TrophyVotes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrophyVotes_OwnerId",
                table: "TrophyVotes",
                newName: "IX_TrophyVotes_UserId");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "LakeVotes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LakeVotes_OwnerId",
                table: "LakeVotes",
                newName: "IX_LakeVotes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LakeVotes_AspNetUsers_UserId",
                table: "LakeVotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LakeVotes_Vote_VoteId",
                table: "LakeVotes",
                column: "VoteId",
                principalTable: "Vote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrophyVotes_AspNetUsers_UserId",
                table: "TrophyVotes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrophyVotes_Vote_VoteId",
                table: "TrophyVotes",
                column: "VoteId",
                principalTable: "Vote",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
