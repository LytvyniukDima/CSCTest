using AutoMapper;
using CSCTest.Data.Entities;
using CSCTest.Tools.Extensions;
using CSCTest.Service.DTOs.Users;
using CSCTest.Service.DTOs.Organizations;
using CSCTest.Service.DTOs.Countries;
using CSCTest.Service.DTOs.Businesses;
using CSCTest.Service.DTOs.Families;
using CSCTest.Service.DTOs.Offerings;
using CSCTest.Service.DTOs.Departments;

namespace CSCTest.Tests.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();

            CreateMap<OrganizationDto, Organization>()
                .ForMember("Type", opt => opt.MapFrom(od => od.Type.GetOrganizationType()))
                .ForMember("Id", opt => opt.Ignore());
            CreateMap<Organization, OrganizationDto>()
                .ForMember("Type", opt => opt.MapFrom(o => o.Type.GetStringName()))
                .ForMember("OwnerId", opt => opt.MapFrom(o => o.UserId))
                .ForMember("HasChildren", opt => opt.MapFrom(o => o.Countries.Count > 0 ? true : false));

            CreateMap<CreateCountryDto, Country>();
            CreateMap<Country, CountryDto>()
                .ForMember("HasChildren", opt => opt.MapFrom(c => c.CountryBusinesses.Count > 0 ? true : false));

            CreateMap<Business, BusinessTypeDto>();

            CreateMap<CountryBusiness, BusinessDto>()
                .ForMember("Name", opt => opt.MapFrom(cb => cb.Business.Name))
                .ForMember("HasChildren", opt => opt.MapFrom(cb => cb.BusinessFamilies.Count > 0 ? true : false));
            
            CreateMap<Family, FamilyTypeDto>()
                .ForMember("BusinessName", opt => opt.MapFrom(f => f.Business.Name));
            CreateMap<BusinessFamily, FamilyDto>()
                .ForMember("Name", opt => opt.MapFrom(bf => bf.Family.Name))
                .ForMember("BusinessId", opt => opt.MapFrom(bf => bf.CountryBusinessId))
                .ForMember("HasChildren", opt => opt.MapFrom(bf => bf.FamilyOfferings.Count > 0 ? true : false));

            CreateMap<Offering, OfferingTypeDto>()
                .ForMember("FamilyName", opt => opt.MapFrom(o => o.Family.Name));
            CreateMap<FamilyOffering, OfferingDto>()
                .ForMember("Name", opt => opt.MapFrom(fo => fo.Offering.Name))
                .ForMember("FamilyId", opt => opt.MapFrom(fo => fo.BusinessFamily.Id))
                .ForMember("HasChildren", opt => opt.MapFrom(fo => fo.Departments.Count > 0 ? true : false));

            CreateMap<Department, DepartmentDto>()
                .ForMember("OfferingId", opt => opt.MapFrom(d => d.FamilyOfferingId));
        }
    }
}