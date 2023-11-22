using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Model.Requests.Product;
using Order.Model.Requests.Products;
using Order.Service.Application.Product.Queries;
using Order.Service.Application.Products.Commands;
using Order.Service.Application.Products.Queries;

namespace Order.API.Controllers;

public class ProductController : CustomBaseController
{
    public ProductController(IMediator _meditor) : base(_meditor)
    {
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return CreateActionResult(await _mediator.Send(new GetProducts.Query()));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new GetProductById.Query(id)));
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        return CreateActionResult(await _mediator.Send(new CreateProduct.Command(request)));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id,UpdateProductRequest request)
    {
        return CreateActionResult(await _mediator.Send(new UpdateProduct.Command(id, request)));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return CreateActionResult(await _mediator.Send(new DeleteProduct.Command(id)));
    }
}
