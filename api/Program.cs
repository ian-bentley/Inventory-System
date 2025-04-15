using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Newtonsoft.Json.Serialization;
using api.Data;
using api.Models;
using Microsoft.Extensions.Options;
using api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//JSON Serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// Email Sender
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, SendGridEmailSender>();

// Add EF Core with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=tcp:portfolio-ian-bentley.database.windows.net,1433;Initial Catalog=inventoryDB;Persist Security Info=False;User ID=portfolioadmin;Password=Pasw*750243*;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password options
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    // Lockout settings
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.AllowedForNewUsers = true;

    // SignIn settings
    options.SignIn.RequireConfirmedEmail = false;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Use cookie authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    //options.LoginPath = "/Account/Login"; // Redirect to login if not authenticated
    //options.LogoutPath = "/Account/Logout"; // Path to logout
    //options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect path for denied access
    options.SlidingExpiration = true; // Enable sliding expiration
    options.ExpireTimeSpan = TimeSpan.FromDays(1); // Cookie expiration time
    options.Cookie.HttpOnly = true; // Cookie should be accessible only via HTTP (not JavaScript)
    options.Cookie.SameSite = SameSiteMode.Strict; // Cookie SameSite mode
    options.Cookie.Name = "IventorySystem.AuthCookie";
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Enable CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

// Map minimal Identity API endpoints
app.MapIdentityApi<ApplicationUser>();


app.MapControllers();

app.Run();
