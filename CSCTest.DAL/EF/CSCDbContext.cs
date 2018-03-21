using CSCTest.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCTest.DAL.EF
{
    public class CSCDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Offering> Offerings { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<OrganizationCountry> OrganizationCountries { get; set; }
        public DbSet<CountryBusiness> CountryBusinesses { get; set; }
        public DbSet<BusinessFamily> BusinessFamilies { get; set; }
        public DbSet<FamilyOffering> FamilyOfferings { get; set; }

        public CSCDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ManyToMany(Organization, Country)
            modelBuilder.Entity<OrganizationCountry>()
                .HasAlternateKey(oc => new { oc.OrganizationId, oc.CountryId });

            // ManyToMany(OrganizationCountry, Business)
            modelBuilder.Entity<CountryBusiness>()
                .HasAlternateKey(cb => new { cb.OrganizationCountryId, cb.BusinessId });

            // ManyToMany(CountryBusiness, Family)
            modelBuilder.Entity<BusinessFamily>()
                .HasAlternateKey(cf => new { cf.CountryBusinessId, cf.FamilyId });

            //ManyToMany(BusinessFamily, Offering)
            modelBuilder.Entity<FamilyOffering>()
                .HasAlternateKey(fo => new { fo.BussinessFamilyId, fo.OfferingId });

            modelBuilder.Entity<BusinessFamily>()
                .HasOne(bf => bf.Family)
                .WithMany(f => f.BusinessFamilies)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}