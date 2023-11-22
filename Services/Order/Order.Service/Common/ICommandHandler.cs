using MediatR;

namespace Order.Service.Common;

public interface ICommandHandler<in TCommand,TResponse>:IRequestHandler<TCommand,TResponse>
    where TCommand :ICommand<TResponse>
{
}
