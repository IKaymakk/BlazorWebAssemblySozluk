using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
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

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            query = query.Where(entry => entry.CreatedById == request.UserId);

        else if (!string.IsNullOrEmpty(request.UserName))
            query = query.Where(entry => entry.CreatedBy.UserName == request.UserName);

        else return null;

        query = query.Include(entry => entry.CreatedBy).Include(entry => entry.EntryFavorites);

        var list = query.Select(x => new GetUserEntriesDetailViewModel
        {
            Content = x.Content,
            CreatedByUserName = x.CreatedBy.UserName,
            CreatedDate = x.CreateDate,
            FavoritedCount = x.EntryFavorites.Count,
            Id = x.Id,
            IsFavorited = false,
            Subject = x.Subject
        });

        return await list.GetPaged(request.Page, request.PageSize);
    }
}
