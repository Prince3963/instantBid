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
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add SignalR
builder.Services.AddSignalR();

// ✅ Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ JWT Authentication Setup
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
});

// ✅ Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myConn"));
});

// ✅ Dependency Injection
builder.Services.AddScoped<IUserRepoInterface, UserRepo>();
builder.Services.AddScoped<IUserServiceInterface, UserService>();
builder.Services.AddScoped<JWTTokenService>();
builder.Services.AddScoped<IAuctionRepoInterface, AuctionRepo>();
builder.Services.AddScoped<IauctionServiceInterface, AuctionService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<IItemRepoInterface, ItemRepo>();
builder.Services.AddScoped<IItemServiceInterface, ItemServices>();

// ✅ CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ✅ Swagger setup (for dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Middleware pipeline (ORDER IS VERY IMPORTANT)
app.UseHttpsRedirection();

app.UseCors("frontend");

app.UseRouting(); // 🧩 This must come BEFORE endpoints

app.UseAuthentication();
app.UseAuthorization();

// ✅ Map controllers + hubs properly
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<AuctionHub>("/auctionHub"); // 🔥 SignalR hub endpoint
});

app.Run();
