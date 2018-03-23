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

        [HttpPost("~/api/businesses/{businessId}/families")]
        public async Task<IActionResult> Post(int businessId, [FromBody]string familyName)
        {
            string email = User.Identity.Name;

            await familyService.AddFamilyAsync(businessId, familyName, email);
            return Ok();
        }

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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<FamilyDto> families = await familyService.GetFamiliesAsync();
            var familyViewModels = mapper.Map<IEnumerable<FamilyDto>, IEnumerable<FamilyViewModel>>(families);
            
            return Ok(familyViewModels); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await familyService.DeleteFamilyAsync(id, email);
            return Ok();
        }
    }
}