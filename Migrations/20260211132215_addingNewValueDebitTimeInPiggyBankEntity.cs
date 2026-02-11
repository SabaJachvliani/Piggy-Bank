using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiggyBank.Migrations
{
    /// <inheritdoc />
    public partial class addingNewValueDebitTimeInPiggyBankEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DebitTime",
                table: "PiggyBank",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebitTime",
                table: "PiggyBank");
        }
    }
}
