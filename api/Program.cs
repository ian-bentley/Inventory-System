using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using api.Data;
using api.Models;
using api.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

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

//JWT
var jwtKey = builder.Configuration["Jwt:Key"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key
    };
});

// CORS
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
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
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Map minimal Identity API endpoints
app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();
