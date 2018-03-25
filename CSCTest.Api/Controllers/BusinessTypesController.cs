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
    [Route("/api/business_types")]
    public class BusinessTypesController : Controller
    {
        private readonly IBusinessService businessService;
        private readonly IMapper mapper;

        public BusinessTypesController(IBusinessService businessService, IMapper mapper)
        {
            this.businessService = businessService;
            this.mapper = mapper;
        }

        /// <summary>Get all types of businesses</summary>
        /// <returns>Array with information about all types of business</returns>
        /// <response code="200">Get type of business successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var businessTypes = await businessService.GetBusinessTypesAsync();

            return Ok(mapper.Map<IEnumerable<BusinessTypeDto>, IEnumerable<BusinessTypeViewModel>>(businessTypes));
        }

        /// <summary>Get type of business by id</summary>
        /// <returns>Information about type of business</returns>
        /// <param name="id">Id of type of business</param>
        /// <response code="200">Get type of business successful</response>
        /// <response code="404">Not found type of business with this id</response>
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var businessType = await businessService.GetBusinessTypeAsync(id);
            if (businessType == null)
                return NotFound();

            return Ok(mapper.Map<BusinessTypeDto, BusinessTypeViewModel>(businessType));
        }
        
        /// <summary>Create a new type of business</summary>
        /// <returns>an IActionResult</returns>
        /// <param name="businessName">Name of business type</param> 
        /// <response code="200">Type of business created successful</response>
        /// <response code="400">Type of business with the same name already exist</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string businessName)
        {
            await businessService.AddBusinessTypeAsync(businessName);

            return Ok();
        }

        /// <summary>Update type of business by id</summary>
        /// <returns>an IActionResult</returns>
        /// <param name="id">Id of type of business</param>
        /// <param name="name">New name for type of business</param>
        /// <response code="200">Changed type of business successful</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Not found type of business</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            await businessService.UpdateBusinessTypeAsync(id, name);

            return Ok();
        }
    }
}