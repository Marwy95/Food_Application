using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Queries
{
    
        public record GetUserByNameQuery(string userName) : IRequest<ResultDTO<User>>;
        public class GetUserByNameQueryHandler : IRequestHandler<GetUserByNameQuery, ResultDTO<User>>
        {
            private readonly IRepository<User> _userRepository;
            public GetUserByNameQueryHandler(IRepository<User> userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<ResultDTO<User>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
            {
                var user = _userRepository.First(u => u.UserName == request.userName);
                if (user != null)
                {
                    return ResultDTO<User>.Sucess(user);
                }
                return ResultDTO<User>.Faliure(ErrorCode.UserNameIsNotFound, "User Name is Not Found");
            }


        }
    }
