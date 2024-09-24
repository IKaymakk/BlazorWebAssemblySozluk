using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infastructure;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav;

public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(
            exchangeName: SozlukConstans.FavExchangeName,
            exchangeType: SozlukConstans.DefaultExchangeType,
            queueName: SozlukConstans.DeleteEntryFavQueueName,
            obj: new DeleteEntryFavEvent()
            {
                CreatedBy = request.UserId,
                EntryId=request.EntryId,
            });

        return await Task.FromResult(true);
    }
}
