using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wms_praktyki_yosi_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedMaxLoadtoShelves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxLoad",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxLoad",
                table: "Shelves");
        }
    }
}
