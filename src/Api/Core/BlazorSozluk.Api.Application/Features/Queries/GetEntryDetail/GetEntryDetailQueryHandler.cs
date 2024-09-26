using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IEntryRepository _entryRepository;

    public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query.Include(x => x.EntryVotes)
            .Include(x => x.EntryFavorites)
            .Include(x => x.CreatedBy)
            .Where(x => x.Id == request.EntryId);

        var list = query.Select(x => new GetEntryDetailViewModel
        {
            Id = request.EntryId,
            Subject = x.Subject,
            Content = x.Content,
            CreatedByUserName = x.CreatedBy.UserName,
            CreatedDate = x.CreateDate,
            FavoritedCount = x.EntryFavorites.Count,

            IsFavorited = request.UserId.HasValue && x.EntryComments.Any(ec => ec.CreatedById == request.UserId),

            VoteType = request.UserId.HasValue && x.EntryVotes.Any(ev => ev.CreatedById == request.UserId)
            ? x.EntryVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType : VoteType.None
        });

        return await list.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
