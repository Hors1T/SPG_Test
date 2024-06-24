using Forging_calculation.Models;
using Microsoft.EntityFrameworkCore;

namespace Forging_calculation;

public class ApplicationDbContext : DbContext
{
    public DbSet<Allowance> Allowances { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allowance>()
            .ToTable("allowance")
            .HasKey(t => t.Id);
    }
}