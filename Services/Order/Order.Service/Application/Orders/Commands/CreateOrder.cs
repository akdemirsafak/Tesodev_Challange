using FluentValidation;
using Mapster;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Model.Requests.Orders;
using Order.Model.Responses.Orders;
using Order.Service.Common;
using Shared.Library;

namespace Order.Service.Application.Orders.Commands;

public static class CreateOrder
{
    public record Command(CreateOrderRequest Model, string CustomerId) : ICommand<ApiResponse<CreatedOrderResponse>>;

    public class CommandHandler(IGenericRepository<Order.Core.Entities.Order> _orderRepository, IUnitOfWork _unitOfWork,HttpClient _httpClient)
        : ICommandHandler<Command, ApiResponse<CreatedOrderResponse>>
    {
        public async Task<ApiResponse<CreatedOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var response=await _orderRepository.CreateAsync(request.Model.Adapt<Order.Core.Entities.Order>());
            response.CustomerId = request.CustomerId;

            //var address = await _httpClient.GetFromJsonAsync<ApiResponse<GetUserAddressResponse>>("https://localhost:7018/Address");
            //var addressContent=address.Data;

            response.Adress = new Core.Entities.Address { Id = request.Model.AdressId };
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<CreatedOrderResponse>.Success(response.Adapt<CreatedOrderResponse>(), 201);
        }
    }
    public class CreateOrderCommandValidator : AbstractValidator<Command>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Model.AdressId)
                .NotNull();

            RuleFor(x => x.Model.ProductId)
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
