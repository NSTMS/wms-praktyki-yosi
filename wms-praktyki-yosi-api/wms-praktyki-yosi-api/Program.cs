using wms_praktyki_yosi_api.Services;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});
builder.Services.AddControllers();

builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
