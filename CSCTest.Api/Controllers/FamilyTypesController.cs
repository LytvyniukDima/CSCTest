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
    [Route("/api/family_types")]
    public class FamilyTypesController : Controller
    {
        private readonly IFamilyService familyService;
        private readonly IMapper mapper;

        public FamilyTypesController(IFamilyService familyService, IMapper mapper)
        {
            this.familyService = familyService;
            this.mapper = mapper;
        }
        
        /// <summary>Get all types of families</summary>
        /// <returns>Array with information about all types of families</returns>
        /// <response code="200">Get type of family successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var familyTypes = await familyService.GetFamilyTypesAsync();

            var families = mapper.Map<IEnumerable<FamilyTypeDto>, IEnumerable<FamilyTypeViewModel>>(familyTypes);
            return Ok(families);
        }

        /// <summary>Get type of family by id</summary>
        /// <returns>Information about type of family</returns>
        /// <param name="id">Id of type of family</param>
        /// <response code="200">Get type of family successful</response>
        /// <response code="404">Not found type of family with this id</response>
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var familyType = await familyService.GetFamilyTypeAsync(id);
            if (familyType == null)
                return NotFound();
            
            var family = mapper.Map<FamilyTypeDto, FamilyTypeViewModel>(familyType);
            return Ok(family);
        }
        
        /// <summary>Create a new type of family</summary>
        /// <remarks>
        /// Create a new type of family which depend on concrete type of business
        /// </remarks>
        /// <returns>an IActionResult</returns>
        /// <param name="businessTypeId">Id of business type on which depend new type of family</param>
        /// <param name="name">Name of family type</param> 
        /// <response code="200">Type of family created successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("~/api/business_types/{businessTypeId}/family_types")]
        public async Task<IActionResult> Post(int businessTypeId, [FromBody]string name)
        {
            await familyService.AddFamilyTypeAsync(new FamilyTypeCreateDto { 
                Name = name,
                BussinesTypeId = businessTypeId
            });

            return Ok();
        }

        /// <summary>Update type of family by id</summary>
        /// <returns>an IActionResult</returns>
        /// <param name="id">Id of type of family</param>
        /// <param name="name">New name for type of family</param>
        /// <response code="200">Changed type of family successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            await familyService.UpdateFamilyTypeAsync(id, name);
            
            return Ok();
        }
    }
}