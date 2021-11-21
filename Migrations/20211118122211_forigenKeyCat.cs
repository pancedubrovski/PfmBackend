using Microsoft.EntityFrameworkCore.Migrations;

namespace PmfBackend.Migrations
{
    public partial class forigenKeyCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "transactions",
                type: "character(3)",
                fixedLength: true,
                maxLength: 3,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(3)",
                oldFixedLength: true,
                oldMaxLength: 3);
        }
    }
}
