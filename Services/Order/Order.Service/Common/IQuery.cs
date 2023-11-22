using MediatR;

namespace Order.Service.Common;

public interface IQuery<out TResponse>:IRequest<TResponse>
{
}
