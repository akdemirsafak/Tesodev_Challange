using Mapster;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Order.Core.Entities;
using Order.Core.Repositories;
using Order.Core.UnifOfWorks;
using Order.Model.Requests.Product;
using Order.Model.Requests.Products;
using Order.Model.Responses.Product;
using Order.Service.Application.Product.Queries;
using Order.Service.Application.Products.Commands;
using Order.Service.Application.Products.Queries;
using System.Linq.Expressions;

namespace Order.Tests;

public class ProductTests
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ProductTests()
    {
        _productRepository=Substitute.For<IGenericRepository<Product>>();
        _unitOfWork=Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task GetProducts_Should_StatusOk()
    {
        //Arrange
        List<Product> productList = new()
        {
            new Product()
            {
                Id=new Guid(),
                Name="Computer",
                ImageUrl="laptop.png"
            },
            new Product()
            {
                Id=new Guid(),
                Name="Phone",
                ImageUrl="iphone.jpg"
            }
        };

        _productRepository.GetAllAsync().Returns(Task.FromResult(productList));
        var productListRepositoryResponse= productList.Adapt<List<GetProductResponse>>();

        var query= new GetProducts.Query();
        var queryHandler= new GetProducts.QueryHandler(_productRepository);

        //Act

        var handlerResponse= await queryHandler.Handle(query,CancellationToken.None);

        //Assert

        Assert.NotNull(handlerResponse.Data);
        Assert.True(handlerResponse.StatusCode == StatusCodes.Status200OK);
        Assert.Equivalent(productListRepositoryResponse, handlerResponse.Data);
    }

    [Fact]
    public async Task GetProductById_Should_Return_When_Found()
    {
        //Arrange
        var computer=new Product()
        {
            Id=Guid.NewGuid(),
            Name="Computer",
            ImageUrl="laptop.png"
        };

        _productRepository.GetAsync(Arg.Any<Expression<Func<Product, bool>>>()).Returns(Task.FromResult(computer));
        var productRepositoryResponse= computer.Adapt<GetProductResponse>();

        var query= new GetProductById.Query(computer.Id);
        var queryHandler= new GetProductById.QueryHandler(_productRepository);

        //Act

        var handlerResponse= await queryHandler.Handle(query,CancellationToken.None);

        //Assert

        Assert.NotNull(handlerResponse.Data);
        Assert.True(handlerResponse.StatusCode == StatusCodes.Status200OK);
        Assert.Equivalent(productRepositoryResponse, handlerResponse.Data);
    }

    [Fact]
    public async Task CreateProduct_Should_SuccessWith201StatusCode_WhenCreated()
    {
        //Assert

        var product = new Product()
        {
            Name = "Iphone",
            ImageUrl = "apple.img"
        };
        _productRepository.CreateAsync(Arg.Any<Product>()).Returns(Task.FromResult(product));
        var createdProducResponse=product.Adapt<CreatedProductResponse>();
    

        var request = new CreateProductRequest(product.Name, product.ImageUrl);
        var command = new CreateProduct.Command(request);
        var commandHandler = new CreateProduct.CommandHandler(_productRepository, _unitOfWork);

        //Act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert

        Assert.NotNull(result.Data);
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == StatusCodes.Status201Created);
        Assert.Equivalent(result.Data, createdProducResponse);
    }
    [Fact]
    public async Task UpdateProduct_Should_SuccessWith200StatusCode_WhenUpdated()
    {
        var product = new Product()
        {
            Id=Guid.NewGuid(),
            Name = "Iphone",
            ImageUrl = "apple.img"
        };

        var newProductValues = new Product
        {
            Name = "Samsung",
            ImageUrl = "s50.jpeg"
        };

        var existProduct= _productRepository.GetAsync(Arg.Any<Expression<Func<Product, bool>>>()).Returns(Task.FromResult(product));
        _productRepository.UpdateAsync(Arg.Any<Product>()).Returns(Task.FromResult(newProductValues));
        var updatedProductResponse= new UpdatedProductResponse
        {
            Id=product.Id,
            Name=newProductValues.Name,
            ImageUrl=newProductValues.ImageUrl
        };

        var request = new UpdateProductRequest(newProductValues.Name, newProductValues.ImageUrl);
        var command = new UpdateProduct.Command(product.Id, request);
        var commandHandler = new UpdateProduct.CommandHandler(_productRepository, _unitOfWork);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == StatusCodes.Status200OK);
        Assert.NotNull(result.Data);
        Assert.Equivalent(updatedProductResponse, result.Data);

    }

    [Fact]
    public async Task DeleteProduct_Should_SuccessWithNoContent204StatusCode_WhenDeleted()
    {
        var product = new Product()
        {
            Id=Guid.NewGuid(),
            Name = "Iphone",
            ImageUrl = "apple.img"
        };

        var existProduct= _productRepository.GetAsync(Arg.Any<Expression<Func<Product, bool>>>()).Returns(Task.FromResult(product));
        _productRepository.DeleteAsync(Arg.Any<Product>()).Returns(Task.FromResult(existProduct));

        var command = new DeleteProduct.Command(product.Id);
        var commandHandler = new DeleteProduct.CommandHandler(_productRepository, _unitOfWork);

        //Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        //Assert
        Assert.Null(result.Errors);
        Assert.True(result.StatusCode == StatusCodes.Status204NoContent);
    }
}
