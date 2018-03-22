using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Organizations;
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
        private readonly IMapper mapper;
        
        public OrganizationsController(IOrganizationService organizationService, IMapper mapper)
        {
            this.organizationService = organizationService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var organizations = await organizationService.GetOrganizationsAsync();

            var organizationViewModels = mapper.Map<IEnumerable<OrganizationDto>, IEnumerable<OrganizationViewModel>>(organizations);

            return Ok(organizationViewModels);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var organization = organizationService.GetOrganization(id);
            if (organization == null)
                return NotFound();

            var organizationViewModel = mapper.Map<OrganizationDto, OrganizationViewModel>(organization);

            return Ok(organizationViewModel);
        }

        [HttpPost]
        public void Post([FromBody]CreateOrganizationModel createOrganizationModel)
        {
            string email = User.Identity.Name;

            organizationService.AddOrganization(
                mapper.Map<CreateOrganizationModel, OrganizationDto>(createOrganizationModel),
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
            string email = User.Identity.Name;
            organizationService.DeleteOrganization(id, email);
        }
    }
}