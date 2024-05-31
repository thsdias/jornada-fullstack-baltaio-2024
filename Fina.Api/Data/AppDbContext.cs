using Fina.Api.Data.Mappings;
using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Transaction> Transactions { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new TransactionMapping());
    }
}