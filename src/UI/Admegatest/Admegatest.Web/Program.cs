using Admegatest.Configuration.Account;
using Admegatest.Data.DbContexts;
using Admegatest.Services.Interfaces.Account;
using Admegatest.Services.Services.Account;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

#region Database services
builder.Services.AddDbContext<AdmegatestDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdmegatestDb")));
#endregion

#region NuGet packages services
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
#endregion

#region Account services
builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, AdmegatestAuthenticationStateProvider>();
#endregion

#region Admegatest services
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Account services configuration
var jwtSection = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSettings>(jwtSection);
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
