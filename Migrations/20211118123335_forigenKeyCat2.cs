using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class forigenKeyCat2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatKey",
                table: "splitTransactions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatKey",
                table: "splitTransactions");
        }
    }
}
