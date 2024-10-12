using Contaminados.Aplication.Handlers;
using Contaminados.Aplication.Queries;
using Contaminados.Aplication.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Contaminados.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GetGameByIdHandler _getGameByIdHandler;
        private readonly CreateGameHandler _createGameHandler;
        public GameController(GetGameByIdHandler getGameByIdHandler, CreateGameHandler createGameHandler)
        {
            _getGameByIdHandler = getGameByIdHandler;
            _createGameHandler = createGameHandler;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var query = new GetGameByIdQuery(id);
            var game = await _getGameByIdHandler.HandleAsync(query);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command){
            var id = await _createGameHandler.HandleAsync(command);
            return CreatedAtAction(nameof(GetGameById), new { id }, command);
        }
    }
}