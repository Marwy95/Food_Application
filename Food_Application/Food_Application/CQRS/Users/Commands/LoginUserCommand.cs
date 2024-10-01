using Food_Application.CQRS.Users.Queries;
using Food_Application.DTOs;
using Food_Application.Enums;
using Food_Application.Exceptions;
using Food_Application.Helpers;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Food_Application.CQRS.Users.Commands
{
    public record LoginUserCommand(string email, string password) : IRequest<ResultDTO<string>>;
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ResultDTO<string>>
    {
        private readonly IMediator _mediator;
        public LoginUserCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ResultDTO<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.email));
            var result = BCrypt.Net.BCrypt.Verify(request.password, user.Data.Password);
            if (user != null && result && user.Data.IsActive == true)
            {
                return ResultDTO<string>.Sucess(TokenGenerator.GenerateToken(user.Data));
            }
            else
            {
                throw new BusinessException(ErrorCode.WrongPasswordOrEmail, "Wrong Password or Email");
            }
        }
    }
}
