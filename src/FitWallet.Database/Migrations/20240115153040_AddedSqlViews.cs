using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitWallet.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedSqlViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW wallets_category_spendings AS
                 SELECT w.""Id"" AS ""WalletId"",
                    w.""Name"" AS ""WalletName"",
                    w.""UserId"",
                    te.""CategoryId"",
                    c.""Name"" AS ""CategoryName"",
                    c.""DisplayColor"" AS ""CategoryDisplayColor"",
                    abs(round(sum(te.""Value"")::numeric, 2)) AS ""TotalSpent""
                   FROM ""Wallets"" w
                     LEFT JOIN ""Transactions"" t ON w.""Id"" = t.""WalletId""
                     LEFT JOIN ""TransactionElements"" te ON t.""Id"" = te.""TransactionId""
                     LEFT JOIN ""Categories"" c ON te.""CategoryId"" = c.""Id""
                  WHERE t.""OperationDate"" >= date_trunc('month'::text, CURRENT_DATE::timestamp with time zone) AND te.""Value"" < 0::double precision
                  GROUP BY te.""CategoryId"", w.""Id"", w.""Name"", w.""UserId"", c.""Name"", c.""DisplayColor"";");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW wallets_category_spendings;");
        }
    }
}
