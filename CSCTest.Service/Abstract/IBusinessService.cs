using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs.Businesses;

namespace CSCTest.Service.Abstract
{
    public interface IBusinessService
    {
        Task AddBusiness(int countryId, string name, string email);
        

        Task AddBusinessTypeAsync(string name);
        Task<BusinessTypeDto> GetBusinessTypeAsync(int id);
        Task<IEnumerable<BusinessTypeDto>> GetBusinessTypesAsync();
        Task UpdateBusinessTypeAsync(int id, string name);

    }
}