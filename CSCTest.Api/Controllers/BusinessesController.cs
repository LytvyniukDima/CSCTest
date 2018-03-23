using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Businesses;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Businesses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Authorize]
    [Route("api/businesses")]
    public class BusinessesController : Controller
    {
        private readonly IBusinessService businessService;
        private readonly IMapper mapper;

        public BusinessesController(IBusinessService businessService, IMapper mapper)
        {
            this.businessService = businessService;
            this.mapper = mapper;
        }

        [HttpPost("~/api/countries/{countryId}/businesses")]
        public async Task<IActionResult> Post(int countryId, [FromBody]string businessName)
        {
            string email = User.Identity.Name;

            await businessService.AddBusiness(countryId, businessName, email);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var business = await businessService.GetBusinessAsync(id);
            if (business == null)
                return NotFound();

            var businessViewModel = mapper.Map<BusinessDto, BusinessViewModel>(business);
            return Ok(businessViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var businesses = await businessService.GetBusinessesAsync();
            var businessViewModels = mapper.Map<IEnumerable<BusinessDto>, IEnumerable<BusinessViewModel>>(businesses);

            return Ok(businessViewModels);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await businessService.DeleteBusinessAsync(id, email);
            return Ok();
        }
    }
}