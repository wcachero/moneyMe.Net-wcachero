using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackListedEmailDomain",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackListedEmailDomain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlacklistedPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedPhoneNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanProduct",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxTerm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InitialMonthNoInterest = table.Column<int>(type: "int", nullable: false),
                    ProcessFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateAdded = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLoanDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Repayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyAmortization = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalRepayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    FinanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateSubmitted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateUpdated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLoanDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerLoanDetails_CustomerDetail_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CustomerDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerLoanDetails_LoanProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLoanDetails_CustomerId",
                table: "CustomerLoanDetails",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLoanDetails_ProductId",
                table: "CustomerLoanDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackListedEmailDomain");

            migrationBuilder.DropTable(
                name: "BlacklistedPhoneNumbers");

            migrationBuilder.DropTable(
                name: "CustomerLoanDetails");

            migrationBuilder.DropTable(
                name: "CustomerDetail");

            migrationBuilder.DropTable(
                name: "LoanProduct");
        }
    }
}
