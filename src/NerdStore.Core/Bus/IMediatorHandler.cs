using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T ev) where T : Event;
    Task<bool> SendCommand<T>(T command) where T : Command;
}