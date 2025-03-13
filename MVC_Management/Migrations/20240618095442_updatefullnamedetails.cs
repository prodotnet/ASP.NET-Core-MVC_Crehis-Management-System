using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Management.Migrations
{
    /// <inheritdoc />
    public partial class updatefullnamedetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Loans");
        }
    }
}
