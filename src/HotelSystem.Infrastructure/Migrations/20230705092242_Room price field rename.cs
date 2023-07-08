using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Roompricefieldrename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartPrice",
                table: "Rooms",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Rooms",
                newName: "StartPrice");
        }
    }
}
