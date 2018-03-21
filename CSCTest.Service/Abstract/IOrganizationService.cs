using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IOrganizationService
    {
        void AddOrganization(OrganizationDto organizationDTO);
        void DeleteOrganization(string code);
    }
}