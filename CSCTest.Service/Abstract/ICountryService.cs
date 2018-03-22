using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface ICountryService
    {
        Task AddCountryAsync(int organizationId, CreateCountryDto createCountryDto, string email);
        Task<CountryDto> GetCountryAsync(int id);
        Task<IEnumerable<CountryDto>> GetCountriesAsync();
        Task UpdateCountryAsync(int id, CreateCountryDto createCountryDto, string email);
        Task DeleteCountryAsync(int id, string email);
    }
}