using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IAccountService
    {
        void RegisterUser(UserRegistrationDto userRegistrationDto);
        string CreateJwtToken(string email, string password);
    }
}