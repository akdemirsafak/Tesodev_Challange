using FluentValidation;
using Mapster;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Model.Common;
using Order.Model.Requests.Products;
using Order.Model.Responses.Product;
using Order.Service.Common;

namespace Order.Service.Application.Products.Commands;

public static class UpdateProduct
{
    public record Command(Guid Id,UpdateProductRequest Model) : ICommand<ApiResponse<UpdatedProductResponse>>;
    public class CommandHandler(IGenericRepository<Order.Core.Entities.Product> _productRepository, IUnitOfWork _unitOfWork) : ICommandHandler<Command, ApiResponse<UpdatedProductResponse>>
    {
        public async Task<ApiResponse<UpdatedProductResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existProduct= await _productRepository.GetAsync(x=>x.Id == request.Id);
            if (existProduct is null)
                return ApiResponse<UpdatedProductResponse>.Fail("Product not found.",404);

            existProduct.Name=request.Model.Name;
            existProduct.ImageUrl=request.Model.ImageUrl;
            await _productRepository.UpdateAsync(existProduct);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<UpdatedProductResponse>.Success(existProduct.Adapt<UpdatedProductResponse>());
        }
    }
    public class UpdateProductCommandValidator : AbstractValidator<Command>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.Model.Name)
                .NotNull();

            RuleFor(x => x.Model.ImageUrl)
                .NotNull();
        }
    }
}
