using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace wms_praktyki_yosi_api.Enitities
{
    public class MagazinesDbContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }

        public MagazinesDbContext(DbContextOptions<MagazinesDbContext> options) : base(options)
        {
           
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

    
    }


}
