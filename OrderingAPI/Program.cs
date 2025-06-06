using Microsoft.EntityFrameworkCore;
using OrderingAPI.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OrderingDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Ordering API";
        options.HideClientButton = true;
        options.HideModels = true;
    });
}

app.UseHttpsRedirection();

PrepareDb.LoadData(app);

app.MapGet("/order/{userName}", GetOrders)
    .WithName("GetOrder");

app.MapPost("/order", CheckOutOrder)
    .WithName("CheckoutOrder");

app.MapPut("/order/{orderId}", UpdateOrder)
    .WithName("UpdateOrder");

app.MapDelete("/order/{orderId}",DeleteOrder)
    .WithName("DeleteOrder");

app.Run();


IEnumerable<Order> GetOrders(AppDbContext appDbContext, string userName)
{
    return appDbContext.Orders.Where(x => x.UserName == userName)
        .ToList();
}

int CheckOutOrder(AppDbContext appDbContext, Order order)
{
    appDbContext.Orders.Add(order);
    appDbContext.SaveChanges();

    return order.OrderId;
}

void UpdateOrder(AppDbContext appDbContext, int orderId, Order order)
{
    var existingOrder = appDbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
    if (existingOrder != null)
    {
        existingOrder.UserName = order.UserName;
        existingOrder.FirstName = order.FirstName;
        existingOrder.LastName = order.LastName;
        existingOrder.EmailAddress = order.EmailAddress;
        existingOrder.AddressLine = order.AddressLine;
        existingOrder.Country = order.Country;
        existingOrder.State = order.State;
        existingOrder.ZipCode = order.ZipCode;
        existingOrder.TotalPrice = order.TotalPrice;
    };
    appDbContext.SaveChanges();
}

void DeleteOrder(AppDbContext appDbContext, int orderId)
{
    var order = appDbContext.Orders.FirstOrDefault(x => x.OrderId == orderId);
    if (order != null)
    {
        appDbContext.Orders.Remove(order);
    }
    appDbContext.SaveChanges();
}