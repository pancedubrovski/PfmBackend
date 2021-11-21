using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class MccEntity3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_MccEntity_Mcc",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MccEntity",
                table: "MccEntity");

            migrationBuilder.RenameTable(
                name: "MccEntity",
                newName: "MccCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MccCodes",
                table: "MccCodes",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_MccCodes_Mcc",
                table: "transactions",
                column: "Mcc",
                principalTable: "MccCodes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_MccCodes_Mcc",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MccCodes",
                table: "MccCodes");

            migrationBuilder.RenameTable(
                name: "MccCodes",
                newName: "MccEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MccEntity",
                table: "MccEntity",
                column: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_MccEntity_Mcc",
                table: "transactions",
                column: "Mcc",
                principalTable: "MccEntity",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
