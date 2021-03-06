using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using CSCTest.Tools.Extensions;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs.Organizations;
using CSCTest.Service.Infrastructure;
using CSCTest.DAL.Exceptions;

namespace CSCTest.Service.Concrete
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddOrganizationAsync(OrganizationDto organizationDTO, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                var user = await userRepository.FindAsync(x => x.Email == email);

                var organization = mapper.Map<OrganizationDto, Organization>(organizationDTO);
                organization.User = user;
                try
                {
                    organizationRepository.Add(organization);
                    await unitOfWork.SaveAsync();
                }
                catch (DALException ex)
                {
                    throw new HttpStatusCodeException(400, ex.Message);
                }
            }
        }

        public async Task DeleteOrganizationAsync(int id, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                var organization = await organizationRepository.FindAsync(x => x.Id == id && x.User.Email == email);

                if (organization == null)
                {
                    throw new HttpStatusCodeException(404, $"Not found organization with id \"{id}\" or user with email {email} don't have permisson to manage this organization");
                }

                organizationRepository.Delete(organization);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<OrganizationDto> GetOrganizationAsync(int id)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;

                var organization = await organizationRepository.FindAsync(x => x.Id == id);
                if (organization == null)
                    return null;

                var organizationDto = mapper.Map<Organization, OrganizationDto>(organization);

                return organizationDto;
            }
        }

        public async Task<IEnumerable<OrganizationDto>> GetOrganizationsAsync()
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;

                var organizations = await organizationRepository.GetAllAsync();
                var organizationDtos = mapper.Map<IEnumerable<Organization>, IEnumerable<OrganizationDto>>(organizations);

                return organizationDtos;
            }
        }
        public async Task UpdateAsync(int id, OrganizationDto organizationDto, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;

                var organization = await organizationRepository.FindAsync(x => x.Id == id && x.User.Email == email);
                if (organization == null)
                {
                    throw new HttpStatusCodeException(404, $"Not found organization with id \"{id}\" or user with email {email} don't have permisson to manage this organization");
                }

                mapper.Map<OrganizationDto, Organization>(organizationDto, organization);

                try
                {
                    organizationRepository.Update(organization);
                    await unitOfWork.SaveAsync();
                }
                catch (DALException ex)
                {
                    throw new HttpStatusCodeException(400, ex.Message);
                }
            }
        }
    }
}