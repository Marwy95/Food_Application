using AutoMapper;
using Food_Application.CQRS.Users.Commands;
using Food_Application.Models;
using Food_Application.ViewModels.Users;

namespace Food_Application.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserViewModel, RegisterUserDTO>();
            CreateMap<RegisterUserDTO, User>();
        }
    }
}
