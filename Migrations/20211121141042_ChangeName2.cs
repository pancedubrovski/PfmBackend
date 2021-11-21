using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class ChangeName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "beneficiary-name",
                table: "transactions",
                newName: "beneficiaryname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "beneficiaryname",
                table: "transactions",
                newName: "beneficiary-name");
        }
    }
}
