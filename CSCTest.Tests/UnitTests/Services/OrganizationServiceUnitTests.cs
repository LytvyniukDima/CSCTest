using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CSCTest.DAL.EF;
using CSCTest.Service.Concrete;
using CSCTest.Tests.Infrastructure;
using Xunit;
using CSCTest.Data.Entities;
using CSCTest.Service.DTOs.Organizations;
using CSCTest.Tools.Extensions;

namespace CSCTest.Tests.UnitTests.Services
{
    public class OrganizationServiceUnitTests
    {
        private readonly DbContextOptions dbOptions;
        private readonly IMapper mapper;
        private const string userEmail = "test@gmail.com";

        public OrganizationServiceUnitTests()
        {
            this.dbOptions = new DbContextOptionsBuilder<CSCDbContext>()
                .UseInMemoryDatabase(databaseName: "CSCTest_Tests_OrganizationService")
                .Options;
            Mapper.Initialize(x => x.AddProfile(new MappingProfile()));
            this.mapper = Mapper.Instance;
            this.InitializeDb();
        }

        [Fact]
        public async void AddOrganizationAsync_CorrectParameters_CreateNewOrganization()
        {
            // Arrange
            var organizationType = OrganizationType.LimitedPartnership;
            var organizationDto = new OrganizationDto
            {
                Name = "Organization",
                Code = "Org",
                Type = organizationType.GetStringName()

            };
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            await organizationService.AddOrganizationAsync(organizationDto, userEmail);

            // Assert
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var organization = organizationRepository.Find(x => x.Code == organizationDto.Code);
                Assert.Equal(organizationDto.Name, organization.Name);
                Assert.Equal(organizationDto.Code, organization.Code);
                Assert.Equal(organizationType, organization.Type);
                Assert.Equal(userEmail, organization.User.Email);
            }
            Mapper.Reset();
        }

        [Fact]
        public async void AddOrganizationAsync_ParametersWithCountryCodeThatExist_NotCreateNewOrganization()
        {
            // Arrange
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                unitOfWork.OrganizationRepository.Add(new Organization
                {
                    Name = "Unique Organization",
                    Code = "UniqueOrg",
                    Type = OrganizationType.IncorporatedCompany,
                    User = user
                });
                unitOfWork.Save();
            }
            var organizationDto = new OrganizationDto
            {
                Name = "Ununique Organization",
                Code = "UniqueOrg",
                Type = OrganizationType.LimitedLiabilityCompany.GetStringName()
            };
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            await organizationService.AddOrganizationAsync(organizationDto, userEmail);

            // Assert
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var organization = organizationRepository.Find(x => x.Code == organizationDto.Code && x.Name == organizationDto.Name);
                Assert.Null(organization);
            }
            Mapper.Reset();
        }

        private void InitializeDb()
        {
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var userRepository = unitOfWork.UserRepository;
                var user = userRepository.Find(x => x.Email == userEmail);
                if (user == null)
                {
                    userRepository.Add(new User
                    {
                        Name = "Test",
                        Surname = "Passed",
                        Email = userEmail,
                        Password = "pass"
                    });
                    unitOfWork.Save();
                }
            }
        }
    }
}