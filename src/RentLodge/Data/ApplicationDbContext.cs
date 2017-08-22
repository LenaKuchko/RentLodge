using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentLodge.Models;

namespace RentLodge.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public ApplicationDbContext()
        {

        }
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RentLodge;integrated security=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
