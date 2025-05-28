using BasketAPI;
using Scalar.AspNetCore;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

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

ShoppingCart? UpdateBasket(string userName, ShoppingCart basket)
{
    //TODO: Apply discount
    var cart = shoppingCarts.FirstOrDefault(x => x.UserName == userName);
    if (cart == null)
    {
        cart = new ShoppingCart(userName);
        shoppingCarts.Add(cart);
    }
    else
    {
        cart = basket;
    }
    return cart;
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