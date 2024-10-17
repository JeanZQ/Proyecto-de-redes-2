using Microsoft.EntityFrameworkCore;
using Contaminados.Repositories.IRepository;
using Contaminados.Repositories.Repository;
using Contaminados.Aplication.Handlers;
using Models.gameModels;
using Contaminados.DB;
using Models.playersModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Inyección de dependencias Repositories----------------------------
builder.Services.AddScoped(typeof(IGameRepository<Game>), typeof(GameRepository));
builder.Services.AddScoped(typeof(IPlayerRepository<Players>), typeof(PlayerRepository));

// Inyección de dependencias Handlers--------------------------------
builder.Services.AddScoped<CreateGameHandler>();
builder.Services.AddScoped<CreatePlayerHandler>();
builder.Services.AddScoped<GetGameByIdByPasswordByPlayerHandler>();
builder.Services.AddScoped<GetPlayerByIdHandler>();
builder.Services.AddScoped<GetPlayersByGameIdHandler>();
builder.Services.AddScoped<GetGamesHandler>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbContextClass>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
