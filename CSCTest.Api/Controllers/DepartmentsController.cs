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

        /// <summary>Get all departments</summary>
        /// <returns>Array with information about all departments</returns>
        /// <response code="200">Get countries successful</response> 
        /// <response code="500">Internal Server Error</response> 
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var departments = await departmentService.GetDepartmentsAsync();
            var departmentViewModels = mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<DepartmentViewModel>>(departments);

            return Ok(departmentViewModels);
        }

        /// <summary>Get department by id</summary>
        /// <returns>Information about department</returns>
        /// <param name="id">Id of department</param>
        /// <response code="200">Get department successful</response>
        /// <response code="404">Not found department with this id</response>
        /// <response code="500">Internal Server Error</response> 
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

        /// <summary>Get all departments in concrete offering</summary>
        /// <returns>Array with information about all departments in concrete offering</returns>
        /// <param name="offeringId">Id of depatment</param>
        /// <response code="200">Get countries successful</response> 
        /// <response code="500">Internal Server Error</response>
        [AllowAnonymous]
        [HttpGet("~/api/offerings/{offeringId}/departments")]
        public IActionResult GetOfferingDepartments(int offeringId)
        {
            var departments = departmentService.GetOfferingDepartments(offeringId);
            var departmentViewModels = mapper.Map<IEnumerable<DepartmentDto>, IEnumerable<DepartmentViewModel>>(departments);

            return Ok(departmentViewModels);
        }

        /// <summary>Create a department inside offering</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Only owner of organization can create new department.
        /// </remarks>
        /// <param name="offeringId">Id of offering</param>
        /// <param name="name">Name of department</param>  
        /// <response code="200">Department created successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
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

        /// <summary>Update depatment by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Only owner of organization can update department.
        /// </remarks>
        /// <param name="id">Id of department</param>
        /// <param name="name">New name of department</param>
        /// <response code="200">Department updated successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]string name)
        {
            string email = User.Identity.Name;

            await departmentService.UpdateDepartmentAsync(id, name, email);
            return Ok();
        }

        /// <summary>Delete department by id</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>
        /// Delete Department can only owner of organization
        /// </remarks> 
        /// <param name="id">Id of department</param>
        /// <response code="200">Delete successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string email = User.Identity.Name;

            await departmentService.DeleteDepartmentAsync(id, email);
            return Ok();
        }
    }
}