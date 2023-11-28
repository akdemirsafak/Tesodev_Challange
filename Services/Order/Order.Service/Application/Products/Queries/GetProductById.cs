using FluentValidation;
using Mapster;
using Order.Core.Repositories;
using Order.Model.Responses.Product;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Products.Queries;

public static class GetProductById
{
    public record Query(Guid Id) : IQuery<ApiResponse<GetProductResponse>>;
    public class QueryHandler(IGenericRepository<Order.Core.Entities.Product> _productRepository) : IQueryHandler<Query, ApiResponse<GetProductResponse>>
    {
        public async Task<ApiResponse<GetProductResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var products=await _productRepository.GetAsync(x=>x.Id==request.Id);
            return ApiResponse<GetProductResponse>.Success(products.Adapt<GetProductResponse>());
        }
    }
    public class GetProductByIdCommandValidator : AbstractValidator<Query>
    {
        public GetProductByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
