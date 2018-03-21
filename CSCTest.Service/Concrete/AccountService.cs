using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using CSCTest.Service.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CSCTest.Service.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly AuthOptions authOptions;
        private readonly IMapper mapper;

        public AccountService(
            IUnitOfWork unitOfWork,
            IOptions<AuthOptions> globalOptions,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.authOptions = globalOptions.Value;
            this.mapper = mapper;
        }

        public string CreateJwtToken(string email, string password)
        {
            var claimsIdentity = GetIdentity(email, password);
            if (claimsIdentity == null)
                return null;

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: claimsIdentity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public void RegisterUser(UserRegistrationDto userRegistrationDto)
        {
            var userRepository = unitOfWork.UserRepository;
            var user = mapper.Map<UserRegistrationDto, User>(userRegistrationDto);
            userRepository.Add(user);
            unitOfWork.Save();
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            var userRepository = unitOfWork.UserRepository;

            var user = userRepository.Find(x => x.Email == email && x.Password == password);
            if (user == null)
                return null;

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "customer")
                };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }
    }
}