using GymShopBlazor;
using GymShopBlazor.ApiService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

await builder.Build().RunAsync();
