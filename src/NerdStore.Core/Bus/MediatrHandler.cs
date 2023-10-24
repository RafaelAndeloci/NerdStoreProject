using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public class MediatrHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatrHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T ev) where T : Event
    {
        await _mediator.Publish(ev);
    }

    public async Task<bool> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}