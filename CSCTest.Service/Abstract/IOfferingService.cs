using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs.Offerings;

namespace CSCTest.Service.Abstract
{
    public interface IOfferingService
    {
        Task AddOfferingAsync(int businessFamilyId, string name, string email);
        Task<OfferingDto> GetOfferingAsync(int familyOfferingId);
        Task<IEnumerable<OfferingDto>> GetOferringsAsync();
        Task DeleteOfferingAsync(int id, string email);        

        Task AddOfferingTypeAsync(OfferingTypeCreateDto offeringCreateDto);
        Task<OfferingTypeDto> GetOfferingTypeAsync(int id);
        Task<IEnumerable<OfferingTypeDto>> GetOfferingTypesAsync();
        Task UpdateOfferingTypeAsync(int id, string name);
    }
}