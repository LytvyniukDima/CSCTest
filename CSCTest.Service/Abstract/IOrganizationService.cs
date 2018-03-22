using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IOrganizationService
    {
        Task AddOrganizationAsync(OrganizationDto organizationDto, string email);
        Task DeleteOrganizationAsync(int id, string email);
        Task<OrganizationDto> GetOrganizationAsync(int id);
        Task<IEnumerable<OrganizationDto>> GetOrganizationsAsync();
        Task UpdateAsync(int id, OrganizationDto organizationDto, string email);
    }
}