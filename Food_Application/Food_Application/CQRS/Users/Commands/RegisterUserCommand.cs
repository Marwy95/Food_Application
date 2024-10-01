using Food_Application.CQRS.Users.Queries;
using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Exceptions;
using Food_Application.Helpers;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Commands
{
    public record RegisterUserCommand(RegisterUserDTO DTO) :IRequest<ResultDTO<int>>;
    public record RegisterUserDTO(string UserName, string Password, string ConfirmPassword, string Email, string Phone, string Country);
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ResultDTO<int>>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMediator _mediator;   
        public RegisterUserCommandHandler(IRepository<User> userRepository, IMediator mediator)
        {
            _userRepository = userRepository;
            _mediator = mediator;
        }
        public async Task<ResultDTO<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserByEmailQuery(request.DTO.Email));
            if (result.IsSuccess) 
            {
                throw new BusinessException(ErrorCode.EmailAlreadyExist, "Email is already Exists");
            }
            result = await _mediator.Send(new GetUserByNameQuery(request.DTO.UserName));
            if (result.IsSuccess)
            {
                throw new BusinessException(ErrorCode.UserNameAlreadyExist, "User Name is already Exists");
            }
            
            if (request.DTO.Password != request.DTO.ConfirmPassword) {
                return ResultDTO<int>.Faliure(ErrorCode.PasswordsDontMatch,"Passwords don't match");
            }
            var user =  request.DTO.MapOne<User>();
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.DTO.Password);
            _userRepository.Add(user);
            _userRepository.SaveChanges();
            //send the otp to the email
            return ResultDTO<int>.Sucess(user.ID,"User Added Successfully");
        }
    }
}
