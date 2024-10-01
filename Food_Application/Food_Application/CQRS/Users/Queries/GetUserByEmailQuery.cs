using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Queries
{
    public record GetUserByEmailQuery(string email) :IRequest<ResultDTO<bool>>;
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        public GetUserByEmailQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<bool>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
           var user =  _userRepository.First(u=>u.Email == request.email);
            if (user != null)
            {
                return ResultDTO<bool>.Sucess(true);
            }
            return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
        }

       
    }
}
