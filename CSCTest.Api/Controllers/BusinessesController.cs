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

        /// <summary>Get all businesses</summary>
        /// <returns>Array with information about all businesses</returns>
        /// <response code="200">Get businesses successful</response> 
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var businesses = await businessService.GetBusinessesAsync();
            var businessViewModels = mapper.Map<IEnumerable<BusinessDto>, IEnumerable<BusinessViewModel>>(businesses);

            return Ok(businessViewModels);
        }

        /// <summary>Get business by id</summary>
        /// <returns>Information about business</returns>
        /// <param name="id">Id of business</param>
        /// <response code="200">Get business successful</response>
        /// <response code="404">Not found business with this id</response>
        /// <response code="500">Internal Server Error</response> 
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

        /// <summary>Get all businesses in concrete country</summary>
        /// <returns>Array with information about businesses in concrete country</returns>
        /// <param name="countryId">Id of country</param>
        /// <response code="200">Get countries successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("~/api/countries/{countryId}/businesses")]
        public IActionResult GetCountryBusiness(int countryId)
        {
            var businesses = businessService.GetCountryBusinesses(countryId);
            var businessViewModels = mapper.Map<IEnumerable<BusinessDto>, IEnumerable<BusinessViewModel>>(businesses);

            return Ok(businessViewModels);
        }
        
        /// <summary>Create a business inside country</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Create a business iside a country. If business name doesn't exist 
        /// in business types, will be created new business type.
        /// Only owner of organization can create new business.
        /// </remarks>
        /// <param name="countryId">Country's id, where create new business</param>
        /// <param name="businessName">Name of business type</param> 
        /// <response code="200">Business create successful</response>
        /// <response code="400">Business with the same name already exist in country</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found country with or user don't have permisson to manage this organization</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpPost("~/api/countries/{countryId}/businesses")]
        public async Task<IActionResult> Post(int countryId, [FromBody]string businessName)
        {
            string email = User.Identity.Name;

            await businessService.AddBusiness(countryId, businessName, email);
            return Ok();
        }

        /// <summary>Delete business by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Delete Business can only owner of organization
        /// </remarks>
        /// <param name="id">Id of business</param>
        /// <response code="200">Delete successful</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found business</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await businessService.DeleteBusinessAsync(id, email);
            return Ok();
        }
    }
}