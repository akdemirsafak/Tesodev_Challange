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

    public class CommandHandler : ICommandHandler<Command, ApiResponse<CreatedOrderResponse>>
    {
        private readonly IGenericRepository<Order.Core.Entities.Order> _orderRepository;
        private readonly IGenericRepository<Order.Core.Entities.Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly HttpClient _httpClient;

        public CommandHandler(
            IGenericRepository<Core.Entities.Order> orderRepository, 
            IGenericRepository<Core.Entities.Product> productRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<CreatedOrderResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var product= await _productRepository.GetAsync(x=>x.Id==request.Model.ProductId);
            if (product is null)
                return ApiResponse<CreatedOrderResponse>.Fail("Böyle bir ürün bulunamadı.", 404);
            var entityModel= request.Model.Adapt<Order.Core.Entities.Order>();
            
            entityModel.CustomerId = Guid.Parse(request.CustomerId);
            entityModel.Product= product;

            //var address = await _httpClient.GetFromJsonAsync<ApiResponse<GetUserAddressResponse>>("https://localhost:7018/Address");
            //var addressContent=address.Data; 

            entityModel.Adress = new Core.Entities.Address { Id = request.Model.AdressId,Country="Turkey",City="İstanbul",CityCode=34,Line="Random" };

            var createdResult=await _orderRepository.CreateAsync(entityModel);
    
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<CreatedOrderResponse>.Success(createdResult.Adapt<CreatedOrderResponse>(), 201);
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
