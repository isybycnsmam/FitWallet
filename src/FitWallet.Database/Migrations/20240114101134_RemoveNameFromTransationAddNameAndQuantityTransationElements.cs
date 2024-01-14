using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitWallet.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNameFromTransationAddNameAndQuantityTransationElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Transactions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransactionElements",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TransactionElements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransactionElements");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TransactionElements");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Transactions",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
