using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>

{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(SozlukConstans.FavExchangeName,
                                            SozlukConstans.DefaultExchangeType,
                                            SozlukConstans.CreateEntryCommentFavQueueName,
                                            new CreateEntryCommentFavEvent()
                                             {
                                                 EntryCommentId = request.EntryCommentId,
                                                 CreatedBy = request.UserId
                                             });

        return await Task.FromResult(true);

    }
}
