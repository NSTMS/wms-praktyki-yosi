using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wms_praktyki_yosi_api.Migrations
{
    /// <inheritdoc />
    public partial class AddShelves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dimentions",
                table: "Magazines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_MagazineId",
                table: "Shelves",
                column: "MagazineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Magazines_MagazineId",
                table: "Shelves",
                column: "MagazineId",
                principalTable: "Magazines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Magazines_MagazineId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_MagazineId",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Dimentions",
                table: "Magazines");
        }
    }
}
