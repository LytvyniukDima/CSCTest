using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Offerings;

namespace CSCTest.Service.Concrete
{
    public class OfferingService : IOfferingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OfferingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public async Task AddOfferingAsync(int businessFamilyId, string name, string email)
        {
            using (unitOfWork)
            {
                var businessFamilyRepository = unitOfWork.BusinessFamilyRepository;
                var offeringRepository = unitOfWork.OfferingRepository;
                var familyOfferingRepository = unitOfWork.FamilyOfferingRepository;

                var businessFamily = await businessFamilyRepository.FindAsync(x => 
                    x.Id == businessFamilyId &&
                    x.CountryBusiness.Country.Organization.User.Email == email
                );
                if (businessFamily == null)
                    return;
                
                var offering = await offeringRepository.FindAsync(x =>
                    x.Name == name &&
                    x.FamilyId == businessFamily.FamilyId
                );
                if (offering == null)
                {
                    offering = new Offering
                    {
                        Name = name,
                        Family = businessFamily.Family
                    };
                }

                familyOfferingRepository.Add(new FamilyOffering
                {
                    Offering = offering,
                    BusinessFamily = businessFamily
                });
                await unitOfWork.SaveAsync();
            }
        }

        public async Task AddOfferingTypeAsync(OfferingTypeCreateDto offeringCreateDto)
        {
            using (unitOfWork)
            {
                var familyRepository = unitOfWork.FamilyRepository;
                var offeringRepository = unitOfWork.OfferingRepository;

                var family = await familyRepository.FindAsync(x => x.Id == offeringCreateDto.FamilyTypeId);
                if (family == null)
                    return;
                
                offeringRepository.Add(new Offering
                {
                    Name = offeringCreateDto.Name,
                    Family = family
                });
                await unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteOfferingAsync(int id, string email)
        {
            using (unitOfWork)
            {
                var familyOfferingRepository = unitOfWork.FamilyOfferingRepository;

                var familyOffering = await familyOfferingRepository.FindAsync(x => 
                    x.Id == id &&
                    x.BusinessFamily.CountryBusiness.Country.Organization.User.Email == email
                );
                if (familyOffering == null)
                    return;
                
                familyOfferingRepository.Delete(familyOffering);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<OfferingDto>> GetOferringsAsync()
        {
            using(unitOfWork)
            {
                var familyOfferingRepository = unitOfWork.FamilyOfferingRepository;
                var familyOfferings= await familyOfferingRepository.GetAllAsync();
                return mapper.Map<IEnumerable<FamilyOffering>, IEnumerable<OfferingDto>>(familyOfferings);
            }
        }

        public async Task<OfferingDto> GetOfferingAsync(int familyOfferingId)
        {
            using (unitOfWork)
            {
                var familyOfferingRepository = unitOfWork.FamilyOfferingRepository;

                var familyOffering = await familyOfferingRepository.FindAsync(x => x.Id == familyOfferingId);
                if (familyOffering == null)
                    return null;
                return mapper.Map<FamilyOffering, OfferingDto>(familyOffering);
            }
        }

        public async Task<OfferingTypeDto> GetOfferingTypeAsync(int id)
        {
            using (unitOfWork)
            {
                var offeringRepository = unitOfWork.OfferingRepository;

                var offering = await offeringRepository.FindAsync(x => x.Id == id);
                if (offering == null)
                    return null;
                
                return mapper.Map<Offering, OfferingTypeDto>(offering);
            }
        }

        public async Task<IEnumerable<OfferingTypeDto>> GetOfferingTypesAsync()
        {
            using (unitOfWork)
            {
                var offeringRepository = unitOfWork.OfferingRepository;

                var offerings = await offeringRepository.GetAllAsync();
                
                return mapper.Map<IEnumerable<Offering>, IEnumerable<OfferingTypeDto>>(offerings);
            }
        }

        public async Task UpdateOfferingTypeAsync(int id, string name)
        {
            using (unitOfWork)
            {
                var offeringRepository = unitOfWork.OfferingRepository;

                var offering = await offeringRepository.FindAsync(x => x.Id == id);
                if (offering == null)
                    return;
                
                offering.Name = name;
                offeringRepository.Update(offering);
                await unitOfWork.SaveAsync();
            }
        }
    }
}