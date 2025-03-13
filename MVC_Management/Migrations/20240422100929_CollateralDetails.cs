using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Management.Migrations
{
    /// <inheritdoc />
    public partial class CollateralDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Collateral",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Condition",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collateral",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "Loans");
        }
    }
}
