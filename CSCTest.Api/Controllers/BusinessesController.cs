using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Service.Abstract;
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

            await businessService.AddBusiness(
                countryId,
                businessName,
                email
            );

            return Ok();
        }
    }
}