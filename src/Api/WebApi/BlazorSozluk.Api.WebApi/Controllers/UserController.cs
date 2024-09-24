using BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
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

    [HttpPost("EmailConfirm")]
    public async Task<IActionResult> EmailConfirm(Guid id)
    {
        var values = await _mediator.Send(new ConfirmEmailCommand() { ConfirmationId = id });
        return Ok(values);
    }

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        if (!command.UserId.HasValue)
            command.UserId = UserId;

        var values = await _mediator.Send(command);
        return Ok(values);
    }
}
