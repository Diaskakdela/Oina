using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToysService.category.model;
using ToysService.category.repository;
using ToysService.category.service;
using ToysService.core;
using ToysService.toy.factory;
using ToysService.toy.model;
using ToysService.toy.repository;
using ToysService.toy.service;
using ToysService.toyreview.factory;
using ToysService.toyreview.repository;
using ToysService.toyreview.service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryMapper, CategoryMapper>();

builder.Services.AddScoped<IToyService, ToyService>();
builder.Services.AddScoped<IToyRepository, ToyRepository>();
builder.Services.AddScoped<ToyMapper, ToyMapper>();
builder.Services.AddScoped<ToyFactory, ToyFactory>();

builder.Services.AddScoped<IToyReviewService, ToyReviewService>();
builder.Services.AddScoped<IToyReviewRepository, ToyReviewRepository>();
builder.Services.AddScoped<ToyReviewFactory, ToyReviewFactory>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
