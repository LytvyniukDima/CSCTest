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

        [HttpPost("~/api/business_types/{businessTypeId}/family_types")]
        public async Task<IActionResult> Post(int businessTypeId, string name)
        {
            await familyService.AddFamilyTypeAsync(new FamilyTypeCreateDto { 
                Name = name,
                BussinesTypeId = businessTypeId
            });

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var familyTypes = await familyService.GetFamilyTypesAsync();

            var families = mapper.Map<IEnumerable<FamilyTypeDto>, IEnumerable<FamilyTypeViewModel>>(familyTypes);
            return Ok(families);
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            await familyService.UpdateFamilyTypeAsync(id, name);
            
            return Ok();
        }
    }
}