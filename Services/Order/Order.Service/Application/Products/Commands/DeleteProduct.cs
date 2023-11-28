using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Products.Commands;

public static class DeleteProduct
{
    public record Command(Guid Id) : ICommand<ApiResponse<NoContent>>;
    public class CommandHandler(IGenericRepository<Order.Core.Entities.Product> _productRepository, IUnitOfWork _unitOfWork) : ICommandHandler<Command, ApiResponse<NoContent>>
    {
        public async Task<ApiResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existProduct = await _productRepository.GetAsync(x=>x.Id == request.Id);
            if (existProduct == null)
                return ApiResponse<NoContent>.Fail("Product not found.", 404);
            await _productRepository.DeleteAsync(existProduct);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<NoContent>.Success(204);
        }
    }
    public class DeleteProductCommandValidator : AbstractValidator<Command>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
