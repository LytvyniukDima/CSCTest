using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using CSCTest.Tools.Extensions;
using AutoMapper;

namespace CSCTest.Service.Concrete
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            Mapper.Initialize(cfg => cfg.CreateMap<OrganizationDTO, Organization>()
                .ForMember("Type", opt => opt.MapFrom(od => od.Type.GetOrganizationType())));
        }

        public void AddOrganization(OrganizationDTO organizationDTO)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                var user = userRepository.Find(x => x.Name == "One");

                var organization = Mapper.Map<OrganizationDTO, Organization>(organizationDTO);
                organization.User = user;
                organizationRepository.Add(organization);

                unitOfWork.Save();
            }
        }

        public void DeleteOrganization(string organizationCode)
        {

        }
    }
}