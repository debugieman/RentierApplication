using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentierApplication.Data.Entities;

namespace RentierApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<RealEstate>()
        //        .HasOne(b => b.Payment)
        //        .WithMany(a => a.Transactions)
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Cascade);
        //}

    }

}

