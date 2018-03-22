using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Countries;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await countryService.GetCountriesAsync();
            var countryViewModels = mapper.Map<IEnumerable<CountryDto>, IEnumerable<CountryViewModel>>(countries);

            return Ok(countryViewModels);
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CreateCountryModel createCountryModel)
        {
            string email = User.Identity.Name;

            await countryService.UpdateCountryAsync(
                id,
                mapper.Map<CreateCountryModel, CreateCountryDto>(createCountryModel),
                email);

            return Ok();
        }

        /// <summary>
        /// Delete Country
        /// </summary>
        /// <remarks>Delete country by id.</remarks>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await countryService.DeleteCountryAsync(id, email);
            return Ok();
        }
    }
}