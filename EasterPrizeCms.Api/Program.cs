using EasterPrizeCms.Api.Data;
using EasterPrizeCms.Api.Repositories;
using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EasterPrizeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IPrizeRepository, PrizeRepository>();

builder.Services.AddScoped<ParticipantService>();
builder.Services.AddScoped<PrizeService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

public partial class Program { }
