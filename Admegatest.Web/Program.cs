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
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

#region Authentication and authorization services
builder.Services.AddAuthorization();
builder.Services.AddScoped<AuthenticationStateProvider, AdmegatestAuthenticationStateProvider>();
#endregion

#region Services of Admegatest.Services project
builder.Services.AddScoped<IUserService, UserService>();
#endregion

#region Configuring services for authentication and authorization

var jwtSection = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSettings>(jwtSection);

//to validate the token which has been sent by clients
var appSettings = jwtSection.Get<JWTSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
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
