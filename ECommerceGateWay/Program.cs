using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

var users = new Dictionary<string, (string Password, string Name)>(StringComparer.OrdinalIgnoreCase);


var app = builder.Build();

app.MapPost("/api/auth/register", async (UserRegister request) =>
{
    if (users.ContainsKey(request.Email))
    {
        return Results.BadRequest("User already exists.");
    }

    users[request.Email] = (request.Password, request.Name);
    return Results.Ok("User registered successfully.");
});

app.MapGet("/api/auth/login", (string email, string password) =>
{
    if(!users.TryGetValue(email, out var user) || user.Password != password)
    {
        return Results.Unauthorized();
    }

    var claims = new List<Claim>
    {
        new Claim("sub", email),
        new Claim("name", user.Name),
        new Claim("basket-api-access", true.ToString()),
        new Claim("order-api-access", true.ToString())
    };

    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, BearerTokenDefaults.AuthenticationScheme));

    return Results.SignIn(principal, authenticationScheme: BearerTokenDefaults.AuthenticationScheme);
});

app.MapGet("/api/auth/me", [Authorize] ([FromServices] ClaimsPrincipal user) =>
{
    var email = user.FindFirst("sub")?.Value;
    var name = user.FindFirst("name")?.Value;

    return Results.Ok(new { email, name });
});

app.MapGet("/", () => "Hello World!");

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();

app.Run();
