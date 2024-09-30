using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Helpers;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Commands
{
    public record RegisterUserCommand(RegisterUserDTO DTO) :IRequest<ResultDTO<User>>;
    public record RegisterUserDTO(string UserName, string Password, string ConfirmPassword, string Email, string Phone, string Country);
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO<User>>
    {
        private readonly IRepository<User> _userRepository;
        public RegisterUserCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            
            if (request.DTO.Password != request.DTO.ConfirmPassword) {
                return ResultDTO<User>.Faliure(ErrorCode.PasswordsDontMatch,"Passwords don't match");
            }
            var user =  request.DTO.MapOne<User>();
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.DTO.Password);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            return ResultDTO<User>.Sucess(user);
        }
    }
}
