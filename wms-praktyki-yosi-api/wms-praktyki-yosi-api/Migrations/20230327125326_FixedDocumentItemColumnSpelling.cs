using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wms_praktyki_yosi_api.Migrations
{
    /// <inheritdoc />
    public partial class FixedDocumentItemColumnSpelling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityReal",
                table: "DocumentItems",
                newName: "QuantityDone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantityDone",
                table: "DocumentItems",
                newName: "QuantityReal");
        }
    }
}
