using FluentValidation;
using Mapster;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Model.Requests.Product;
using Order.Model.Responses.Product;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Products.Commands;

public static class CreateProduct
{
    public record Command(CreateProductRequest Model):ICommand<ApiResponse<CreatedProductResponse>>;
    public class CommandHandler(IGenericRepository<Order.Core.Entities.Product> _productRepository,IUnitOfWork _unitOfWork) : ICommandHandler<Command, ApiResponse<CreatedProductResponse>>
    {
        public async Task<ApiResponse<CreatedProductResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var entity=request.Model.Adapt<Order.Core.Entities.Product>();
            await _productRepository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<CreatedProductResponse>.Success(entity.Adapt<CreatedProductResponse>(), 201);
        }
    }
    public class CreateProductCommandValidator : AbstractValidator<Command> {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Model.Name)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Model.ImageUrl)
                .NotNull()
                .NotEmpty();
        }
    } 
}
