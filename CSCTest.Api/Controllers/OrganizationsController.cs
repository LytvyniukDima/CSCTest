using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Organizations;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Organizations;
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

        /// <summary>Get all organizations</summary>
        /// <returns>Array with information about all organizations</returns>
        /// <response code="200">Get organizations successful</response> 
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var organizations = await organizationService.GetOrganizationsAsync();

            var organizationViewModels = mapper.Map<IEnumerable<OrganizationDto>, IEnumerable<OrganizationViewModel>>(organizations);

            return Ok(organizationViewModels);
        }

        /// <summary>Get organization by id</summary>
        /// <returns>Information about organization</returns>
        /// <param name="id">Id of orgnization</param>
        /// <response code="200">Get organization successful</response>
        /// <response code="404">Not found organization with this id</response>
        /// <response code="500">Internal Server Error</response> 
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

        /// <summary>Create new organization</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Create new organization and set user that create organization like owner of organization
        /// </remarks>
        /// <param name="createOrganizationModel">Create Organization Model</param>  
        /// <response code="200">Orgnanization created successful</response>
        /// <response code="400">Organization with the same name already exist</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>Update organizatiom by id</summary>
        /// <returns>an IActionResult</returns>
        ///  <remarks>
        /// Only owner of organization can update it.
        /// </remarks>
        /// <param name="id">Id of organization</param>
        /// <param name="createOrganizationModel">Changed information about organization</param>
        /// <response code="200">Changed organization successful</response>
        /// <response code="400">Organization with the same name already exist</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found organization</response>
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>Delete organization by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Delete Organization can only owner
        /// </remarks>
        /// <param name="id">Id of organization</param>
        /// <response code="200">Delete successful</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found organization</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;
            await organizationService.DeleteOrganizationAsync(id, email);

            return Ok();
        }
    }
}