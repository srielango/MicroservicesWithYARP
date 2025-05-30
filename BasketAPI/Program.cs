using BasketAPI;
using DiscountGrpc.Protos;
using Scalar.AspNetCore;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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

List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
shoppingCarts.Add(new ShoppingCart()
{
    UserName = "Test",
    Items = new List<ShoppingCartItem>
    {
        new ShoppingCartItem { ProductName = "Samsung 10", Price = 100, Quantity = 1 },
        new ShoppingCartItem { ProductName = "IPhone X", Price = 200, Quantity = 2 }
    }
});

app.MapGet("/basket/{userName}", GetBasket)
    .WithName("GetBasket");

app.MapPost("/basket/{userName}", UpdateBasket)
    .WithName("UpdateBasket");

app.MapDelete("/basket/{userName}", DeleteBasket)
    .WithName("DeleteBasket");

app.MapPost("/basket/{userName}/checkout", CheckoutBasket)
    .WithName("CheckoutBasket");

app.Run();

ShoppingCart? GetBasket(string userName)
{
    return shoppingCarts.FirstOrDefault(x => x.UserName == userName);
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

void DeleteBasket(string userName)
{
    var cart = shoppingCarts.FirstOrDefault(x => x.UserName == userName);
    if (cart != null)
    {
        shoppingCarts.Remove(cart);
    }
}

void CheckoutBasket(string userName)
{
    var cart = GetBasket(userName);
    if (cart == null)
    {
        throw new InvalidOperationException("Basket not found");
    }
    //Publish an event or call a payment service to process the payment.
    
    DeleteBasket(userName);

    // Here you would typically process the payment and complete the order.
    // For this example, we'll just return the cart.
}