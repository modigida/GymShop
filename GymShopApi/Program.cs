using GymShopApi.Database;
using GymShopApi.Repositories;
using GymShopApi.Repositories.Interfaces;
using GymShopApi.Services.Interfaces;
using GymShopApi.Services;
using Microsoft.EntityFrameworkCore;
using GymShopApi.Entities;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddScoped<IGenericService<Category>, CategoryService>();
builder.Services.AddScoped<IGenericService<Order>, OrderService>();
builder.Services.AddScoped<IGenericService<OrderProduct>, OrderProductService>();
builder.Services.AddScoped<IGenericService<OrderStatus>, OrderStatusService>();
builder.Services.AddScoped<IGenericService<Product>, ProductService>();
builder.Services.AddScoped<IGenericService<ProductStatus>, ProductStatusService>();
builder.Services.AddScoped<IGenericService<Role>, RoleService>();
builder.Services.AddScoped<IGenericService<User>, UserService>();

var app = builder.Build();

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
