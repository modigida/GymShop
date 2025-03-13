using GymShopApi.Database;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;
using GymShopApi.Services;
using Microsoft.EntityFrameworkCore;
using GymShopApi.Entities;
using GymShopApi.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GymShopApi.DTOs;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<JwtService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Interfaces
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IGenericRepository<CampaignProduct>, CampaignProductRepository>();
builder.Services.AddScoped<IGenericRepository<Campaign>, CampaignRepository>();
builder.Services.AddScoped<IGenericRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IGenericRepository<OrderStatus>, OrderStatusRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IGenericRepository<ProductStatus>, ProductStatusRepository>();
builder.Services.AddScoped<IGenericRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IGenericService<CampaignProduct>, CampaignProductService>();
builder.Services.AddScoped<IGenericService<Campaign>, CampaignService>();
builder.Services.AddScoped<IGenericService<Category>, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IGenericService<OrderProduct>, OrderProductService>();
builder.Services.AddScoped<IGenericService<OrderStatus>, OrderStatusService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IGenericService<ProductStatus>, ProductStatusService>();
builder.Services.AddScoped<IGenericService<Role>, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("https://localhost:7130")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowBlazor");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
