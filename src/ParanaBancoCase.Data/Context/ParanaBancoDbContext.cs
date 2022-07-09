using Microsoft.EntityFrameworkCore;
using ParanaBancoCase.Business.Models;

namespace ParanaBancoCase.Data.Context;

public class ParanaBancoDbContext : DbContext
{
    public ParanaBancoDbContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParanaBancoDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}