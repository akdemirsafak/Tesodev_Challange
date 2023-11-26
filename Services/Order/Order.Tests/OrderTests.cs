using NSubstitute;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;

namespace Order.Tests;

public class OrderTests
{
    private readonly IGenericRepository<Order.Core.Entities.Order> _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    public OrderTests()
    {
        _orderRepository = Substitute.For<IGenericRepository<Order.Core.Entities.Order>>(); 
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

}
