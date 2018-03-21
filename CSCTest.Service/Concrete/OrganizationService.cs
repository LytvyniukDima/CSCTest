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
        private readonly IMapper mapper;
        
        public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public void AddOrganization(OrganizationDto organizationDTO)
        {       
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                userRepository.Add(new User { Name = "One" });
                unitOfWork.Save();

                var user = userRepository.Find(x => x.Name == "One");

                var organization = mapper.Map<OrganizationDto, Organization>(organizationDTO);
                organization.User = user;
                organizationRepository.Add(organization);

                unitOfWork.Save();
            }
        }

        public void DeleteOrganization(string code)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OfferingRepository;
                var userRepository = unitOfWork.UserRepository;

                var user = userRepository.Find(x => x.Name == "One");

                userRepository.Delete(user);

                unitOfWork.Save();
            }
        }
    }
}