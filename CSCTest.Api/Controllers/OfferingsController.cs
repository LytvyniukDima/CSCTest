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

        [HttpPost("~/api/families/{familyId}/offerings")]
        public async Task<IActionResult> Post(int familyId, [FromBody]string offeringName)
        {
            string email = User.Identity.Name;

            await offeringService.AddOfferingAsync(familyId, offeringName, email);
            return Ok();
        }

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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<OfferingDto> offerings = await offeringService.GetOferringsAsync();
            
            var offeringViewModels = mapper.Map<IEnumerable<OfferingDto>, IEnumerable<OfferingViewModel>>(offerings);
            return Ok(offeringViewModels);
        }

        [AllowAnonymous]
        [HttpGet("~/api/families/{familyId}/offerings")]
        public async Task<IActionResult> GetFamilyOfferings(int familyId)
        {
            IEnumerable<OfferingDto> offerings = offeringService.GetFamilyOfferings(familyId);
            
            var offeringViewModels = mapper.Map<IEnumerable<OfferingDto>, IEnumerable<OfferingViewModel>>(offerings);
            return Ok(offeringViewModels);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await offeringService.DeleteOfferingAsync(id, email);
            return Ok();
        }
    }
}