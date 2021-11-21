using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class forigenKeyCat3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatKey",
                table: "splitTransactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatKey",
                table: "splitTransactions",
                type: "text",
                nullable: true);
        }
    }
}
