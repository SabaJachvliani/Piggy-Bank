using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PiggyBank.Migrations
{
    /// <inheritdoc />
    public partial class addUserPiggybankEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BankAccount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegistrationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PiggyBank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositorId = table.Column<int>(type: "int", nullable: false),
                    CashAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PiggyBank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PiggyBank_Users_DepositorId",
                        column: x => x.DepositorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PiggyBank_DepositorId",
                table: "PiggyBank",
                column: "DepositorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PiggyBank");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
