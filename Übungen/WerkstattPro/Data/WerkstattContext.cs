
using WerkstattPro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WerkstattPro.Data;

public class WerkstattContext : DbContext
{
    public WerkstattContext(DbContextOptions<WerkstattContext> options)
        : base(options)
    { }

    public DbSet<Kunde> Kunden => Set<Kunde>();
    public DbSet<Fahrzeug> Fahrzeuge => Set<Fahrzeug>();
    public DbSet<Mechaniker> Mechaniker => Set<Mechaniker>();
    public DbSet<Ersatzteil> Ersatzteile => Set<Ersatzteil>();
    public DbSet<ReparaturAuftrag> ReparaturAuftraege => Set<ReparaturAuftrag>();
    public DbSet<AuftragMechaniker> AuftragMechaniker => Set<AuftragMechaniker>();
    public DbSet<AuftragErsatzteil> AuftragErsatzteile => Set<AuftragErsatzteil>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuftragMechaniker>()
            .HasKey(am => new
            {
                am.ReparaturAuftragId,
                am.MechanikerId
            });

        modelBuilder.Entity<AuftragErsatzteil>()
            .HasKey(ae => new
            {
                ae.ReparaturAuftragId,
                ae.ErsatzteilId
            });
        modelBuilder.Entity<Ersatzteil>()
.Property(e => e.Preis)
.HasPrecision(10, 2);

        modelBuilder.Entity<AuftragErsatzteil>()
            .Property(ae => ae.Einzlpreis)
            .HasPrecision(10, 2);
    }

}