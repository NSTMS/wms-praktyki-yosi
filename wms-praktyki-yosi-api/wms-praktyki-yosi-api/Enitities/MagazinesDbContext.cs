using Microsoft.EntityFrameworkCore;
using System.Net;

namespace wms_praktyki_yosi_api.Enitities
{
    public class MagazinesDbContext : DbContext
    {
        private readonly ConnectionsStrings _connectionStrings;

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public MagazinesDbContext( ConnectionsStrings connetionsStrings)
        {
            _connectionStrings = connetionsStrings;
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

            
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStrings.database);
        }
    }


}
