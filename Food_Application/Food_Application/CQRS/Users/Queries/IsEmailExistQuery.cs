using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Queries
{
    public record IsEmailExistQuery(string email) : IRequest<ResultDTO<bool>>;
    public class IsEmailExistQueryHandler : IRequestHandler<IsEmailExistQuery, ResultDTO<bool>>
    {
        private readonly IRepository<User> _userRepository;
        public IsEmailExistQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResultDTO<bool>> Handle(IsEmailExistQuery request, CancellationToken cancellationToken)
        {
            var result = _userRepository.Any(u => u.Email == request.email);
            if (result)
            {
                return ResultDTO<bool>.Sucess(true);
            }
            return ResultDTO<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is Not Found");
        }


    }
}
