using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;

namespace CSCTest.Service.Concrete
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task AddCountryAsync(int organizationId, CreateCountryDto createCountryDto, string email)
        {
            using (unitOfWork)
            {
                var organizationRepository = unitOfWork.OrganizationRepository;
                var countryRepository = unitOfWork.CountryRepository;

                var organization = await organizationRepository.FindAsync(x => x.Id == organizationId && x.User.Email == email);
                if (organization == null)
                    return;
                
                var country = mapper.Map<CreateCountryDto, Country>(createCountryDto);
                country.Organization = organization;
                countryRepository.Add(country);

                unitOfWork.Save();
            }
        }

        public async Task<CountryDto> GetCountryAsync(int id)
        {
            using (unitOfWork)
            {
                var countryRepository = unitOfWork.CountryRepository;

                var country = await countryRepository.FindAsync(x => x.Id == id);
                if (country == null)
                    return null;
                
                var countryDto = mapper.Map<Country, CountryDto>(country);

                return countryDto;
            }
        }

        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            using (unitOfWork)
            {
                var countryRepository = unitOfWork.CountryRepository;

                var countries = await countryRepository.GetAllAsync();
                var countryDtos = mapper.Map<IEnumerable<Country>, IEnumerable<CountryDto>>(countries);

                return countryDtos;
            }
        }

        public async Task UpdateCountryAsync(int id, CreateCountryDto createCountryDto, string email)
        {
            using (unitOfWork)
            {
                var countryRepository = unitOfWork.CountryRepository;
                
                var country = await countryRepository.FindAsync(x => x.Id == id && x.Organization.User.Email == email);
                if (country == null)
                    return;
                
                mapper.Map<CreateCountryDto, Country>(createCountryDto, country);
                countryRepository.Update(country);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteCountryAsync(int id, string email)
        {
            using (unitOfWork)
            {
                var countryRepository = unitOfWork.CountryRepository;
                
                var country = await countryRepository.FindAsync(x => x.Id == id && x.Organization.User.Email == email);
                if (country == null)
                    return;

                countryRepository.Delete(country);
                await unitOfWork.SaveAsync();
            }
        }
    }
}