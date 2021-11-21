using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class ChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_CatCode",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_MccCodes_Mcc",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "Mcc",
                table: "transactions",
                newName: "mcc");

            migrationBuilder.RenameColumn(
                name: "Kind",
                table: "transactions",
                newName: "kind");

            migrationBuilder.RenameColumn(
                name: "IsSplit",
                table: "transactions",
                newName: "issplit");

            migrationBuilder.RenameColumn(
                name: "Direction",
                table: "transactions",
                newName: "direction");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "transactions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "transactions",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "transactions",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "CatCode",
                table: "transactions",
                newName: "catcode");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "transactions",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "BeneficiaryName",
                table: "transactions",
                newName: "beneficiary-name");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_Mcc",
                table: "transactions",
                newName: "IX_transactions_mcc");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_CatCode",
                table: "transactions",
                newName: "IX_transactions_catcode");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_catcode",
                table: "transactions",
                column: "catcode",
                principalTable: "categories",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_MccCodes_mcc",
                table: "transactions",
                column: "mcc",
                principalTable: "MccCodes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_categories_catcode",
                table: "transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_transactions_MccCodes_mcc",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "mcc",
                table: "transactions",
                newName: "Mcc");

            migrationBuilder.RenameColumn(
                name: "kind",
                table: "transactions",
                newName: "Kind");

            migrationBuilder.RenameColumn(
                name: "issplit",
                table: "transactions",
                newName: "IsSplit");

            migrationBuilder.RenameColumn(
                name: "direction",
                table: "transactions",
                newName: "Direction");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "transactions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "transactions",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "transactions",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "catcode",
                table: "transactions",
                newName: "CatCode");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "transactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "beneficiary-name",
                table: "transactions",
                newName: "BeneficiaryName");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_mcc",
                table: "transactions",
                newName: "IX_transactions_Mcc");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_catcode",
                table: "transactions",
                newName: "IX_transactions_CatCode");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_categories_CatCode",
                table: "transactions",
                column: "CatCode",
                principalTable: "categories",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_MccCodes_Mcc",
                table: "transactions",
                column: "Mcc",
                principalTable: "MccCodes",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
