using FluentValidation;
using Mapster;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Model.Common;
using Order.Model.Requests.Orders;
using Order.Model.Responses.Orders;
using Order.Service.Common;

namespace Order.Service.Application.Orders.Commands;

public static class UpdateOrder
{
    public record Command(Guid Id,UpdateOrderRequest Model) : ICommand<ApiResponse<UpdatedOrderResponse>>;

    public class CommandHandler(IGenericRepository<Order.Core.Entities.Order> _orderRepository, IUnitOfWork _unitOfWork)
        : ICommandHandler<Command, ApiResponse<UpdatedOrderResponse>>
    {
        public async Task<ApiResponse<UpdatedOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var existOrder= await _orderRepository.GetAsync(x=>x.Id==request.Id);
            if (existOrder is null)
                return ApiResponse<UpdatedOrderResponse>.Fail("Order not found.",404);

            // TODO Buralar bi düzenlensin.

            existOrder.Status=request.Model.Status;
            existOrder.Quantity=request.Model.Quantity;
            existOrder.Price=request.Model.Price;
            existOrder.Adress.Id = request.Model.AdressId; // Customer Id'den Address'i çekelim
            existOrder.UpdatedAt=DateTime.UtcNow;
            
            await _orderRepository.UpdateAsync(existOrder);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<UpdatedOrderResponse>.Success(existOrder.Adapt<UpdatedOrderResponse>());
        }
    }
    public class UpdateOrderCommandValidator : AbstractValidator<Command>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();
            RuleFor(x => x.Model.AdressId)
         .NotNull();

            RuleFor(x => x.Model.ProductId)
                .NotNull();

            RuleFor(x => x.Model.CustomerId)
                .NotNull();

            RuleFor(x => x.Model.Status)
                .NotNull();

            RuleFor(x => x.Model.Price)
                .GreaterThanOrEqualTo(0)
                .NotNull();

            RuleFor(x => x.Model.Quantity)
                .NotNull();


        }
    }
}
