using Food_Application.CQRS.Users.Commands;
using Food_Application.CQRS.Users.Queries;
using Food_Application.Helpers;
using Food_Application.Models;
using Food_Application.ViewModels;
using Food_Application.ViewModels.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Food_Application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController :ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(CreateUserViewModel viewModel)
        {
            var user = viewModel.MapOne<RegisterUserDTO>();
            var result = await _mediator.Send(new RegisterUserCommand(user));
            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(ResultViewModel<User>.Sucess(result.Data));
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser()
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<IActionResult> ForgetPassword()
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<IActionResult> ChangePassword()
        {
            throw new NotImplementedException();
        }
        [HttpPut]
        public async Task<IActionResult> ResetPassword()
        {
            throw new NotImplementedException();
        }


    }
}
