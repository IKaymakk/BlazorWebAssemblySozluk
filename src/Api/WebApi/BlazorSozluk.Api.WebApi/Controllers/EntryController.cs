using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntryController : BaseController
{
    private readonly IMediator _mediator;

    public EntryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateEntry")]
    public async Task<IActionResult> CreateEntry(CreateEntryCommand command)
    {
        if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPost("CreateEntryComment")]
    public async Task<IActionResult> CreateCommentEntry(CreateEntryCommentCommand command)
    {
        if (!command.CreatedById.HasValue)
             command.CreatedById = UserId;


        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
