using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("YARP"));

builder.Services
    .AddAuthentication(BearerTokenDefaults.AuthenticationScheme)
    .AddBearerToken();

builder.Services
    .AddAuthorization(o =>
    {
        o.AddPolicy("basket-api-access",
            policy =>
            {
                policy.RequireAuthenticatedUser()
                .RequireClaim("basket-api-access", true.ToString());
            });

        o.AddPolicy("order-api-access",
            policy =>
            {
                policy.RequireAuthenticatedUser()
                .RequireClaim("order-api-access", true.ToString());
            });
    });

var app = builder.Build();

app.MapGet("login", (bool basketApi = false, bool orderApi = false) =>
    Results.SignIn(
        new ClaimsPrincipal(
            new ClaimsIdentity(
                [
                    new Claim("sub", Guid.NewGuid().ToString()),
                    new Claim("basket-api-access", basketApi.ToString()),
                    new Claim("order-api-access", orderApi.ToString())
                ],
                BearerTokenDefaults.AuthenticationScheme)),
        authenticationScheme: BearerTokenDefaults.AuthenticationScheme ));

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
