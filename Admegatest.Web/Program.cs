using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Admegatest.Services.Authentication;
using Blazored.LocalStorage;
using Admegatest.Services.IServices;
using Admegatest.Services.Services;
using Admegatest.Data;
using Microsoft.EntityFrameworkCore;
using Admegatest.Core.Models.AuthenticationAndAuthorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

#region Services of NuGet packages
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
#endregion

#region Services related to databases
builder.Services.AddDbContext<AdmegatestDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdmegatestDb")));
#endregion

#region Services for authentication and authorization
builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, AdmegatestAuthenticationStateProvider>();
var jwtSection = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSettings>(jwtSection);
#endregion

#region Services of Admegatest.Services project
builder.Services.AddScoped<IUserService, UserService>();
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
