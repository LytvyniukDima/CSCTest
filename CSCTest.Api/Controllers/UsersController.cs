using System.Collections.Generic;
using AutoMapper;
using CSCTest.Api.Models;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserService userService;
        
        public UsersController(IMapper mapper, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] RegistrationUserCredentials registrationUserCredentials)
        {
            userService.AddUser(mapper.Map<RegistrationUserCredentials, UserRegistrationDto>(registrationUserCredentials));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Delete Country
        /// </summary>
        /// <remarks>Delete country by id.</remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            
        }
    }
}