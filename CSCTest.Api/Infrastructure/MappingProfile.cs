using AutoMapper;
using CSCTest.Api.Models;
using CSCTest.Data.Entities;
using CSCTest.Service.DTOs;
using CSCTest.Tools.Extensions;

namespace CSCTest.Api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrganizationDto, Organization>()
                .ForMember("Type", opt => opt.MapFrom(od => od.Type.GetOrganizationType()));
            CreateMap<RegistrationUserCredentials, UserRegistrationDto>();
            CreateMap<UserRegistrationDto, User>();
        }
    }
}