using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.Data.Migrations
{
    /// <inheritdoc />
    public partial class UseXminForProductStockConcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ProductStocks");
            // Version property is mapped to PostgreSQL system column "xmin" - no column to add
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ProductStocks",
                type: "bytea",
                nullable: true);
            // xmin is a system column - cannot be dropped
        }
    }
}
