using OrderingAPI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

var Orders = new List<Order>();

app.MapGet("/order/{userName}", GetOrders)
    .WithName("GetOrder");

app.MapPost("/order", CheckOutOrder)
    .WithName("CheckoutOrder");

app.MapPut("/order/{id}",UpdateOrder)
    .WithName("UpdateOrder");

app.MapDelete("/order/{id}",DeleteOrder)
    .WithName("DeleteOrder");

app.Run();


IEnumerable<Order> GetOrders(string userName)
{
    if(Orders.Count == 0)
    {
        Orders.Add(new Order() {
            OrderId = 1,
            UserName = userName,
            FirstName = "Elango",
            LastName = "Srinivasan",
            EmailAddress = "srielango@gmail.com",
            AddressLine = "Chennai",
            Country = "India",
            State ="TN",
            ZipCode = "123456",
            TotalPrice = 350
        });
    }

    return Orders.Where(x => x.UserName == userName)
        .ToList();
}

int CheckOutOrder(Order order)
{
    order.OrderId = Orders.Count() + 1;
    Orders.Add(order);

    return order.OrderId;
}

void UpdateOrder(int orderId, Order order)
{
    var existingOrder = Orders.FirstOrDefault(x => x.OrderId == orderId);
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
    }
}

void DeleteOrder(int orderId)
{
    var order = Orders.FirstOrDefault(x => x.OrderId == orderId);
    if (order != null)
    {
        Orders.Remove(order);
    }
}