using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Queries
{
    
        public record IsUserNameExistQuery(string userName) : IRequest<ResultDTO<bool>>;
        public class IsUserNameExistQueryHandler : IRequestHandler<IsUserNameExistQuery, ResultDTO<bool>>
        {
            private readonly IRepository<User> _userRepository;
            public IsUserNameExistQueryHandler(IRepository<User> userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<ResultDTO<bool>> Handle(IsUserNameExistQuery request, CancellationToken cancellationToken)
            {
                var result = _userRepository.Any(u => u.UserName == request.userName);
                if (result)
                {
                    return ResultDTO<bool>.Sucess(true);
                }
                return ResultDTO<bool>.Faliure(ErrorCode.UserNameIsNotFound, "User Name is Not Found");
            }


        }
    }
