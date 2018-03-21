using AutoMapper;
using CSCTest.Data.Abstract;
using CSCTest.Data.Entities;
using CSCTest.Service.DTOs;
using CSCTest.Service.Abstract;

namespace CSCTest.Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        public void AddUser(UserRegistrationDto userRegistrationDto)
        {
            using (unitOfWork)
            {
                var userRepository = unitOfWork.UserRepository;
                var user = mapper.Map<UserRegistrationDto, User>(userRegistrationDto);
                userRepository.Add(user);
                unitOfWork.Save();
            }
        }
    }
}