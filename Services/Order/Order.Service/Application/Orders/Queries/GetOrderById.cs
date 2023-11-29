using FluentValidation;
using Mapster;
using Order.Core.Repositories;
using Order.Model.Responses.Orders;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Orders.Queries;

public static class GetOrderById
{
    public record Query(Guid Id, string CustomerId) : IQuery<ApiResponse<GetOrderResponse>>;
    public class QueryHandler(IGenericRepository<Order.Core.Entities.Order> _orderRepository) : IQueryHandler<Query, ApiResponse<GetOrderResponse>>
    {
        public async Task<ApiResponse<GetOrderResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var existOrder= await _orderRepository.GetAsync(x=>x.Id==request.Id && x.CustomerId==Guid.Parse(request.CustomerId));

            if (existOrder is null)
                return ApiResponse<GetOrderResponse>.Fail("Order not found.", 404);

            return ApiResponse<GetOrderResponse>.Success(existOrder.Adapt<GetOrderResponse>());
        }
    }
    public class GetOrderByIdQueryValidator : AbstractValidator<Query>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
