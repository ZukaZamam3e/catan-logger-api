using CatanLoggerData.Context;
using CatanLoggerStore.Repositories;
using CatanLoggerStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("CatanLoggerConnection");
builder.Services.AddDbContext<CatanLoggerDbContext>(m => m.UseSqlServer(connectionString), ServiceLifetime.Transient);
builder.Services.AddScoped<IGameRepository, GameRepository>();

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("https://catan-logger.azurewebsites.net", "http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

    //options.AddDefaultPolicy(builder =>
    //    builder.WithOrigins("http://localhost:3000/", "https://catan-logger.azurewebsites.net/")
    //        .SetIsOriginAllowedToAllowWildcardSubdomains()
    //        .WithMethods("GET", "POST")
    //        .AllowAnyHeader());

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

//app.UseCors();
app.UseCors(MyAllowSpecificOrigins);

//app.UseAuthorization();



app.MapControllers();

app.Run();
