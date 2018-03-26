using AutoMapper;
using CSCTest.DAL.EF;
using CSCTest.Data.Entities;
using CSCTest.Tests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CSCTest.Tests.UnitTests.Services
{
    public class CountryServiceUnitTests
    {
        private readonly DbContextOptions dbOptions;
        private readonly IMapper mapper;
        private const string userEmail = "test@gmail.com";
        private int organizationConstId;
        public CountryServiceUnitTests()
        {
            this.dbOptions = new DbContextOptionsBuilder<CSCDbContext>()
                .UseInMemoryDatabase(databaseName: "CSCTest_Tests_CountryService")
                .Options;

            this.mapper = new MapperConfiguration(x => x.AddProfile(new MappingProfile())).CreateMapper();
            this.InitializeDb();
        }

        [Fact]
        public async void AddCountryAsync_CorrectParameters_SuccessfulAddCountry()
        {
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {

            }
        }

        [Fact]
        public async void AddCountryAsync_IncorrectOrganizationId_SuccessfulAddCountry()
        {
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {

            }
        }

        private void InitializeDb()
        {
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var userRepository = unitOfWork.UserRepository;
                var organizationRepository = unitOfWork.OrganizationRepository;
                var user = userRepository.Find(x => x.Email == userEmail);
                if (user == null)
                {
                    user = new User { Name = "Test", Surname = "Passed", Email = userEmail, Password = "pass" };
                    userRepository.Add(user);
                    var organization = new Organization { Name = "Unique Organization", Code = "UniOrg", Type = OrganizationType.LimitedLiabilityCompany, User = user };
                    organizationRepository.Add(organization);
                    organizationConstId = organization.Id;
                    unitOfWork.Save();
                }
            }
        }
    }
}