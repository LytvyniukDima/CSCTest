using System.Collections.Generic;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Route("api/organizations")]
    [Authorize]
    public class OrganizationsController : Controller
    {
        private readonly IOrganizationService organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        // GET api/values
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

        [HttpPost]
        public void Post([FromBody]string value)
        {
            string email = User.Identity.Name;

            organizationService.AddOrganization(new OrganizationDto
            {
                Name = "Bla",
                Code = "US",
                Type = "Incorporated company"
            },
            email);
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
            organizationService.DeleteOrganization("bal");
        }
    }
}