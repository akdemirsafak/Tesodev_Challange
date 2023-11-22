using MediatR;

namespace Order.Service.Common;

public interface ICommand<out TResponse>:IRequest<TResponse>
{
}
