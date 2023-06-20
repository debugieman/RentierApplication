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

        public DbSet<RealEstate> ?RealEstates { get; set; }

        public DbSet<Tenants>? Tenants { get; set; }
        //RentierApplication.Data.Entities.Tenants

        



    }






}

