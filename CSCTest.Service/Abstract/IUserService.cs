using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IUserService
    {
        void AddUser(UserRegistrationDto userRegistrationDto);
    }
}