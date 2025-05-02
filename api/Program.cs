using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using Newtonsoft.Json.Serialization;
using api.Data;
using api.Models;
using api.Services;
using System.Net.Mail;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

//Enable CORS
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        // Allow specific origins as defined in configuration
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();  // Allow credentials (cookies)
    });
});

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
    options.SignIn.RequireConfirmedEmail = true;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Use cookie authentication
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(options =>
{
    options.SlidingExpiration = true; // Enable sliding expiration
    options.ExpireTimeSpan = TimeSpan.FromDays(1); // Cookie expiration time
    options.Cookie.HttpOnly = true; // Cookie should be accessible only via HTTP (not JavaScript)
    options.Cookie.SameSite = SameSiteMode.None; // Cookie SameSite mode for cross-origin requests
    options.Cookie.Name = "InventorySystem.AuthCookie";

    // Set SecurePolicy based on the environment
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    if (environment == Environments.Production)
    {
        // In production, require cookies to be sent over HTTPS
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    }
    else
    {
        // For local development (HTTP), set SecurePolicy to None
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    }
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewEmployees", policy => 
        policy.RequireAssertion(context =>
            context.User.HasClaim("ViewEmployees", "true")));

    options.AddPolicy("EditEmployees", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("EditEmployees", "true")));

    options.AddPolicy("ViewInventory", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("ViewInventory", "true")));

    options.AddPolicy("EditInventory", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("EditInventory", "true")));

    options.AddPolicy("ViewSecurity", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("ViewSecurity", "true")));

    options.AddPolicy("EditSecurity", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("EditSecurity", "true")));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//JSON Serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
    options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Email Sender
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();
builder.Services.AddSingleton(new SmtpClient("smtp.gmail.com")
{
    Port = 587,
    Credentials = new NetworkCredential("ibentley981203@gmail.com", "kfhv znfq csvy uszx"),
    EnableSsl = true
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Map minimal Identity API endpoints
app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();
