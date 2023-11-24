using Mapster;
using Order.Core.Repositories;
using Order.Model.Responses.Orders;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Orders.Queries;

public static class GetOrders
{
    public record Query():IQuery<ApiResponse<List<GetOrderResponse>>>;

    public class QueryHandler(IGenericRepository<Order.Core.Entities.Order> _orderRepository) : IQueryHandler<Query, ApiResponse<List<GetOrderResponse>>>
    {
        public async Task<ApiResponse<List<GetOrderResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var orders= await _orderRepository.GetAllAsync();
            return ApiResponse<List<GetOrderResponse>>.Success(orders.Adapt<List<GetOrderResponse>>());
        }
    }

}
