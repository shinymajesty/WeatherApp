using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using WeatherApp.Services;
using Radzen;
using WeatherApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
    });

builder.Services.AddAuthenticationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<LocationState>();

// Add authorization
builder.Services.AddAuthorization();

// Add our services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddRadzenComponents();

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Add these development-specific settings
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // This might help capture more detailed error info
    Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(new WebApplicationOptions
    {
        EnvironmentName = "Development"
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();