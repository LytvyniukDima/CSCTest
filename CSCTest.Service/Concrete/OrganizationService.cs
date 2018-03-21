using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTO;
using CSCTest.Tools.Extensions;

namespace CSCTest.Service.Concrete
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrganizationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddOrganization(OrganizationDTO organizationDTO)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var userRepository = unitOfWork.UserRepository;

                userRepository.Add(new User { Name = "One" });
                unitOfWork.Save();

                var user = userRepository.Find(x => x.Name == "One");

                organizationRepository.Add(new Organization
                {
                    Name = organizationDTO.Name,
                    Code = organizationDTO.Code,
                    Type = organizationDTO.Type.GetOrganizationType(),
                    User = user
                });

                unitOfWork.Save();
            }
        }
    }
}