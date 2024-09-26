using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
{
    private readonly IEntryCommentRepository _entryRepository;

    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query
            .Include(inc => inc.EntryCommentFavorites)
            .Include(inc => inc.CreatedBy)
            .Include(inc => inc.EntryCommentVotes)
            .Where(entrycomments => entrycomments.EntryId == request.EntryId);

        var list = query.Select(x => new GetEntryCommentsViewModel()
        {
            Id = x.Id,
            Content = x.Content,
            IsFavorited = request.UserId.HasValue &&
                            x.EntryCommentFavorites.Any(x => x.CreatedById == request.UserId),
            FavoritedCount = x.EntryCommentFavorites.Count,
            CreatedDate = x.CreateDate,
            CreatedByUserName = x.CreatedBy.UserName,
            VoteType = request.UserId.HasValue && x.EntryCommentVotes.Any(x => x.CreatedById == request.UserId)
                              ? x.EntryCommentVotes.FirstOrDefault(x => x.CreatedById == request.UserId).VoteType
                                 : Common.ViewModels.VoteType.None


        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
