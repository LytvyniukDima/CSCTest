using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Families;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Families;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Authorize]
    [Route("api/families")]
    public class FamiliesController : Controller
    {
        private readonly IFamilyService familyService;
        private readonly IMapper mapper;

        public FamiliesController(IFamilyService familyService, IMapper mapper)
        {
            this.familyService = familyService;
            this.mapper = mapper;
        }
        
        /// <summary>Get all families</summary>
        /// <returns>Array with information about all families</returns>
        /// <response code="200">Get families successful</response> 
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<FamilyDto> families = await familyService.GetFamiliesAsync();
            var familyViewModels = mapper.Map<IEnumerable<FamilyDto>, IEnumerable<FamilyViewModel>>(families);

            return Ok(familyViewModels);
        }

        /// <summary>Get family by id</summary>
        /// <returns>Information about family</returns>
        /// <param name="id">Id of family</param>
        /// <response code="200">Get family successful</response>
        /// <response code="404">Not found family with this id</response>
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            FamilyDto family = await familyService.GetFamilyAsync(id);
            if (family == null)
                return NotFound();

            var familyViewModel = mapper.Map<FamilyDto, FamilyViewModel>(family);
            return Ok(familyViewModel);
        }

        /// <summary>Get all families in concrete business</summary>
        /// <returns>Array with information about families in concrete business</returns>
        /// <param name="businessId">Id of business</param>
        /// <response code="200">Get families successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("~/api/businesses/{businessId}/families")]
        public IActionResult GetBusinessFamiles(int businessId)
        {
            IEnumerable<FamilyDto> families = familyService.GetBusinessFamilies(businessId);
            var familyViewModels = mapper.Map<IEnumerable<FamilyDto>, IEnumerable<FamilyViewModel>>(families);

            return Ok(familyViewModels);
        }
        
        /// <summary>Create a family inside business</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Create a family iside business. If family name doesn't exist in 
        /// families types, will be created new family type.
        /// Only owner of organization can create new family.
        /// </remarks>
        /// <param name="businessId">Business's id, where create new family</param>
        /// <param name="familyName">Name type of family</param> 
        /// <response code="200">Family create successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("~/api/businesses/{businessId}/families")]
        public async Task<IActionResult> Post(int businessId, [FromBody]string familyName)
        {
            string email = User.Identity.Name;

            await familyService.AddFamilyAsync(businessId, familyName, email);
            return Ok();
        }
        
        /// <summary>Delete family by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Delete Fmily can only owner of organization
        /// </remarks>
        /// <param name="id">Id of family</param>
        /// <response code="200">Delete successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await familyService.DeleteFamilyAsync(id, email);
            return Ok();
        }
    }
}