using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Exceptions;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string email) :IRequest<ResultDTO<User>>;
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ResultDTO<User>>
    {
        private readonly IRepository<User> _userRepository;
        public GetUserByEmailQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
           var user =  _userRepository.First(u=>u.Email == request.email);
            if (user != null)
            {
                return ResultDTO<User>.Sucess(user);
            }
            return ResultDTO<User>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
            // throw new BusinessException(ErrorCode.EmailIsNotFound, "Email is Not Found");
        }

       
    }
}
