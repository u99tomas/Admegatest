using AdMegasoft.Web.Authentication;
using Application;
using Application.Configurations;
using Blazored.LocalStorage;
using Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

#region Web
// Authorization
builder.Services.AddScoped<AuthenticationStateProvider, AdMegasoftAuthenticationStateProvider>();

// Nuget packages
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
#endregion

#region Application
builder.Services.AddApplication();

// Bad, should be in AddAdMegasoftApplication() method
var jwtSection = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSettings>(jwtSection);
// end Bad (=

#endregion

#region Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
