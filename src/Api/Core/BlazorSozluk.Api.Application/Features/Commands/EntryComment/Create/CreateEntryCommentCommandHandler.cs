using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Common.Infastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.Create;

public class CreateEntryCommentCommandHandler : IRequestHandler<CreateEntryCommentCommand, Guid>
{
    private readonly IEntryCommentRepository _repository;
    private readonly IMapper _mapper;

    public CreateEntryCommentCommandHandler(IEntryCommentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEntryCommentCommand request, CancellationToken cancellationToken)
    {
        var dbEntryComment = _mapper.Map<Domain.Models.EntryComment>(request);

        await _repository.AddAsync(dbEntryComment);

        return dbEntryComment.Id;

    }
}
