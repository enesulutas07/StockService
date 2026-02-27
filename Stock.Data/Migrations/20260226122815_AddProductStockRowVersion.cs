using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductStockRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ProductStocks",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ProductStocks");
        }
    }
}
