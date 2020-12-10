using Microsoft.EntityFrameworkCore.Migrations;

namespace Carpfish.Data.Migrations
{
    public partial class AddReferenceFromRigAndStepToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Rig_RigId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Rig_AspNetUsers_OwnerId",
                table: "Rig");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Rig_RigId",
                table: "Step");

            migrationBuilder.DropForeignKey(
                name: "FK_Trophies_Rig_RigId",
                table: "Trophies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Step",
                table: "Step");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rig",
                table: "Rig");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Rig");

            migrationBuilder.RenameTable(
                name: "Step",
                newName: "Steps");

            migrationBuilder.RenameTable(
                name: "Rig",
                newName: "Rigs");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameIndex(
                name: "IX_Step_RigId",
                table: "Steps",
                newName: "IX_Steps_RigId");

            migrationBuilder.RenameIndex(
                name: "IX_Rig_OwnerId",
                table: "Rigs",
                newName: "IX_Rigs_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Rig_IsDeleted",
                table: "Rigs",
                newName: "IX_Rigs_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Material_RigId",
                table: "Materials",
                newName: "IX_Materials_RigId");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Steps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Rigs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Steps",
                table: "Steps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rigs",
                table: "Rigs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ImageId",
                table: "Steps",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Rigs_ImageId",
                table: "Rigs",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Rigs_RigId",
                table: "Materials",
                column: "RigId",
                principalTable: "Rigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rigs_AspNetUsers_OwnerId",
                table: "Rigs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rigs_Image_ImageId",
                table: "Rigs",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Image_ImageId",
                table: "Steps",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Rigs_RigId",
                table: "Steps",
                column: "RigId",
                principalTable: "Rigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trophies_Rigs_RigId",
                table: "Trophies",
                column: "RigId",
                principalTable: "Rigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Rigs_RigId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Rigs_AspNetUsers_OwnerId",
                table: "Rigs");

            migrationBuilder.DropForeignKey(
                name: "FK_Rigs_Image_ImageId",
                table: "Rigs");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Image_ImageId",
                table: "Steps");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Rigs_RigId",
                table: "Steps");

            migrationBuilder.DropForeignKey(
                name: "FK_Trophies_Rigs_RigId",
                table: "Trophies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Steps",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_ImageId",
                table: "Steps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rigs",
                table: "Rigs");

            migrationBuilder.DropIndex(
                name: "IX_Rigs_ImageId",
                table: "Rigs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Rigs");

            migrationBuilder.RenameTable(
                name: "Steps",
                newName: "Step");

            migrationBuilder.RenameTable(
                name: "Rigs",
                newName: "Rig");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameIndex(
                name: "IX_Steps_RigId",
                table: "Step",
                newName: "IX_Step_RigId");

            migrationBuilder.RenameIndex(
                name: "IX_Rigs_OwnerId",
                table: "Rig",
                newName: "IX_Rig_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Rigs_IsDeleted",
                table: "Rig",
                newName: "IX_Rig_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_RigId",
                table: "Material",
                newName: "IX_Material_RigId");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Step",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Rig",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Step",
                table: "Step",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rig",
                table: "Rig",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Rig_RigId",
                table: "Material",
                column: "RigId",
                principalTable: "Rig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rig_AspNetUsers_OwnerId",
                table: "Rig",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Rig_RigId",
                table: "Step",
                column: "RigId",
                principalTable: "Rig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trophies_Rig_RigId",
                table: "Trophies",
                column: "RigId",
                principalTable: "Rig",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
