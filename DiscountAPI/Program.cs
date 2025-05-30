using DiscountAPI;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Discount API";
        options.HideClientButton = true;
        options.HideModels = true;
    });
}

List<Coupon> coupons = new List<Coupon>
{
    new Coupon { Id = 1, ProductName = "ProductA", Description = "10 off", Amount = 10 },
    new Coupon { Id = 2, ProductName = "ProductB", Description = "20 off", Amount = 20 }
};

app.MapGet("/discount/{productName:string}", GetDiscount)
    .WithName("GetDiscount");

app.MapPost("/discount", CreateDiscount)
    .WithName("CreateDiscount");

app.MapPut("/discount", UpdateDiscount)
    .WithName("UpdateDiscount");

app.MapDelete("/discount", DeleteDiscount)
    .WithName("DeleteDiscount");

bool DeleteDiscount(string productName)
{
    var coupon = GetDiscount(productName);
    if (coupon != null && coupon.Id > 0)
    {
        coupons.Remove(coupon);
        return true;
    }
    return false;
}

bool UpdateDiscount(Coupon coupon)
{
    var existingCoupon = coupons.FirstOrDefault(x => x.Id == coupon.Id);
    if (existingCoupon != null)
    {
        existingCoupon.ProductName = coupon.ProductName;
        existingCoupon.Description = coupon.Description;
        existingCoupon.Amount = coupon.Amount;
        return true;
    }
    return false;
}

Coupon GetDiscount(string productName)
{
    return coupons.FirstOrDefault(x => x.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase))
           ?? new Coupon { ProductName = productName, Description = "No discount available", Amount = 0 };
}

bool CreateDiscount(Coupon coupon)
{
    coupons.Add(coupon);
    return true;
}

app.UseHttpsRedirection();

app.Run();
