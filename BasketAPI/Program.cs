using BasketAPI;
using BasketAPI.Data;
using DiscountGrpc.Protos;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BasketDb"));

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
builder.Services.AddScoped<DiscountGrpcService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Basket API";
        options.HideClientButton = true;
        options.HideModels = true;
    });
}

app.UseHttpsRedirection();

PrepareDb.LoadData(app);

app.MapGet("/basket/{userName}", GetBasket)
    .WithName("GetBasket");

app.MapPost("/basket/{userName}", UpdateBasket)
    .WithName("UpdateBasket");

app.MapDelete("/basket/{userName}", DeleteBasket)
    .WithName("DeleteBasket");

app.MapPost("/basket/{userName}/checkout", CheckoutBasket)
    .WithName("CheckoutBasket");

app.Run();

ShoppingCart? GetBasket(AppDbContext appDbContext, string userName)
{
    return appDbContext.ShoppingCarts
        .Include(x => x.Items)
        .FirstOrDefault(x => x.UserName == userName);
}

async Task<ShoppingCart?> UpdateBasket(DiscountGrpcService discountGrpcService, ShoppingCart basket)
{
    foreach(var item in basket.Items)
    {
        var coupon = await discountGrpcService.GetDiscountAsync(item.ProductName);
        item.Price -= coupon.Amount;
    }
    return basket;
}

void DeleteBasket(AppDbContext appDbContext, string userName)
{
    var cart = appDbContext.ShoppingCarts.FirstOrDefault(x => x.UserName == userName);
    if (cart != null)
    {
        appDbContext.ShoppingCarts.Remove(cart);
    }
}

void CheckoutBasket(AppDbContext appDbContext, string userName)
{
    var cart = GetBasket(appDbContext, userName);
    if (cart == null)
    {
        throw new InvalidOperationException("Basket not found");
    }
    //Publish an event or call a payment service to process the payment.
    
    DeleteBasket(appDbContext, userName);

    // Here you would typically process the payment and complete the order.
    // For this example, we'll just return the cart.
}