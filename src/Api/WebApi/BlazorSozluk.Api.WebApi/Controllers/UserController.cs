using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var values = await _mediator.Send(command);
            return Ok(values);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var values = await _mediator.Send(command);
            return Ok(values);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var values = await _mediator.Send(command);
            return Ok(values);
        }
    }
}
