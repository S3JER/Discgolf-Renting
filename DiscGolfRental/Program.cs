using DiscGolfRental.Db;
using DiscGolfRental.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DiscDatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/available-discs", async (DiscDatabaseContext db) =>
{
    return await db.Discs
        .Where(d => !db.Rentals.Any(r => r.DiscId == d.Id && r.DueDate >= DateTime.Now))
        .ToListAsync();
});

app.MapPost("/create-rental", async (Rental rental, DiscDatabaseContext db) =>
{
    if (rental.DueDate > rental.RentalDate.AddDays(3))
    {
        return Results.BadRequest("Lejeperioden kan ikke være længere end 3 dage.");
    }

    db.Rentals.Add(rental);
    await db.SaveChangesAsync();
    return Results.Created($"/rentals/{rental.Id}", rental);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
