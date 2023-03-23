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


            base.OnModelCreating(modelBuilder);
            /* modelBuilder.Entity<User>()
                 .Property(u => u.Email)
                 .IsRequired();

             modelBuilder.Entity<Role>()
                 .Property(u => u.Name)
                 .IsRequired();*/

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.database);
        }
    }


}
