using System;
using System.Threading.Tasks;
using CSCTest.DAL.Repositories;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCTest.DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly CSCDbContext dbContext;

        private UserRepository userRepository;
        private OrganizationRepository organizationRepository;
        private CountryRepository countryRepository;
        private BusinessRepository businessRepository;
        private FamilyRepository familyRepository;
        private OfferingRepository offeringRepository;
        private DepartmentRepository departmentRepository;

        private CountryBusinessRepository countryBusinessRepository;
        private BusinessFamilyRepository businessFamilyRepository;
        private FamilyOfferingRepository familyOfferingRepository;

        public EFUnitOfWork(DbContextOptions options)
        {
            dbContext = new CSCDbContext(options);
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(dbContext);
                }
                return userRepository;
            }
        }

        public IRepository<Organization> OrganizationRepository
        {
            get
            {
                if (organizationRepository == null)
                {
                    organizationRepository = new OrganizationRepository(dbContext);
                }
                return organizationRepository;
            }
        }

        public IRepository<Country> CountryRepository
        {
            get
            {
                if (countryRepository == null)
                {
                    countryRepository = new CountryRepository(dbContext);
                }
                return countryRepository;
            }
        }

        public IRepository<Business> BussinessRepository
        {
            get
            {
                if (businessRepository == null)
                {
                    businessRepository = new BusinessRepository(dbContext);
                }
                return businessRepository;
            }
        }

        public IRepository<Family> FamilyRepository
        {
            get
            {
                if (familyRepository == null)
                {
                    familyRepository = new FamilyRepository(dbContext);
                }
                return familyRepository;
            }
        }

        public IRepository<Offering> OfferingRepository
        {
            get
            {
                if (offeringRepository == null)
                {
                    offeringRepository = new OfferingRepository(dbContext);
                }
                return offeringRepository;
            }
        }

        public IRepository<Department> DepartmentRepository
        {
            get
            {
                if (departmentRepository == null)
                {
                    departmentRepository = new DepartmentRepository(dbContext);
                }
                return departmentRepository;
            }
        }

        public IRepository<CountryBusiness> CountryBusinessRepository
        {
            get
            {
                if (countryBusinessRepository == null)
                {
                    countryBusinessRepository = new CountryBusinessRepository(dbContext);
                }
                return countryBusinessRepository;
            }
        }

        public IRepository<BusinessFamily> BusinessFamilyRepository
        {
            get
            {
                if (businessFamilyRepository == null)
                {
                    businessFamilyRepository = new BusinessFamilyRepository(dbContext);
                }
                return businessFamilyRepository;
            }
        }

        public IRepository<FamilyOffering> FamilyOfferingRepository
        {
            get
            {
                if (familyOfferingRepository == null)
                {
                    familyOfferingRepository = new FamilyOfferingRepository(dbContext);
                }
                return familyOfferingRepository;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}