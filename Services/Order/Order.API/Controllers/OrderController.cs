using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Model.Requests.Orders;
using Order.Service.Application.Orders.Commands;
using Order.Service.Application.Orders.Queries;
using Shared.Library.CustomControllerBase;
using Shared.Library.Helper;

namespace Order.API.Controllers;

[Authorize]
public class OrderController : CustomBaseController
{
    private readonly IMediator _mediator;
    private readonly ICurrentUser _currentUser;

    public OrderController(IMediator mediator, ICurrentUser currentUser)
    {
        _mediator = mediator;
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetOrders.Query(_currentUser.GetUserId)));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetOrderById.Query(id, _currentUser.GetUserId)));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateOrder.Command(request, _currentUser.GetUserId)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateOrderRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateOrder.Command(id, request, _currentUser.GetUserId)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteOrder.Command(id, _currentUser.GetUserId)));
    }
}
