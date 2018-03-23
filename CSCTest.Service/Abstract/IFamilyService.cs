using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs.Families;

namespace CSCTest.Service.Abstract
{
    public interface IFamilyService
    {
        Task AddFamilyAsync(int countryBusinessId, string name, string email);
        Task<FamilyDto> GetFamilyAsync(int businessFamilyId);
        Task<IEnumerable<FamilyDto>> GetFamiliesAsync();
        IEnumerable<FamilyDto> GetBusinessFamilies(int countryBusinessId);
        Task DeleteFamilyAsync(int id, string email);        

        Task AddFamilyTypeAsync(FamilyTypeCreateDto familyCreateDto);
        Task<FamilyTypeDto> GetFamilyTypeAsync(int id);
        Task<IEnumerable<FamilyTypeDto>> GetFamilyTypesAsync();
        Task UpdateFamilyTypeAsync(int id, string name);
    }
}