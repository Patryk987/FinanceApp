using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FamilyGroup",
                columns: table => new
                {
                    IdGroup = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyGroup", x => x.IdGroup);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    idGroup = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.idGroup);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    IdShop = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.IdShop);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdGroup = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    idGroup = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_idGroup",
                        column: x => x.idGroup,
                        principalTable: "ProductGroup",
                        principalColumn: "idGroup");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    IdShop = table.Column<int>(type: "int", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataDokumentu = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_IdShop",
                        column: x => x.IdShop,
                        principalTable: "Shops",
                        principalColumn: "IdShop");
                    table.ForeignKey(
                        name: "FK_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    amountPLN = table.Column<decimal>(type: "money", nullable: false),
                    typeOfPayments = table.Column<short>(type: "smallint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    amountWal = table.Column<decimal>(type: "money", nullable: false),
                    waluta = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_P_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentPos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    idDoc = table.Column<int>(type: "int", nullable: false),
                    idProd = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_idDoc",
                        column: x => x.idDoc,
                        principalTable: "Documents",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_idProd",
                        column: x => x.idProd,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Savings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Idpaymants = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oszczednosci", x => x.id);
                    table.ForeignKey(
                        name: "FK_Idpaymants",
                        column: x => x.Idpaymants,
                        principalTable: "Payments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "FK_2",
                table: "DocumentPos",
                column: "idDoc");

            migrationBuilder.CreateIndex(
                name: "FK_3",
                table: "DocumentPos",
                column: "idProd");

            migrationBuilder.CreateIndex(
                name: "FK_2",
                table: "Documents",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "FK_3",
                table: "Documents",
                column: "IdShop");

            migrationBuilder.CreateIndex(
                name: "FK_2",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "FK_2",
                table: "products",
                column: "idGroup");

            migrationBuilder.CreateIndex(
                name: "FK_2",
                table: "Savings",
                column: "Idpaymants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentPos");

            migrationBuilder.DropTable(
                name: "FamilyGroup");

            migrationBuilder.DropTable(
                name: "Savings");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
