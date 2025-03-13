using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Management.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingPaymentAnddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "Loans",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Loans",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Loans");
        }
    }
}
