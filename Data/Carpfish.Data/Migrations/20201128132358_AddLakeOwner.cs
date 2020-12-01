namespace Carpfish.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddLakeOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lakes_AspNetUsers_ApplicationUserId",
                table: "Lakes");

            migrationBuilder.DropIndex(
                name: "IX_Lakes_ApplicationUserId",
                table: "Lakes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Lakes");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Lakes",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.CreateIndex(
                name: "IX_Lakes_OwnerId",
                table: "Lakes",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lakes_AspNetUsers_OwnerId",
                table: "Lakes",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lakes_AspNetUsers_OwnerId",
                table: "Lakes");

            migrationBuilder.DropIndex(
                name: "IX_Lakes_OwnerId",
                table: "Lakes");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Lakes");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Lakes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lakes_ApplicationUserId",
                table: "Lakes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lakes_AspNetUsers_ApplicationUserId",
                table: "Lakes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
