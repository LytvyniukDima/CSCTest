using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.Api.Models.Departments;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCTest.Api.Controllers
{
    [Authorize]
    [Route("api/departments")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly IMapper mapper;

        public DepartmentsController(IDepartmentService departmentService, IMapper mapper)
        {
            this.departmentService = departmentService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departments = await departmentService.GetDepartmentsAsync();
            var departmentViewModels = mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<DepartmentViewModel>>(departments);

            return Ok(departmentViewModels);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var department = await departmentService.GetDepartmentAsync(id);
            if (department == null)
                return NotFound();

            var departmentViewModel = mapper.Map<DepartmentDto, DepartmentViewModel>(department);
            return Ok(department);
        }

        [AllowAnonymous]
        [HttpGet("~/api/offerings/{offeringId}/departments")]
        public async Task<IActionResult> GetOfferingDepartments(int offeringId)
        {
            var departments = departmentService.GetOfferingDepartments(offeringId);
            var departmentViewModels = mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<DepartmentViewModel>>(departments);

            return Ok(departmentViewModels);
        }

        [HttpPost("~/api/offerings/{offeringId}/departments")]
        public async Task<IActionResult> Post(int offeringId, [FromBody]string name)
        {
            string email = User.Identity.Name;

            await departmentService.AddDepartmentAsync(
                new DepartmentCreateDto
                {
                    Name = name,
                    OfferingId = offeringId
                },
                email
            );

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            string email = User.Identity.Name;

            await departmentService.UpdateDepartmentAsync(id, name, email);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await departmentService.DeleteDepartmentAsync(id, email);
            return Ok();
        }
    }
}