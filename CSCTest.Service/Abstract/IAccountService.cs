using System.Threading.Tasks;
using CSCTest.Service.DTOs;

namespace CSCTest.Service.Abstract
{
    public interface IAccountService
    {
        Task RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<string> CreateJwtTokenAsync(string email, string password);
    }
}