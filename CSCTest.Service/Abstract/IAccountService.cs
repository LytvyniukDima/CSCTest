using System.Threading.Tasks;
using CSCTest.Service.DTOs.Users;

namespace CSCTest.Service.Abstract
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<string> CreateJwtTokenAsync(string email, string password);
    }
}