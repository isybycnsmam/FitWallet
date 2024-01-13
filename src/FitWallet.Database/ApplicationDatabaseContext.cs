using FitWallet.Core.Models;
using FitWallet.Core.Models.Transactions;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureWalletEntity(modelBuilder);

        ConfigureTransactionEntity(modelBuilder);

        modelBuilder.Entity<TransactionElement>()
            .HasOne(te => te.Category)
            .WithMany()
            .HasForeignKey(te => te.CategoryId)
            .IsRequired();

        modelBuilder.Entity<Company>()
            .HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.Parent)
            .WithMany()
            .HasForeignKey(c => c.ParentId)
            .IsRequired(false);
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
}
