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
    public record VerifyAccountCommand(string email, string OtpCode) : IRequest<ResultDTO<bool>>;
    public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, ResultDTO<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<User> _userRepository;
        public VerifyAccountCommandHandler(IMediator mediator, IRepository<User> userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;

        }
        public async Task<ResultDTO<bool>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserByEmailQuery(request.email));
            if(user.Data.OtpCode==request.OtpCode)
            {
                user.Data.IsActive = true;
                _userRepository.Update(user.Data);
                _userRepository.SaveChanges();

                return ResultDTO<bool>.Sucess(true);
            }
            else
            {
                throw new BusinessException(ErrorCode.WrongOtp, "Wrong OTP Code");
            }
        }
    }
}
