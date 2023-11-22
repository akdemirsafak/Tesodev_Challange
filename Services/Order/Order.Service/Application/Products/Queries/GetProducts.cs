using Mapster;
using Order.Core.Repositories;
using Order.Model.Common;
using Order.Model.Responses.Product;
using Order.Service.Common;

namespace Order.Service.Application.Product.Queries;

public static class GetProducts
{
    public record Query():IQuery<ApiResponse<List<GetProductResponse>>>;
    public class QueryHandler(IGenericRepository<Order.Core.Entities.Product> _productRepository) : IQueryHandler<Query, ApiResponse<List<GetProductResponse>>>
    {
        public async Task<ApiResponse<List<GetProductResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products=await _productRepository.GetAllAsync();
            return ApiResponse<List<GetProductResponse>>.Success(products.Adapt<List<GetProductResponse>>());
        }
    }

}
