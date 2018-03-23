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
    [Route("/api/offring_types")]
    public class OfferingTypesController : Controller
    {
        private readonly IOfferingService offeringService;
        private readonly IMapper mapper;

        public OfferingTypesController(IOfferingService offeringService, IMapper mapper)
        {
            this.offeringService = offeringService;
            this.mapper = mapper;
        }

        [HttpPost("~/api/family_types/{familyTypeId}/offering_types")]
        public async Task<IActionResult> Post(int familyTypeId, string name)
        {
            await offeringService.AddOfferingTypeAsync(new OfferingTypeCreateDto
            {
                Name = name,
                FamilyTypeId = familyTypeId
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var offeringTypes = await offeringService.GetOfferingTypesAsync();

            var offerings = mapper.Map<IEnumerable<OfferingTypeDto>, IEnumerable<OfferingTypeViewModel>>(offeringTypes);
            return Ok(offerings);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var offeringType = await offeringService.GetOfferingTypeAsync(id);
            if (offeringType == null)
                return NotFound();
            
            var offering = mapper.Map<OfferingTypeDto, OfferingTypeViewModel>(offeringType);
            return Ok(offering);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            await offeringService.UpdateOfferingTypeAsync(id, name);

            return Ok();
        }
    }
}