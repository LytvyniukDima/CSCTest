using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Families;

namespace CSCTest.Service.Concrete
{
    public class FamilyService : IFamilyService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FamilyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddFamilyAsync(int countryBusinessId, string name, string email)
        {
            using (unitOfWork)
            {
                var countryBusinessRepository = unitOfWork.CountryBusinessRepository;
                var familyRepository = unitOfWork.FamilyRepository;
                var businessFamilyRepository = unitOfWork.BusinessFamilyRepository;

                var countryBusiness = await countryBusinessRepository.FindAsync(
                    x => x.Id == countryBusinessId &&
                    x.Country.Organization.User.Email == email
                );
                if (countryBusiness == null)
                    return;

                var family = await familyRepository.FindAsync(
                    x => x.Name == name &&
                    x.BusinessId == countryBusiness.BusinessId
                );
                if (family == null)
                {
                    family = new Family
                    {
                        Name = name,
                        Business = countryBusiness.Business
                    };
                }

                businessFamilyRepository.Add(new BusinessFamily
                {
                    Family = family,
                    CountryBusiness = countryBusiness
                });
                await unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteFamilyAsync(int id, string email)
        {
            using (unitOfWork)
            {
                var businessFamilyRepository = unitOfWork.BusinessFamilyRepository;

                var businessFamily = await businessFamilyRepository.FindAsync(
                    x => x.Id == id &&
                    x.CountryBusiness.Country.Organization.User.Email == email
                );
                if (businessFamily == null)
                    return;
                
                businessFamilyRepository.Delete(businessFamily);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<FamilyDto>> GetFamiliesAsync()
        {
            using (unitOfWork)
            {
                var businessFamilyRepository = unitOfWork.BusinessFamilyRepository;
                var businessFamilies = await businessFamilyRepository.GetAllAsync();
                return mapper.Map<IEnumerable<BusinessFamily>, IEnumerable<FamilyDto>>(businessFamilies);
            }
        }

        public async Task<FamilyDto> GetFamilyAsync(int businessFamilyId)
        {
            using (unitOfWork)
            {
                var businessFamilyRepository = unitOfWork.BusinessFamilyRepository;
                
                var businessFamily = await businessFamilyRepository.FindAsync(x => x.Id == businessFamilyId);
                if (businessFamily == null)
                    return null;
                return mapper.Map<BusinessFamily, FamilyDto>(businessFamily);
            }
        }
        
        public IEnumerable<FamilyDto> GetBusinessFamilies(int countryBusinessId)
        {
            using (unitOfWork)
            {
                var businessFamilyRepository = unitOfWork.BusinessFamilyRepository;
                var businessFamilies = businessFamilyRepository.FindAll(x => x.CountryBusinessId == countryBusinessId);
                return mapper.Map<IEnumerable<BusinessFamily>, IEnumerable<FamilyDto>>(businessFamilies);
            }
        }


        public async Task AddFamilyTypeAsync(FamilyTypeCreateDto familyCreateDto)
        {
            using (unitOfWork)
            {
                var businessRepository = unitOfWork.BussinessRepository;
                var familyRepository = unitOfWork.FamilyRepository;

                var business = await businessRepository.FindAsync(x => x.Id == familyCreateDto.BussinesTypeId);
                if (business == null)
                    return;

                familyRepository.Add(new Family
                {
                    Name = familyCreateDto.Name,
                    Business = business
                });

                await unitOfWork.SaveAsync();
            }
        }

        public async Task<FamilyTypeDto> GetFamilyTypeAsync(int id)
        {
            using (unitOfWork)
            {
                var familyRepository = unitOfWork.FamilyRepository;
                var family = await familyRepository.FindAsync(x => x.Id == id);
                if (family == null)
                    return null;

                return mapper.Map<Family, FamilyTypeDto>(family);
            }
        }

        public async Task<IEnumerable<FamilyTypeDto>> GetFamilyTypesAsync()
        {
            using (unitOfWork)
            {
                var familyRepository = unitOfWork.FamilyRepository;
                var families = await familyRepository.GetAllAsync();

                return mapper.Map<IEnumerable<Family>, IEnumerable<FamilyTypeDto>>(families);
            }
        }

        public async Task UpdateFamilyTypeAsync(int id, string name)
        {
            using (unitOfWork)
            {
                var familyRepository = unitOfWork.FamilyRepository;

                var family = await familyRepository.FindAsync(x => x.Id == id);
                if (family == null)
                    return;

                family.Name = name;
                familyRepository.Update(family);
                await unitOfWork.SaveAsync();
            }
        }
    }
}