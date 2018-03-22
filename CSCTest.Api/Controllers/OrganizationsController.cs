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
    [Authorize]
    [Route("api/organizations")]
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
        public async Task<IActionResult> Get(int id)
        {
            var organization = await organizationService.GetOrganizationAsync(id);
            if (organization == null)
                return NotFound();

            var organizationViewModel = mapper.Map<OrganizationDto, OrganizationViewModel>(organization);

            return Ok(organizationViewModel);
        }

        [HttpPost]
        public async Task <IActionResult> Post([FromBody]CreateOrganizationModel createOrganizationModel)
        {
            string email = User.Identity.Name;

            await organizationService.AddOrganizationAsync(
                mapper.Map<CreateOrganizationModel, OrganizationDto>(createOrganizationModel),
                email
            );

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CreateOrganizationModel createOrganizationModel)
        {
            string email = User.Identity.Name;

            await organizationService.UpdateAsync(
                id,
                mapper.Map<CreateOrganizationModel, OrganizationDto>(createOrganizationModel),
                email
            );
            
            return Ok();
        }

        /// <summary>
        /// Delete Country
        /// </summary>
        /// <remarks>Delete country by id.</remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;
            await organizationService.DeleteOrganizationAsync(id, email);

            return Ok();
        }
    }
}