using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infastructure;
using BlazorSozluk.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.Deletevote;

public class DeleteEntryCommentVoteCommand:IRequest<bool>
{
    public Guid EntryCommentId { get; set; }
    public Guid UserId { get; set; }

    public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }




}
public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
                 exchangeName: SozlukConstans.VoteExchangeName,
                 exchangeType: SozlukConstans.DefaultExchangeType,
                 queueName: SozlukConstans.DeleteEntryCommentVoteQueueName,
                 obj: new DeleteEntryCommentVoteEvent()
                 {
                     CreatedBy = request.UserId,
                     EntryCommentId = request.EntryCommentId
                 });

        return await Task.FromResult(true);
    }
}
