using Microsoft.EntityFrameworkCore;
using LoggingDemo.Models;

namespace LoggingDemo.Data
{
    //public class AppDbContext : DbContext      // ersetzt durch pimary constructor
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // ersetzt durch pimary constructor
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{
        //}


        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Author> Authors { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Book>()
        //        .HasKey(b => b.Id);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Title = "EUR", Description= "European EURO" },
                new Currency() { Id = 2, Title = "Dollar", Description= "US Dollar" },
                new Currency() { Id = 3, Title = "Dinar", Description= "Dinar" },
                new Currency() { Id = 4, Title = "INR", Description= "Indian INR" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "Deutsch", Description = "Deutsch" },
                new Language() { Id = 2, Title = "English", Description = "English" },
                new Language() { Id = 3, Title = "Spanisch", Description = "Spanisch" },
                new Language() { Id = 4, Title = "Italienisch", Description = "Italienisch" }
                );
        }
    }
}
