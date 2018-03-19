using System;
using System.Threading.Tasks;
using CSCTest.Data.Entities;

namespace CSCTest.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; set; }
        IRepository<Organization> Organizations { get;}
        IRepository<Country> Countries { get;}
        IRepository<Business> Bussinesses { get; }
        IRepository<Family> Families { get; }
        IRepository<Offering> Offerings { get; }
        IRepository<Department> Departments { get; }

        IRepository<OrganizationCountry> OrganizationCountries { get; }
        IRepository<CountryBusiness> CountryBusinesses { get; }
        IRepository<BusinessFamily> BusinessFamilies { get; }
        IRepository<FamilyOffering> FamilyOfferings { get; }

        void Save();
        Task SaveAsync();
    }
}