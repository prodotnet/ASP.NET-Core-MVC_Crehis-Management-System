using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Management.Migrations
{
    /// <inheritdoc />
    public partial class updatebalance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Oustanding",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Oustanding",
                table: "Loans");
        }
    }
}
