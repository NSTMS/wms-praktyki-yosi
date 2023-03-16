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


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = new ConnectionsStrings();
builder.Configuration.GetSection("ConnectionStrings").Bind(connectionString);


//               Singletones services
builder.Services.AddSingleton(connectionString);


//                Scope services
builder.Services.AddScoped<MagazinesDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
builder.Services.AddCors();
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

app.UseAuthorization();

app.MapControllers();

app.Run();
