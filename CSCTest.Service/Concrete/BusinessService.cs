using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Businesses;

namespace CSCTest.Service.Concrete
{
    public class BusinessService : IBusinessService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BusinessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddBusiness(int countryId, string name, string email)
        {
            using (unitOfWork)
            {
                var businessRepository = unitOfWork.BussinessRepository;
                var countryBusinessRepository = unitOfWork.CountryBusinessRepository;
                var countryRepository = unitOfWork.CountryRepository;

                var country = await countryRepository.FindAsync(x => x.Id == countryId && x.Organization.User.Email == email);
                if (country == null)
                    return;

                var business = await businessRepository.FindAsync(x => x.Name == name);
                if (business == null)
                {
                    business = new Business { Name = name };
                    businessRepository.Add(business);
                }

                countryBusinessRepository.Add(new CountryBusiness
                {
                    Country = country,
                    Business = business
                });

                await unitOfWork.SaveAsync();
            }
        }

        public async Task AddBusinessTypeAsync(string name)
        {
            using (unitOfWork)
            {
                var businessRepository = unitOfWork.BussinessRepository;

                var business = await businessRepository.FindAsync(x => x.Name == name);
                if (business == null)
                {
                    business = new Business { Name = name };
                    businessRepository.Add(business);
                }

                await unitOfWork.SaveAsync();
            }
        }

        public async Task<BusinessTypeDto> GetBusinessTypeAsync(int id)
        {
            using (unitOfWork)
            {
                var businessRepository = unitOfWork.BussinessRepository;

                var business = await businessRepository.FindAsync(x => x.Id == id);
                if (business == null)
                    return null;

                return mapper.Map<Business, BusinessTypeDto>(business);
            }
        }

        public async Task<IEnumerable<BusinessTypeDto>> GetBusinessTypesAsync()
        {
            using (unitOfWork)
            {
                var businessRepository = unitOfWork.BussinessRepository;

                var businesses = await businessRepository.GetAllAsync();
                if (businesses == null)
                    return null;

                return mapper.Map<IEnumerable<Business>, IEnumerable<BusinessTypeDto>>(businesses);
            }
        }

        public async Task UpdateBusinessTypeAsync(int id, string name)
        {
            using (unitOfWork)
            {
                var businessRepository = unitOfWork.BussinessRepository;

                var business = await businessRepository.FindAsync(x => x.Id == id);
                if (business == null)
                    return;
                business.Name = name;

                businessRepository.Update(business);
                await unitOfWork.SaveAsync();
            }
        }
    }
}