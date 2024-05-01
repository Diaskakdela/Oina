using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserServiceOina.config;
using UserServiceOina.events.listeners;
using UserServiceOina.factory;
using UserServiceOina.kafka;
using UserServiceOina.mapper;
using UserServiceOina.repository;
using UserServiceOina.repository.impl;
using UserServiceOina.service;
using UserServiceOina.service.impl;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<IUserFactory, UserFactory>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRenterService, RenterService>();
builder.Services.AddScoped<IRenterRepository, RenterRepository>();
builder.Services.AddScoped<IRenterFactory, RenterFactory>();
builder.Services.AddScoped<KafkaProducer>();
builder.Services.AddScoped<KafkaListener>(sp => {
    var userService = sp.GetRequiredService<IUserService>();
    var kafkaProducer = sp.GetRequiredService<KafkaProducer>();
    var logger = sp.GetRequiredService<ILogger<KafkaListener>>();
    return new KafkaListener(userService, kafkaProducer, logger);
});
builder.Services.AddControllers();
builder.Services.AddLogging(configure => configure.AddConsole())
    .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

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
var scope = app.Services.CreateScope();
var kafkaListener = scope.ServiceProvider.GetRequiredService<KafkaListener>();
kafkaListener.SubscribeToEvents();

app.Run();