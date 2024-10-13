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
        private readonly GetGameByIdHandler _getGameByIdByPasswordByOwnerHandler;
        private readonly CreateGameHandler _createGameHandler;
        private readonly GetPlayerByIdHandler _getPlayerByIdHandler;
        public GameController(
            GetGameByIdHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetPlayerByIdHandler getPlayerByIdHandler)
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getPlayerByIdHandler = getPlayerByIdHandler;
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(Guid gameId, string password, string player)
        {
            try
            {
                var query = new GetGameByIdQuery(gameId);
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(query);

                //Validaciones
                if (game.Password != password)
                {
                    var error = ErrorMessages.GetErrorMessage(401);
                    return Unauthorized(new { msg = error.Message, status = error.Status });
                }
                if (game.Owner != player)
                {
                    var error = ErrorMessages.GetErrorMessage(403);
                    return StatusCode(403, new { msg = error.Message, status = error.Status });
                }

                //Respuesta
                var result = new StatusCodesOk
                {
                    Status = 200,
                    Msg = "Game Found",
                    Data = new Data
                    {
                        Name = game.Name,
                        Status = game.GameStatus.ToString(),
                        Password = game.Password != null ? true : false,
                        CurrentRound = game.CurrentRoundId,
                        Players = ["buscar jugadores"],
                        Enemies = ["buscar enemigos"]
                    }
                };
                return Ok(result);
            }
            
            //No se encontró el juego
            catch (KeyNotFoundException)
            {
                var error = ErrorMessages.GetErrorMessage(404);
                return NotFound(new { msg = error.Message, status = error.Status });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
        {
            var id = await _createGameHandler.HandleAsync(command);
            return Ok(id);
        }
    }
}