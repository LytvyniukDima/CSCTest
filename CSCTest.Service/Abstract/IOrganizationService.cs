using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IOrganizationService
    {
        void AddOrganization(OrganizationDto organizationDto, string email);
        void DeleteOrganization(string code);
    }
}