using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Orders.Commands;

public static class DeleteOrder
{
    public record Command(Guid Id, string CustomerId) : ICommand<ApiResponse<NoContent>>;

    public class CommandHandler(IGenericRepository<Order.Core.Entities.Order> _orderRepository, IUnitOfWork _unitOfWork)
        : ICommandHandler<Command, ApiResponse<NoContent>>
    {
        public async Task<ApiResponse<NoContent>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existOrder= await _orderRepository.GetAsync(x=>x.Id==request.Id && x.CustomerId==request.CustomerId);

            if (existOrder is null)
                return ApiResponse<NoContent>.Fail("Order not found.", 404);

            await _orderRepository.DeleteAsync(existOrder);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<NoContent>.Success(204);
        }
    }
    public class DeleteOrderCommandValidator : AbstractValidator<Command>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}
