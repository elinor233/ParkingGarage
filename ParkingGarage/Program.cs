using DB.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.Services;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options =>
    {
      options.AddPolicy("AllowFrontend", policy =>
      {
        policy
          .WithOrigins("http://localhost:3000")
          .AllowAnyMethod()
          .AllowAnyHeader();
      });
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

    builder.Services.AddDbContext<GarageContext>(options =>
      options.UseSqlServer("Server=(local)\\DYNAMTECH;Database=ParkingGarage;Trusted_Connection=True;TrustServerCertificate=True"));

    builder.Services.AddScoped<ParkingService>();
    builder.Services.AddScoped<TicketsService>();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowFrontend");

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
  }
}
