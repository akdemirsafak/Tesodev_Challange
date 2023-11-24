using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Model.Requests.Orders;
using Order.Service.Application.Orders.Commands;
using Order.Service.Application.Orders.Queries;

namespace Order.API.Controllers;

public class OrderController : CustomBaseController
{
    public OrderController(IMediator _meditor) : base(_meditor)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetOrders.Query()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetOrderById.Query(id)));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateOrder.Command(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id,UpdateOrderRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateOrder.Command(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteOrder.Command(id)));
    }
}
