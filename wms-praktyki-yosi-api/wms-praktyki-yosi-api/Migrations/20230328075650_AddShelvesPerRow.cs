using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wms_praktyki_yosi_api.Migrations
{
    /// <inheritdoc />
    public partial class AddShelvesPerRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelvesPerRow",
                table: "Magazines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelvesPerRow",
                table: "Magazines");
        }
    }
}
