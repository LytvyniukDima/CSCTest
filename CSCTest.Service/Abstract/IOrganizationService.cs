using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IOrganizationService
    {
        void AddOrganization(OrganizationDto organizationDto, string email);
        void DeleteOrganization(int id, string email);
        OrganizationDto GetOrganization(int id);
        Task<IEnumerable<OrganizationDto>> GetOrganizationsAsync();
        void Update(int id, OrganizationDto organizationDto, string email);
    }
}