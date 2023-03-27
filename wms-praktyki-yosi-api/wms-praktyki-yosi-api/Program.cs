using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using wms_praktyki_yosi_api;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Models.Validators;
using Microsoft.EntityFrameworkCore;
using wms_praktyki_yosi_api.Middleweare;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMvc();

var connectionString = new ConnectionsStrings();
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionString);


//               Singletones services
builder.Services.AddSingleton(connectionString);

builder.Services.AddDbContext<MagazinesDbContext>(options =>
    options.UseSqlServer(connectionString.database));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MagazinesDbContext>()
    .AddDefaultTokenProviders();
//                Scope services

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMagazineService, MagazineService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<ErrorHandlingMiddleweare>();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(options
    => options.SuppressModelStateInvalidFilter = true);

//            Authentication loading
var authenticationSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddCors();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleweare>();

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
