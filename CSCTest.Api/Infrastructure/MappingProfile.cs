using AutoMapper;
using CSCTest.Api.Models.Users;
using CSCTest.Api.Models.Organizations;
using CSCTest.Data.Entities;
using CSCTest.Service.DTOs;
using CSCTest.Tools.Extensions;
using CSCTest.Api.Models.Countries;

namespace CSCTest.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationUserCredentials, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>();

            CreateMap<OrganizationDto, Organization>()
                .ForMember("Type", opt => opt.MapFrom(od => od.Type.GetOrganizationType()))
                .ForMember("Id", opt => opt.Ignore());
            CreateMap<Organization, OrganizationDto>()
                .ForMember("Type", opt => opt.MapFrom(o => o.Type.GetStringName()))
                .ForMember("OwnerId", opt => opt.MapFrom(o => o.UserId))
                .ForMember("HasChildren", opt => opt.MapFrom(o => o.Countries.Count > 0 ? true : false));
            CreateMap<CreateOrganizationModel, OrganizationDto>();
            CreateMap<OrganizationDto, OrganizationViewModel>();

            CreateMap<CreateCountryModel, CreateCountryDto>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<Country, CountryDto>()
                .ForMember("HasChildren", opt => opt.MapFrom(c => c.CountryBusinesses.Count > 0 ? true : false));
            CreateMap<CountryDto, CountryViewModel>();
        }
    }
}