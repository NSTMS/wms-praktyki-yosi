using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wms_praktyki_yosi_api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shelf",
                table: "ProductLocations",
                newName: "ShelfId");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "ProductLocations",
                newName: "ShelvesId");

            migrationBuilder.CreateTable(
                name: "Shelves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagazineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelves", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocations_ShelvesId",
                table: "ProductLocations",
                column: "ShelvesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductLocations_Shelves_ShelvesId",
                table: "ProductLocations",
                column: "ShelvesId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductLocations_Shelves_ShelvesId",
                table: "ProductLocations");

            migrationBuilder.DropTable(
                name: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_ProductLocations_ShelvesId",
                table: "ProductLocations");

            migrationBuilder.RenameColumn(
                name: "ShelvesId",
                table: "ProductLocations",
                newName: "Row");

            migrationBuilder.RenameColumn(
                name: "ShelfId",
                table: "ProductLocations",
                newName: "Shelf");
        }
    }
}
