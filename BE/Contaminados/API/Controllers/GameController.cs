using Contaminados.Aplication.Handlers;
using Contaminados.Aplication.Queries;
using Contaminados.Aplication.Commands;
using Microsoft.AspNetCore.Mvc;
using Contaminados.Models.Common;

namespace Contaminados.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GetGameByIdByPasswordByPlayerHandler _getGameByIdByPasswordByOwnerHandler;
        private readonly CreateGameHandler _createGameHandler;
        private readonly GetPlayersByGameIdHandler _getPlayersByGameIdHandler;
        public GameController(
            GetGameByIdByPasswordByPlayerHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetPlayersByGameIdHandler getPlayersByGameIdHandler)
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getPlayersByGameIdHandler = getPlayersByGameIdHandler;
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(Guid gameId, string? password, string player)
        {
            try
            {
                var query = new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player);
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(query);
                var players = await _getPlayersByGameIdHandler.HandleAsync(new GetPlayersByGameIdQuery(gameId));

                var result = new StatusCodesOk
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
                        Players = players.Where(p => p.GameId == game.Id).Select(p => p.PlayerName.ToString()).ToArray(),
                        Enemies = players.Where(p => p.IsEnemy == true).Select(p => p.PlayerName.ToString()).ToArray()
                    }
                };
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

                var result = new StatusCodesOk
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
                        Players = players.Where(p => p.GameId == game.Id).Select(p => p.PlayerName.ToString()).ToArray(),
                        Enemies = players.Where(p => p.IsEnemy == true).Select(p => p.PlayerName.ToString()).ToArray()
                    }
                };
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message, status = ex.Status });
            }
        }
    }
}