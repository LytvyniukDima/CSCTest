using System.Collections.Generic;
using System.Threading.Tasks;
using CSCTest.Service.DTOs.Departments;

namespace CSCTest.Service.Abstract
{
    public interface IDepartmentService
    {
        Task AddDepartmentAsync(DepartmentCreateDto departmentCreateDto, string email);
        Task<DepartmentDto> GetDepartmentAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        IEnumerable<DepartmentDto> GetOfferingDepartments(int familyOfferingId);
        Task UpdateDepartmentAsync(int id, string name, string email);
        Task DeleteDepartmentAsync(int id, string email);
    }
}