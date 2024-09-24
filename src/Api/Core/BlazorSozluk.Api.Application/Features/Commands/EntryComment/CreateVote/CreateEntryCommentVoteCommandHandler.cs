using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateVote;

public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstans.FavExchangeName,
            exchangeType: SozlukConstans.DefaultExchangeType,
            queueName: SozlukConstans.CreateEntryCommentVoteQueueName,
            obj: new CreateEntryCommentVoteEvent()
            {
                VoteType = request.voteType,
                CreatedBy = request.CreatedBy,
                EntryCommentId  =request.EntryCommentId
            });

        return await Task.FromResult(true); 
    }
}
