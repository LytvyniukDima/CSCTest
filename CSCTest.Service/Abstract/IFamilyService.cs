using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs.Families;

namespace CSCTest.Service.Abstract
{
    public interface IFamilyService
    {
        Task AddFamilyTypeAsync(FamilyTypeCreateDto familyCreateDto);
        Task<FamilyTypeDto> GetFamilyTypeAsync(int id);
        Task<IEnumerable<FamilyTypeDto>> GetFamilyTypesAsync();
        Task UpdateFamilyTypeAsync(int id, string name);
    }
}