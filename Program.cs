using ReactMongoDB_API_01.Models;
using ReactMongoDB_API_01.Services;
using ReactMongoDB_API_01.Controllers;

var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.Configure<ElementDatabaseSettings>(
    builder.Configuration.GetSection("MaDatabaseMongo"));

builder.Services.AddSingleton<ElementsService>();

builder.Services.AddControllers();
    //.AddJsonOptions(
    //    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
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

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

//app.MapElementEndpoints();

app.Run();
