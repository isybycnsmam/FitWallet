using FitWallet.Core.Models;
using FitWallet.Core.Models.Transactions;
using FitWallet.Core.Models.Views;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitWallet.Database;

public class ApplicationDatabaseContext : IdentityDbContext<User>
{
    public ApplicationDatabaseContext(DbContextOptions options) : base(options) { }

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionElement> TransactionElements { get; set; }

    public DbSet<WalletCategorySpending> WalletsSpendingsPerCategory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureWalletEntity(modelBuilder);

        ConfigureTransactionEntity(modelBuilder);

        ConfigureCompanyEntity(modelBuilder);

        ConfigureCategoryEntity(modelBuilder);

        // TODO: TransactionElement category relation?

        modelBuilder.Entity<TransactionElement>()
            .HasOne(te => te.Category)
            .WithMany()
            .HasForeignKey(te => te.CategoryId)
            .IsRequired();

        modelBuilder.Entity<WalletCategorySpending>()
            .ToView("wallets_category_spendings")
            .HasNoKey();
    }

    private static void ConfigureWalletEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wallet>()
            .HasOne(w => w.User)
            .WithMany(u => u.Wallets)
            .HasForeignKey(w => w.UserId);

        modelBuilder.Entity<Wallet>()
            .HasMany(w => w.Transactions)
            .WithOne(t => t.Wallet)
            .HasForeignKey(t => t.WalletId)
            .IsRequired();

        modelBuilder.Entity<Wallet>()
            .HasIndex(p => new { p.UserId, p.Name })
            .IsUnique();
    }

    private static void ConfigureTransactionEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Company)
            .WithMany()
            .HasForeignKey(t => t.CompanyId)
            .IsRequired(false);

        modelBuilder.Entity<Transaction>()
            .HasMany(e => e.Elements)
            .WithOne(e => e.Transaction)
            .HasForeignKey(e => e.TransactionId)
            .IsRequired();

    }

    private static void ConfigureCompanyEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>()
            .HasIndex(c => new { c.UserId, c.Name })
            .IsUnique();

        modelBuilder.Entity<Company>()
            .HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);

        modelBuilder.Entity<Company>()
            .HasOne(c => c.User)
            .WithMany(u => u.Companies)
            .HasForeignKey(c => c.UserId)
            .IsRequired();
    }
    
    private static void ConfigureCategoryEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(c => new { c.UserId, c.Name })
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasIndex(c => new { c.UserId, c.DisplayColor })
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .IsRequired();
    }
}
