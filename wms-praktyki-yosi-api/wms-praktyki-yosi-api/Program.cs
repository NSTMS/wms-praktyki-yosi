using Microsoft.AspNetCore.Builder;
using wms_praktyki_yosi_api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IProductService, ProductService>();

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
