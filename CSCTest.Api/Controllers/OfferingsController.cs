using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Offerings;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Offerings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Authorize]
    [Route("api/offerings")]
    public class OfferingsController : Controller
    {
        private readonly IOfferingService offeringService;
        private readonly IMapper mapper;

        public OfferingsController(IOfferingService offeringService, IMapper mapper)
        {
            this.offeringService = offeringService;
            this.mapper = mapper;
        }

        /// <summary>Get all offerings</summary>
        /// <returns>Array with information about all offerings</returns>
        /// <response code="200">Get offerings successful</response> 
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<OfferingDto> offerings = await offeringService.GetOferringsAsync();
            
            var offeringViewModels = mapper.Map<IEnumerable<OfferingDto>, IEnumerable<OfferingViewModel>>(offerings);
            return Ok(offeringViewModels);
        }

        /// <summary>Get offering by id</summary>
        /// <returns>Information about offering</returns>
        /// <param name="id">Id of offering</param>
        /// <response code="200">Get offering successful</response>
        /// <response code="404">Not found offering with this id</response>
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            OfferingDto offering = await offeringService.GetOfferingAsync(id);
            if (offering == null)
                return NotFound();

            var offeringViewModel = mapper.Map<OfferingDto, OfferingViewModel>(offering);
            return Ok(offeringViewModel);
        }

        /// <summary>Get all offerings in concrete family</summary>
        /// <returns>Array with information about offerings in concrete family</returns>
        /// <param name="familyId">Id of family</param>
        /// <response code="200">Get offerings successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("~/api/families/{familyId}/offerings")]
        public IActionResult GetFamilyOfferings(int familyId)
        {
            IEnumerable<OfferingDto> offerings = offeringService.GetFamilyOfferings(familyId);
            
            var offeringViewModels = mapper.Map<IEnumerable<OfferingDto>, IEnumerable<OfferingViewModel>>(offerings);
            return Ok(offeringViewModels);
        }
        
        /// <summary>Create an offering inside family</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Create an offering iside family. If offering name doesn't exist in 
        /// offerings types, will be created new type of offering.
        /// Only owner of organization can create new Offering.
        /// </remarks>
        /// <param name="familyId">Family's id, where create new offering</param>
        /// <param name="offeringName">Name of type of offering</param> 
        /// <response code="200">Offering create successful</response>
        /// <response code="400">Offering already exist in family</response> 
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found family</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("~/api/families/{familyId}/offerings")]
        public async Task<IActionResult> Post(int familyId, [FromBody]string offeringName)
        {
            string email = User.Identity.Name;

            await offeringService.AddOfferingAsync(familyId, offeringName, email);
            return Ok();
        }

        /// <summary>Delete offering by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Delete Offering can only owner of organization
        /// </remarks>
        /// <param name="id">Id of offering</param>
        /// <response code="200">Delete successful</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found offering</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await offeringService.DeleteOfferingAsync(id, email);
            return Ok();
        }
    }
}