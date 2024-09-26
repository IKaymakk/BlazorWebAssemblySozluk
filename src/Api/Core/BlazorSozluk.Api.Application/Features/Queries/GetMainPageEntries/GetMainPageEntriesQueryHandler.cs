﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query
            .Include(inc => inc.EntryFavorites)
            .Include(inc => inc.CreatedBy)
            .Include(inc => inc.EntryVotes);

        var list = query.Select(x => new GetEntryDetailViewModel()
        {
            Id = x.Id,
            Subject = x.Subject,
            Content = x.Content,

            IsFavorited = request.UserId.HasValue &&
                            x.EntryFavorites.Any(x => x.CreatedById == request.UserId),

            FavoritedCount = x.EntryFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.UserName,

            VoteType = request.UserId.HasValue && x.EntryVotes.Any(x => x.CreatedById == request.UserId)
                              ? x.EntryVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType
                                 : Common.ViewModels.VoteType.None


        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
