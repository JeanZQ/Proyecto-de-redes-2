using Microsoft.EntityFrameworkCore;
using Contaminados.Repositories.IRepository;
using Contaminados.Repositories.Repository;
using Contaminados.Application.Handlers;
using Models.gameModels;
using Contaminados.DB;
using Models.playersModels;
using Models.roundGroupModels;
using Models.roundModels;
using Models.roundVoteModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Inyección de dependencias Repositories----------------------------
builder.Services.AddScoped(typeof(IGameRepository<Game>), typeof(GameRepository));
builder.Services.AddScoped(typeof(IPlayerRepository<Players>), typeof(PlayerRepository));
builder.Services.AddScoped(typeof(IRoundGroupRepository<RoundGroup>), typeof(RoundGroupRepository));
builder.Services.AddScoped(typeof(IRoundRepository<Round>), typeof(RoundRepository));
builder.Services.AddScoped(typeof(IRoundVoteRepository<RoundVote>), typeof(RoundVoteRepository));
// -------------------------------------------------------------------

// Inyección de dependencias Handlers--------------------------------
// Game
builder.Services.AddScoped<CreateGameHandler>();
builder.Services.AddScoped<GetGameByIdByPasswordByPlayerHandler>();
// Players
builder.Services.AddScoped<CreatePlayerHandler>();
builder.Services.AddScoped<GetPlayerByIdHandler>();
builder.Services.AddScoped<GetGamesHandler>();
builder.Services.AddScoped<GetAllPlayersByGameIdHandler>();
// Round
builder.Services.AddScoped<CreateRoundHandler>();
builder.Services.AddScoped<GetAllRoundByGameIdHandler>();
builder.Services.AddScoped<GetRoundByIdHandler>();
builder.Services.AddScoped<UpdateRoundHandler>();
// RoundGroup
builder.Services.AddScoped<CreateRoundGroupHandler>();
builder.Services.AddScoped<GetAllRoundGroupByRoundIdHandler>();
// RoundVote
builder.Services.AddScoped<CreateRoundVoteHandler>();
builder.Services.AddScoped<GetAllRoundVoteByRoundIdHandler>();


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
