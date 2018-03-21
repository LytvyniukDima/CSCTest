using AutoMapper;
using CSCTest.Api.Models;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        [HttpPost("token")]
        public IActionResult GetToken([FromBody] LoginCredentials loginCredentials)
        {
            string token = accountService.CreateJwtToken(loginCredentials.Email, loginCredentials.Password);
            if (token == null)
                return BadRequest("Invalid username or password.");

            return Ok(new { access_token = token});
        }

        [HttpPost("registration")]
        public IActionResult RegisterUser([FromBody] RegistrationUserCredentials registrationUserCredentials)
        {
            accountService.RegisterUser(mapper.Map<RegistrationUserCredentials, UserRegistrationDto>(registrationUserCredentials));
            
            string token = accountService.CreateJwtToken(registrationUserCredentials.Email, registrationUserCredentials.Password);
            if (token == null)
                return BadRequest("Invalid username or password.");

            return Ok(new { access_token = token});
        }
    }
}