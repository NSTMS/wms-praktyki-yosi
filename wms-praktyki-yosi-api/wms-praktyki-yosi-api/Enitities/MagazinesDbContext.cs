using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Enitities
{
    public class MagazinesDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductLocations> ProductLocations { get; set; }

        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentItem> DocumentItems { get; set; }

        private readonly ConnectionsStrings _connectionStrings;
        public MagazinesDbContext(DbContextOptions<MagazinesDbContext> options, ConnectionsStrings connectionStrings) : base(options)
        {
            _connectionStrings = connectionStrings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
                .Property(r => r.ProductName)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(r => r.EAN)
                .IsRequired()
                .HasMaxLength(13);

            modelBuilder.Entity<Document>()
                .Property(d => d.Date)
                .IsRequired();

            modelBuilder.Entity<Document>()
                .Property(d => d.Client)
                .IsRequired();

            modelBuilder.Entity<DocumentItem>()
                .Property(di => di.DocumentId)
                .IsRequired();

            modelBuilder.Entity<DocumentItem>()
                .Property(di => di.Quantityplaned)
                .IsRequired();

            modelBuilder.Entity<Document>()
                .Property(x => x.Version)
                .IsRowVersion();

            modelBuilder.Entity<DocumentItem>()
                .Property(x => x.Version)
                .IsRowVersion();

            modelBuilder.Entity<Magazine>()
                .Property(x => x.Version)
                .IsRowVersion();

            modelBuilder.Entity<Product>()
                .Property(x => x.Version)
                .IsRowVersion();

            modelBuilder.Entity<ProductLocations>()
                .Property(x => x.Version)
                .IsRowVersion();

            modelBuilder.Entity<Shelf>()
                .Property(x => x.Version)
                .IsRowVersion();



            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.database);
        }
    }


}
