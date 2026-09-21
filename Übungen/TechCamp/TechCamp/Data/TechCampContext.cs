using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechCamp.Models;


namespace TechCamp.Data
{
    public class TechCampContext : DbContext
    {
        public TechCampContext(DbContextOptions<TechCampContext> options)
            : base(options)
        {
        }

        // Define your DbSets here

        public DbSet<Raum> Räume => Set<Raum>();
        public DbSet<Kurs> Kurse => Set<Kurs>();
        public DbSet<Teilnehmer> Teilnehmer => Set<Teilnehmer>();
        public DbSet<Dozent> Dozenten => Set<Dozent>();
        public DbSet<KursTeilnehmer> KursTeilnehmer => Set<KursTeilnehmer>();
        public DbSet<KursDozent> KursDozenten => Set<KursDozent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Raum 
            modelBuilder.Entity<Raum>()
            .Property(r => r.Bezeichnung)
            .HasMaxLength(50)
            .IsRequired();

            // Kurs
            modelBuilder.Entity<Kurs>()
            .Property(k => k.Titel)
            .HasMaxLength(100)
            .IsRequired();
            modelBuilder.Entity<Kurs>()
            .Property(k => k.MaxTeilnehmer)
            .HasDefaultValue(20);

            modelBuilder.Entity<Kurs>()
            .Property(k => k.Kursart)
            .HasConversion<string>();

            // Kurs _> Raum
            modelBuilder.Entity<Kurs>()
            .HasOne(k => k.Raum)
            .WithMany(r => r.Kurse)
            .HasForeignKey(k => k.RaumId)
            .OnDelete(DeleteBehavior.Restrict);

            // KursDozent
            modelBuilder.Entity<KursDozent>()
            .HasKey(kd => new { kd.KursId, kd.DozentId });

            modelBuilder.Entity<KursDozent>()
            .HasOne(kd => kd.Kurs)
            .WithMany(k => k.KursDozenten)
            .HasForeignKey(kd => kd.KursId);

            modelBuilder.Entity<KursDozent>()
            .HasOne(kd => kd.Dozent)
            .WithMany(d => d.KursDozenten)
            .HasForeignKey(kd => kd.DozentId);


            // KursTeilnehmer
            modelBuilder.Entity<KursTeilnehmer>()
            .HasKey(kt => new { kt.KursId, kt.TeilnehmerId });

            modelBuilder.Entity<KursTeilnehmer>()
            .HasOne(kt => kt.Kurs)
            .WithMany(k => k.KursTeilnehmer)
            .HasForeignKey(kt => kt.KursId);

            modelBuilder.Entity<KursTeilnehmer>()
            .Property(kt => kt.Status)
            .HasConversion<string>();

            modelBuilder.Entity<KursTeilnehmer>()
            .HasOne(kt => kt.Teilnehmer)
            .WithMany(t => t.KursTeilnehmer)
            .HasForeignKey(kt => kt.TeilnehmerId);



        }
    }
}