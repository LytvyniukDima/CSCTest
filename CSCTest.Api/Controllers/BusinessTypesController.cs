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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string businessName)
        {
            await businessService.AddBusinessTypeAsync(businessName);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var businessTypes = await businessService.GetBusinessTypesAsync();

            return Ok(mapper.Map<IEnumerable<BusinessTypeDto>, IEnumerable<BusinessTypeViewModel>>(businessTypes));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var businessType = await businessService.GetBusinessTypeAsync(id);
            if (businessType == null)
                return NotFound();

            return Ok(mapper.Map<BusinessTypeDto, BusinessTypeViewModel>(businessType));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            await businessService.UpdateBusinessTypeAsync(id, name);

            return Ok();
        }
    }
}