using System;
using System.Threading.Tasks;
using CSCTest.Data.Entities;

namespace CSCTest.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> UserRepository { get; }
        IRepository<Organization> OrganizationRepository { get; }
        IRepository<Country> CountryRepository { get; }
        IRepository<Business> BussinessRepository { get; }
        IRepository<Family> FamilyRepository { get; }
        IRepository<Offering> OfferingRepository { get; }
        IRepository<Department> DepartmentRepository { get; }

        IRepository<CountryBusiness> CountryBusinessRepository { get; }
        IRepository<BusinessFamily> BusinessFamilyRepository { get; }
        IRepository<FamilyOffering> FamilyOfferingRepository { get; }

        void Save();
        Task SaveAsync();
    }
}