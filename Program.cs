using System.Text;
using instantBid.DBContext;
using instantBid.HelperServices;
using instantBid.Repositories.Implementations;
using instantBid.Repositories.Interfaces;
using instantBid.Services.Implementstions;
using instantBid.Services.Interfaces;
using instantBidBackend.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR
builder.Services.AddSignalR();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication Setup
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWTToken:Issuer"],
        ValidAudience = builder.Configuration["JWTToken:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWTToken:Key"]))
    };


    option.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/auctionHub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };

});

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConn"));
});

// Dependency Injection
builder.Services.AddScoped<IUserRepoInterface, UserRepo>();
builder.Services.AddScoped<IUserServiceInterface, UserService>();
builder.Services.AddScoped<JWTTokenService>();
builder.Services.AddScoped<IAuctionRepoInterface, AuctionRepo>();
builder.Services.AddScoped<IauctionServiceInterface, AuctionService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<IItemRepoInterface, ItemRepo>();
builder.Services.AddScoped<IItemServiceInterface, ItemServices>();

// CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://localhost:5173",
            "https://localhost:7119",
            "http://localhost:7119")
      .AllowAnyHeader()
      .AllowAnyMethod()
      .AllowCredentials();

    });
});

var app = builder.Build();

// Swagger setup (for dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware pipeline (ORDER IS VERY IMPORTANT)
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<AuctionHub>("/auctionHub");
app.Run();