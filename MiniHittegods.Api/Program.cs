using MiniHittegods.Api.Repositories;
using MiniHittegods.Application.Interfaces;
using MiniHittegods.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IFoundItemRepository, InMemoryFoundItemRepository>();
builder.Services.AddScoped<FoundItemsService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

public partial class Program { }