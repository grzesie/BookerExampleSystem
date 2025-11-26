using BookerAPI;
using BookerAPI.Database;
using BookerAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDatabase, BookerDatabase>();
builder.Services.AddScoped<BookerEngine>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/bookings/book", (BookingRequest request, BookerEngine engine) =>
{
    int id = engine.BookEvent(request.who, request.when);

    if (id > 0) return Results.Ok(new BookingResponse(id));

    return Results.Conflict(new BookingResponse(id));
});

app.Run();

// For WebApplicationFactory
public partial class Program;
