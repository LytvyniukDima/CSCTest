using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using CSCTest.Tools.Extensions;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public void AddOrganization(OrganizationDto organizationDTO, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                var user = userRepository.Find(x => x.Email == email);

                var organization = mapper.Map<OrganizationDto, Organization>(organizationDTO);
                organization.User = user;
                organizationRepository.Add(organization);

                unitOfWork.Save();
            }
        }

        public void DeleteOrganization(int id, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                var user = userRepository.Find(x => x.Email == email);
                var organization = organizationRepository.Find(x => x.Id == id && x.UserId == user.Id);

                if (organization != null)
                {
                    organizationRepository.Delete(organization);
                    unitOfWork.Save();
                }
            }
        }

        public OrganizationDto GetOrganization(int id)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;

                var organization = organizationRepository.Find(x => x.Id == id);
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
        public void Update(int id, OrganizationDto organizationDto, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;

                var organization = organizationRepository.Find(x => x.Id == id && x.User.Email == email);
                if (organization == null)
                    return;

                mapper.Map<OrganizationDto, Organization>(organizationDto, organization);
                organizationRepository.Update(organization);
                unitOfWork.Save();
            }
        }
    }
}