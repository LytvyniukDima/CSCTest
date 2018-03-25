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

        /// <summary>Get all types of offerings</summary>
        /// <returns>Array with information about all types of offerings</returns>
        /// <response code="200">Get type of offering successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var offeringTypes = await offeringService.GetOfferingTypesAsync();

            var offerings = mapper.Map<IEnumerable<OfferingTypeDto>, IEnumerable<OfferingTypeViewModel>>(offeringTypes);
            return Ok(offerings);
        }

        /// <summary>Get type of offering by id</summary>
        /// <returns>Information about type of offering</returns>
        /// <param name="id">Id of type of offering</param>
        /// <response code="200">Get type of offering successful</response>
        /// <response code="404">Not found type of offering with this id</response>
        /// <response code="500">Internal Server Error</response> 
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

        /// <summary>Create a new type of offering</summary>
        /// <remarks>
        /// Create a new type of offering which depend on concrete type of family
        /// </remarks>
        /// <returns>an IActionResult</returns>
        /// <param name="familyTypeId">Id of type of family on which depend new type of offering</param>
        /// <param name="name">Name of offering type</param> 
        /// <response code="200">Type of offering created successful</response>
        /// <response code="400">Type of offering with the same name and which depended on the same type of family already exist</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found type of family</response>
        /// <response code="500">Internal Server Error</response> 
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
        
        /// <summary>Update type of offering by id</summary>
        /// <returns>an IActionResult</returns>
        /// <param name="id">Id of type of offering</param>
        /// <param name="name">New name for type of offering</param>
        /// <response code="200">Changed type of offering successful</response>
        /// <response code="400">Type of offering with the same name and which depended on the same type of family already exist</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found type of offering</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            await offeringService.UpdateOfferingTypeAsync(id, name);

            return Ok();
        }
    }
}