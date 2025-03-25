using Blazored.LocalStorage;
using GymShopBlazor;
using GymShopBlazor.ApiService;
using GymShopBlazor.AuthService;
using GymShopBlazor.Event;
using GymShopBlazor.Layout;
using GymShopBlazor.Pages;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7097/") });

builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<Checkout>();
builder.Services.AddScoped<MainLayout>();
builder.Services.AddScoped<AuthenticationStateNotifier>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
