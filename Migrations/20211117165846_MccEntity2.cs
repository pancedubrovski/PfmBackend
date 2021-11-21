using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PmfBackend.Migrations
{
    public partial class MccEntity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyticsModels",
                columns: table => new
                {
                    CatCode = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    ParentCode = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Code);
                    table.ForeignKey(
                        name: "FK_categories_categories_CategoryCode",
                        column: x => x.CategoryCode,
                        principalTable: "categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MccEntity",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    MerchactType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MccEntity", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    BeneficiaryName = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Mcc = table.Column<string>(type: "text", nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    CatCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_categories_CatCode",
                        column: x => x.CatCode,
                        principalTable: "categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_transactions_MccEntity_Mcc",
                        column: x => x.Mcc,
                        principalTable: "MccEntity",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "splitTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionId = table.Column<string>(type: "text", nullable: true),
                    CategoryEntityCode = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_splitTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_splitTransactions_categories_CategoryEntityCode",
                        column: x => x.CategoryEntityCode,
                        principalTable: "categories",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_splitTransactions_transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_CategoryCode",
                table: "categories",
                column: "CategoryCode");

            migrationBuilder.CreateIndex(
                name: "IX_splitTransactions_CategoryEntityCode",
                table: "splitTransactions",
                column: "CategoryEntityCode");

            migrationBuilder.CreateIndex(
                name: "IX_splitTransactions_TransactionId",
                table: "splitTransactions",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_CatCode",
                table: "transactions",
                column: "CatCode");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_Mcc",
                table: "transactions",
                column: "Mcc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticsModels");

            migrationBuilder.DropTable(
                name: "splitTransactions");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "MccEntity");
        }
    }
}
