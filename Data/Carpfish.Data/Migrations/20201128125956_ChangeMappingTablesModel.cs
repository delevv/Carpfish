using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Carpfish.Data.Migrations
{
    public partial class ChangeMappingTablesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Image_ImageId",
                table: "ItemImages");

            migrationBuilder.DropForeignKey(
                name: "FK_LakeImages_Image_ImageId",
                table: "LakeImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TrophyImages_Image_ImageId",
                table: "TrophyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrophyImages",
                table: "TrophyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LakeImages",
                table: "LakeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "TrophyImages",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TrophyImages",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TrophyImages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "TrophyImages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TrophyImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TrophyImages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "LakeImages",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "LakeId",
                table: "LakeImages",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LakeImages",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "LakeImages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "LakeImages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LakeImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "LakeImages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "ItemImages",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItemImages",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ItemImages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ItemImages",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ItemImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ItemImages",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrophyImages",
                table: "TrophyImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LakeImages",
                table: "LakeImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrophyImages_IsDeleted",
                table: "TrophyImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TrophyImages_TrophyId",
                table: "TrophyImages",
                column: "TrophyId");

            migrationBuilder.CreateIndex(
                name: "IX_LakeImages_IsDeleted",
                table: "LakeImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LakeImages_LakeId",
                table: "LakeImages",
                column: "LakeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_IsDeleted",
                table: "ItemImages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Image_ImageId",
                table: "ItemImages",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LakeImages_Image_ImageId",
                table: "LakeImages",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrophyImages_Image_ImageId",
                table: "TrophyImages",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemImages_Image_ImageId",
                table: "ItemImages");

            migrationBuilder.DropForeignKey(
                name: "FK_LakeImages_Image_ImageId",
                table: "LakeImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TrophyImages_Image_ImageId",
                table: "TrophyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrophyImages",
                table: "TrophyImages");

            migrationBuilder.DropIndex(
                name: "IX_TrophyImages_IsDeleted",
                table: "TrophyImages");

            migrationBuilder.DropIndex(
                name: "IX_TrophyImages_TrophyId",
                table: "TrophyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LakeImages",
                table: "LakeImages");

            migrationBuilder.DropIndex(
                name: "IX_LakeImages_IsDeleted",
                table: "LakeImages");

            migrationBuilder.DropIndex(
                name: "IX_LakeImages_LakeId",
                table: "LakeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_IsDeleted",
                table: "ItemImages");

            migrationBuilder.DropIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TrophyImages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TrophyImages");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "TrophyImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TrophyImages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TrophyImages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LakeImages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "LakeImages");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "LakeImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LakeImages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "LakeImages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ItemImages");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ItemImages");

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "TrophyImages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LakeId",
                table: "LakeImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "LakeImages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageId",
                table: "ItemImages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrophyImages",
                table: "TrophyImages",
                columns: new[] { "TrophyId", "ImageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LakeImages",
                table: "LakeImages",
                columns: new[] { "LakeId", "ImageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemImages",
                table: "ItemImages",
                columns: new[] { "ItemId", "ImageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemImages_Image_ImageId",
                table: "ItemImages",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LakeImages_Image_ImageId",
                table: "LakeImages",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrophyImages_Image_ImageId",
                table: "TrophyImages",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
