using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstans.FavExchangeName,
            exchangeType: SozlukConstans.DefaultExchangeType,
            queueName: SozlukConstans.DeleteEntryCommentFavQueueName,
            obj: new DeleteEntryCommentFavEvent()
            {
                CreatedBy = request.UserId,
                EntryCommentId = request.EntryCommentId,
            });
        return await Task.FromResult(true);
        
    }
}
