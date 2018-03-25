using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CSCTest.DAL.Exceptions;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.Abstract;
using CSCTest.Service.DTOs.Departments;
using CSCTest.Service.Infrastructure;

namespace CSCTest.Service.Concrete
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddDepartmentAsync(DepartmentCreateDto departmentCreateDto, string email)
        {
            using (unitOfWork)
            {
                var familyOfferingRepository = unitOfWork.FamilyOfferingRepository;
                var departmentRepository = unitOfWork.DepartmentRepository;

                var familyOffering = await familyOfferingRepository.FindAsync(x =>
                    x.Id == departmentCreateDto.OfferingId &&
                    x.BusinessFamily.CountryBusiness.Country.Organization.User.Email == email
                );
                if (familyOffering == null)
                {
                    throw new HttpStatusCodeException(404, $"Not found offering with id \"{departmentCreateDto.OfferingId}\" or user with email {email} don't have permisson to manage this organization");
                }

                try
                {
                    departmentRepository.Add(new Department
                    {
                        Name = departmentCreateDto.Name,
                        FamilyOffering = familyOffering
                    });
                    await unitOfWork.SaveAsync();
                }
                catch (DALException ex)
                {
                    throw new HttpStatusCodeException(400, ex.Message);
                }
            }
        }

        public async Task DeleteDepartmentAsync(int id, string email)
        {
            using (unitOfWork)
            {
                var departmentRepository = unitOfWork.DepartmentRepository;

                var department = await departmentRepository.FindAsync(x =>
                    x.Id == id &&
                    x.FamilyOffering.BusinessFamily.CountryBusiness.Country.Organization.User.Email == email
                );
                if (department == null)
                {
                    throw new HttpStatusCodeException(404, $"Not found department with id \"{id}\" or user with email {email} don't have permisson to manage this organization");
                }

                departmentRepository.Delete(department);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task<DepartmentDto> GetDepartmentAsync(int id)
        {
            using (unitOfWork)
            {
                var departmentRepository = unitOfWork.DepartmentRepository;

                var department = await departmentRepository.FindAsync(x => x.Id == id);
                if (department == null)
                    return null;

                var departmentDto = mapper.Map<Department, DepartmentDto>(department);
                return departmentDto;
            }
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            using (unitOfWork)
            {
                var departmentRepository = unitOfWork.DepartmentRepository;

                var departments = await departmentRepository.GetAllAsync();
                var departmentDtos = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departments);

                return departmentDtos;
            }
        }

        public IEnumerable<DepartmentDto> GetOfferingDepartments(int familyOfferingId)
        {
            using (unitOfWork)
            {
                var departmentRepository = unitOfWork.DepartmentRepository;

                var departments = departmentRepository.FindAll(x => x.FamilyOfferingId == familyOfferingId);
                var departmentDtos = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departments);

                return departmentDtos;
            }
        }

        public async Task UpdateDepartmentAsync(int id, string name, string email)
        {
            using (unitOfWork)
            {
                var departmentRepository = unitOfWork.DepartmentRepository;

                var department = await departmentRepository.FindAsync(x =>
                    x.Id == id &&
                    x.FamilyOffering.BusinessFamily.CountryBusiness.Country.Organization.User.Email == email
                );
                if (department == null)
                {
                    throw new HttpStatusCodeException(404, $"Not found department with id \"{id}\" or user with email {email} don't have permisson to manage this organization");
                }

                department.Name = name;

                try
                {
                    departmentRepository.Update(department);
                    await unitOfWork.SaveAsync();
                }
                catch (DALException ex)
                {
                    throw new HttpStatusCodeException(400, ex.Message);
                }
            }
        }
    }
}