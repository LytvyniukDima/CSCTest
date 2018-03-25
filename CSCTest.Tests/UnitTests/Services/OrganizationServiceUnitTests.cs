using Microsoft.EntityFrameworkCore;
using AutoMapper;
using CSCTest.DAL.EF;
using CSCTest.Service.Concrete;
using CSCTest.Tests.Infrastructure;
using Xunit;
using CSCTest.Data.Entities;
using CSCTest.Service.DTOs.Organizations;
using CSCTest.Tools.Extensions;
using System;
using CSCTest.Service.Infrastructure;
using CSCTest.DAL.Exceptions;
using System.Collections.Generic;

namespace CSCTest.Tests.UnitTests.Services
{
    public class OrganizationServiceUnitTests : IDisposable
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
        }

        [Fact]
        public async void AddOrganizationAsync_ParametersWithCountryCodeThatExist_ThrowsHttpStatusCodeException()
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

            // Act and Assert
            await Assert.ThrowsAsync<HttpStatusCodeException>(() => organizationService.AddOrganizationAsync(organizationDto, userEmail));
        }

        [Fact]
        public async void DeleteOrganizationAsync_CorrectParameters_SuccessDelete()
        {
            // Arrange
            int organizationId;
            var organizationCode = "S93";
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organization = new Organization
                {
                    Name = "Unique Organization",
                    Code = organizationCode,
                    Type = OrganizationType.IncorporatedCompany,
                    User = user
                };
                unitOfWork.OrganizationRepository.Add(organization);
                unitOfWork.Save();
                organizationId = organization.Id;
            }
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            await organizationService.DeleteOrganizationAsync(organizationId, userEmail);

            // Assert
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var organization = organizationRepository.Find(x => x.Code == organizationCode);
                Assert.Null(organization);
            }
        }

        [Fact]
        public async void DeleteOrganizationAsync_ParametersWithNotOwnerEmail_ThrowsHttpStatusCodeException()
        {
            // Arrange
            int organizationId;
            var organizationCode = "S123";
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organization = new Organization
                {
                    Name = "Unique Organization",
                    Code = organizationCode,
                    Type = OrganizationType.IncorporatedCompany,
                    User = user
                };
                unitOfWork.OrganizationRepository.Add(organization);
                unitOfWork.Save();
                organizationId = organization.Id;
            }
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act and Assert
            await Assert.ThrowsAsync<HttpStatusCodeException>(() => organizationService.DeleteOrganizationAsync(organizationId, "incorrect@gmail.com"));
        }

        [Fact]
        public async void GetOrganizationAsync_CorrectOrganizationId_ReturnOrganizationDto()
        {
            // Arrange
            int organizationId;
            var organizationCode = "S153";
            var organizationType = OrganizationType.LimitedPartnership;
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organization = new Organization
                {
                    Name = organizationCode,
                    Code = organizationCode,
                    Type = organizationType,
                    User = user
                };
                unitOfWork.OrganizationRepository.Add(organization);
                unitOfWork.Save();
                organizationId = organization.Id;
            }
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            var organizationDto = await organizationService.GetOrganizationAsync(organizationId);

            // Assert
            Assert.Equal(organizationCode, organizationDto.Code);
            Assert.Equal(organizationCode, organizationDto.Name);
            Assert.Equal(organizationType.GetStringName(), organizationDto.Type);
            Assert.False(organizationDto.HasChildren);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public async void GetOrganizationAsync_IncorrectOrganizationId_ReturnNull(int id)
        {
            // Arrange
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            var result = await organizationService.GetOrganizationAsync(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async void GetOrganizationsAsync_ReturnCollectionOfOrganizationDtos()
        {
            // Arrange
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organizations = new Organization[]
                {
                    new Organization {Name = "Unique Organization", Code = "S205", Type = OrganizationType.IncorporatedCompany, User = user},
                    new Organization {Name = "Unique Organization", Code = "S206", Type = OrganizationType.IncorporatedCompany, User = user},
                    new Organization {Name = "Unique Organization", Code = "S207", Type = OrganizationType.IncorporatedCompany, User = user},
                };
                unitOfWork.OrganizationRepository.AddRange(organizations);
                unitOfWork.Save();
            }
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            var organizationDtos = await organizationService.GetOrganizationsAsync();
            var organizationList = new List<OrganizationDto>(organizationDtos);

            // Assert
            Assert.True(organizationList.Count > 0);
        }

        [Fact]
        public async void UpdateAsync_CorrectParameters_SuccessfulUpdateOrganization()
        {
            // Arrange
            int organizationId;
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organization = new Organization
                {
                    Name = "Old Name",
                    Code = "S234",
                    Type = OrganizationType.GeneralPartnership,
                    User = user
                };
                unitOfWork.OrganizationRepository.Add(organization);
                unitOfWork.Save();
                organizationId = organization.Id;
            }
            var organizationDto = new OrganizationDto { Name = "Organization", Code = "S242", Type = OrganizationType.IncorporatedCompany.GetStringName() };
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act
            await organizationService.UpdateAsync(organizationId, organizationDto, userEmail);

            // Assert
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var organization = organizationRepository.Find(x => x.Id == organizationId);
                Assert.Equal(organizationDto.Name, organization.Name);
                Assert.Equal(organizationDto.Code, organization.Code);
                Assert.Equal(organizationDto.Type.GetOrganizationType(), organization.Type);
            }
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1000)]
        public async void UpdateAsync_IncorrectId_ThrowsHttpStatusCodeException(int id)
        {
            // Arrange
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);
            var organizationDto = new OrganizationDto { Name = "Organization", Code = "S266", Type = OrganizationType.IncorporatedCompany.GetStringName() };
            
            // Act and Assert
            await Assert.ThrowsAsync<HttpStatusCodeException>(() => organizationService.UpdateAsync(id, organizationDto, userEmail));
        }

        [Fact]
        public async void UpdateAsync_IncorrectEmail_ThrowsHttpStatusCodeException()
        {
           // Arrange
            int organizationId;
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organization = new Organization
                {
                    Name = "Old Name",
                    Code = "S285",
                    Type = OrganizationType.GeneralPartnership,
                    User = user
                };
                unitOfWork.OrganizationRepository.Add(organization);
                unitOfWork.Save();
                organizationId = organization.Id;
            }
            var organizationDto = new OrganizationDto { Name = "Organization", Code = "S242", Type = OrganizationType.IncorporatedCompany.GetStringName() };
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act and Assert
            await Assert.ThrowsAsync<HttpStatusCodeException>(() => organizationService.UpdateAsync(organizationId, organizationDto, "failed@gmail.com"));
        }

        [Fact]
        public async void UpdateAsync_IncorrectCodeThatAlreadyExist_ThrowsHttpStatusCodeException()
        {
            int organizationId;
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var user = unitOfWork.UserRepository.Find(x => x.Email == userEmail);
                var organization1 = new Organization
                {
                    Name = "Old Name",
                    Code = "S308",
                    Type = OrganizationType.GeneralPartnership,
                    User = user
                };
                var organization2 = new Organization
                {
                    Name = "Old Name",
                    Code = "S315",
                    Type = OrganizationType.GeneralPartnership,
                    User = user
                };
                unitOfWork.OrganizationRepository.Add(organization1);
                unitOfWork.OrganizationRepository.Add(organization2);
                unitOfWork.Save();
                organizationId = organization1.Id;
            }
            var organizationDto = new OrganizationDto { Name = "Organization", Code = "S315", Type = OrganizationType.IncorporatedCompany.GetStringName() };
            var organizationService = new OrganizationService(new EFUnitOfWork(dbOptions), mapper);

            // Act and Assert
            await Assert.ThrowsAsync<HttpStatusCodeException>(() => organizationService.UpdateAsync(organizationId, organizationDto, userEmail));
       
        }

        private void InitializeDb()
        {
            using (var unitOfWork = new EFUnitOfWork(dbOptions))
            {
                var userRepository = unitOfWork.UserRepository;
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

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}
