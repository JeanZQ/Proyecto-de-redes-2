using Contaminados.Aplication.Handlers;
using Contaminados.Aplication.Queries;
using Contaminados.Aplication.Commands;
using Microsoft.AspNetCore.Mvc;
using Contaminados.Models.Common;
using Models.gameModels;
using Models.playersModels;

namespace Contaminados.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GetGameByIdByPasswordByPlayerHandler _getGameByIdByPasswordByOwnerHandler;
        private readonly CreateGameHandler _createGameHandler;
        private readonly GetPlayersByGameIdHandler _getPlayersByGameIdHandler;
        private readonly GetGamesHandler _getGamesHandler;
        public GameController(
            GetGameByIdByPasswordByPlayerHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetPlayersByGameIdHandler getPlayersByGameIdHandler,
            GetGamesHandler getGamesHandler
            )
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getPlayersByGameIdHandler = getPlayersByGameIdHandler;
            _getGamesHandler = getGamesHandler;
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(Guid gameId, string? password, string player)
        {
            try
            {
                var query = new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player);
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(query);
                var players = await _getPlayersByGameIdHandler.HandleAsync(new GetPlayersByGameIdQuery(gameId));

                var result = CreateResult(game, players);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message, status = ex.Status });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
        {
            try
            {
                var game = await _createGameHandler.HandleAsync(command);
                var players = await _getPlayersByGameIdHandler.HandleAsync(new GetPlayersByGameIdQuery(game.Id));

                var result = CreateResult(game, players);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message, status = ex.Status });
            }
        }

        //No hacer el metodo ASYNC ni llamar a ningun Handler
        private StatusCodesOk CreateResult(Game game, IEnumerable<Players> players)
        {
            return new StatusCodesOk
            {
                Status = 200,
                Msg = "Game Found",
                Data = new Data
                {
                    Id = game.Id.ToString(),
                    Name = game.Name,
                    Status = game.GameStatus.ToString(),
                    Password = game.Password?.Length != 0,
                    CurrentRound = game.CurrentRoundId,
                    Players = players.Select(p => p.PlayerName.ToString()).ToArray(),
                    Enemies = players.Where(p => p.IsEnemy == true).Select(p => p.PlayerName.ToString()).ToArray()
                }
            };
        }

        private StatusCodeAllGames GamesList(List<Game> games)
        {
            return new StatusCodeAllGames
            {
                Status = 200,
                Msg = "Games Found",
                Data = games.Select(g => new Data
                {
                    Id = g.Id.ToString(),
                    Name = g.Name,
                    Status = g.GameStatus.ToString(),
                    Password = g.Password?.Length != 0,
                    CurrentRound = g.CurrentRoundId ?? Guid.Empty,
                    Players = _getPlayersByGameIdHandler.HandleAsync(new GetPlayersByGameIdQuery(g.Id)).Result.Select(p => p.PlayerName.ToString()).ToArray(),
                    Enemies = _getPlayersByGameIdHandler.HandleAsync(new GetPlayersByGameIdQuery(g.Id)).Result.Where(p => p.IsEnemy == true).Select(p => p.PlayerName.ToString()).ToArray()
                }).ToArray()
            };
        }


        //Obtener los juegos que hayan
        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            try
            {
                var query = new GetGamesPossibleQuery("", Status.Lobby, 0, 0);
                List<Game> games = (List<Game>)await _getGamesHandler.HandleAsync(query);
                
                var result = GamesList(games);

                return Ok(result);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message, status = ex.Status });
            }
        }

    }
}