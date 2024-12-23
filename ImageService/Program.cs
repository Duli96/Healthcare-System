using Microsoft.EntityFrameworkCore;
using ImageService.Data;
using ImageService.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<ImageServiceDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29))));

builder.Services.AddScoped<IImageService, ImageServiceImpl>();

// Add OpenAPI / Swagger generation services
builder.Services.AddEndpointsApiExplorer(); // Replaces AddOpenApi
builder.Services.AddSwaggerGen(); // Swagger UI generator

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Replaces MapOpenApi
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
