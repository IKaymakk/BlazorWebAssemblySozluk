using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infastructure;
using MediatR;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(
                exchangeName: SozlukConstans.FavExchangeName,
                exchangeType: SozlukConstans.DefaultExchangeType,
                queueName: SozlukConstans.CreateEntryFavQueueName,
                obj: new CreateEntryFavEvent()
                {
                    CreatedBy = request.UserId.Value,
                    EntryId = request.EntryId.Value
                });
            return Task.FromResult(true);
        }
    }
}
