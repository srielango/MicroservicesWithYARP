using CatalogAPI.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CatalogDb"));

var app = builder.Build();

builder.WebHost.UseUrls("http://+:80");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Catalog API";
        options.HideClientButton = true;
        options.HideModels = true;
    });
}

app.UseHttpsRedirection();

PrapareDb.LoadData(app);

app.MapGet("/products", GetProducts)
    .WithName("GetProducts");

app.MapGet("/products/{id:int}", GetProductById)
    .WithName("GetProductById");

app.MapGet("/products/category/{category}", GetProductByCategory)
    .WithName("GetProductsByCategory");

app.MapPost("/products", CreateProduct)
    .WithName("CreateProduct");

app.MapPut("/products/{id:int}", UpdateProduct)
    .WithName("UpdateProduct");

app.MapDelete("/products/{id:int}", DeleteProduct)
    .WithName("DeleteProduct");

app.Run();

IResult GetProducts(AppDbContext appDbContext)
{
    return Results.Ok(appDbContext.Products.ToList());
}

IResult GetProductById(AppDbContext appDbContext, int id)
{
    return Results.Ok(appDbContext.Products.FirstOrDefault(p => p.Id == id));
}

IResult GetProductByCategory(AppDbContext appDbContext, string category)
{
    return Results.Ok(appDbContext.Products
        .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
        .ToList());
}

IResult CreateProduct(AppDbContext appDbContext, Product product)
{
    appDbContext.Products.Add(product);
    appDbContext.SaveChanges();
    return Results.Created($"/products/{product.Id}", product);
}

IResult UpdateProduct(AppDbContext appDbContext, int id, Product updatedProduct)
{
    var product = appDbContext.Products.FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        throw new KeyNotFoundException($"Product with ID {id} not found.");
    }
    product.Name = updatedProduct.Name;
    product.Category = updatedProduct.Category;
    product.Summary = updatedProduct.Summary;
    product.Description = updatedProduct.Description;
    product.ImageFile = updatedProduct.ImageFile;
    product.Price = updatedProduct.Price;
    appDbContext.SaveChanges();
    return Results.Ok(product);
}

IResult DeleteProduct(AppDbContext appDbContext, int id)
{
    var product = appDbContext.Products.FirstOrDefault(p => p.Id == id);
    if (product == null)
    {
        throw new KeyNotFoundException($"Product with ID {id} not found.");
    }
    appDbContext.Products.Remove(product);
    appDbContext.SaveChanges();

    return Results.NoContent();
}