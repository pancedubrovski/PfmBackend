using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class CatCodeKey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_splitTransactions_categories_CategoryEntityCode",
                table: "splitTransactions");

            migrationBuilder.RenameColumn(
                name: "CategoryEntityCode",
                table: "splitTransactions",
                newName: "CatCode");

            migrationBuilder.RenameIndex(
                name: "IX_splitTransactions_CategoryEntityCode",
                table: "splitTransactions",
                newName: "IX_splitTransactions_CatCode");

            migrationBuilder.AddForeignKey(
                name: "FK_splitTransactions_categories_CatCode",
                table: "splitTransactions",
                column: "CatCode",
                principalTable: "categories",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_splitTransactions_categories_CatCode",
                table: "splitTransactions");

            migrationBuilder.RenameColumn(
                name: "CatCode",
                table: "splitTransactions",
                newName: "CategoryEntityCode");

            migrationBuilder.RenameIndex(
                name: "IX_splitTransactions_CatCode",
                table: "splitTransactions",
                newName: "IX_splitTransactions_CategoryEntityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_splitTransactions_categories_CategoryEntityCode",
                table: "splitTransactions",
                column: "CategoryEntityCode",
                principalTable: "categories",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
