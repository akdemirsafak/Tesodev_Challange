﻿namespace Order.Model.Responses.Product;

public class GetProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}
