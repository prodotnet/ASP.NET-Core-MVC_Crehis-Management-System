using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Management.Migrations
{
    /// <inheritdoc />
    public partial class workdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferanceName",
                table: "Registration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReferanceNumber",
                table: "Registration",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Work",
                table: "Registration",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferanceName",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "ReferanceNumber",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "Work",
                table: "Registration");
        }
    }
}
