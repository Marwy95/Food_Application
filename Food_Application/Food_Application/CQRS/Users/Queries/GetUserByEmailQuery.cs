using Food_Application.Models;
using Food_Application.Repositories;
using MediatR;

namespace Food_Application.CQRS.Users.Queries
{
    public class GetUserByEmailQuery :IRequest<string>
    {

    }
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery ,string>
    {
        private readonly IRepository<User> _userRepository;
        public GetUserByEmailQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return  _userRepository.Test();
        }
    }
}
