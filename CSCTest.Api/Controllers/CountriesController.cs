using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Countries;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Authorize]
    [Route("api/countries")]
    public class CountriesController : Controller
    {
        private readonly ICountryService countryService;
        private readonly IMapper mapper;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            this.countryService = countryService;
            this.mapper = mapper;
        }

        /// <summary>Get all countries</summary>
        /// <returns>Array with information about all countries</returns>
        /// <response code="200">Get countries successful</response> 
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await countryService.GetCountriesAsync();
            var countryViewModels = mapper.Map<IEnumerable<CountryDto>, IEnumerable<CountryViewModel>>(countries);

            return Ok(countryViewModels);
        }

        /// <summary>Get country by id</summary>
        /// <returns>Information about country</returns>
        /// <param name="id">Id of country</param>
        /// <response code="200">Get country successful</response>
        /// <response code="404">Not found country with this id</response>
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var country = await countryService.GetCountryAsync(id);
            if (country == null)
                return NotFound();

            var countryViewModel = mapper.Map<CountryDto, CountryViewModel>(country);
            return Ok(countryViewModel);
        }

        /// <summary>Get all countries in concrete organization</summary>
        /// <returns>Array with information about all countries in concrete organization</returns>
        /// <param name="organizationId">Id of country</param>
        /// <response code="200">Get countries successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("~/api/organizations/{organizationId}/countries")]
        public IActionResult GetOrganizationCountries(int organizationId)
        {
            var countries = countryService.GetOrganizationCountries(organizationId);
            var countryViewModels = mapper.Map<IEnumerable<CountryDto>, IEnumerable<CountryViewModel>>(countries);

            return Ok(countryViewModels);
        }

        /// <summary>Create a country under organization</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Only owner of organization can create new country.
        /// </remarks>
        /// <param name="organizationId">Id of organization</param>
        /// <param name="createCountryModel">Create Country Model</param>  
        /// <response code="200">Country created successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpPost("~/api/organizations/{organizationId}/countries")]
        public async Task<IActionResult> Post(int organizationId, [FromBody]CreateCountryModel createCountryModel)
        {
            string email = User.Identity.Name;

            await countryService.AddCountryAsync(
                organizationId,
                mapper.Map<CreateCountryModel, CreateCountryDto>(createCountryModel),
                email
            );

            return Ok();
        }

        /// <summary>Update country by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Only owner of organization can update country.
        /// </remarks>
        /// <param name="id">Id of country</param>
        /// <param name="createCountryModel">Changed data about country</param>
        /// <response code="200">Country updated successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CreateCountryModel createCountryModel)
        {
            string email = User.Identity.Name;

            await countryService.UpdateCountryAsync(
                id,
                mapper.Map<CreateCountryModel, CreateCountryDto>(createCountryModel),
                email
            );
            return Ok();
        }

        /// <summary>Delete country by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Delete Country can only owner of organization
        /// </remarks> 
        /// <param name="id">Id of country</param>
        /// <response code="200">Delete successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await countryService.DeleteCountryAsync(id, email);
            return Ok();
        }
    }
}