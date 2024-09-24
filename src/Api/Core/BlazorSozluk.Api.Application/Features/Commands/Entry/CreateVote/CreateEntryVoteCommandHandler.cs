using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateVote;

public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstans.VoteExchangeName,
            exchangeType: SozlukConstans.DefaultExchangeType,
            queueName: SozlukConstans.CreateEntryVoteQueueName,
            obj: new CreateEnryVoteEvent()
            {
                VoteType = request.VoteType,
                CreatedBy = request.CreatedBy,
                EntryId =request.EntryId,
            });

        return await Task.FromResult(true);


    }
}
