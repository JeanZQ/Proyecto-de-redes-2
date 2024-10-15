using Contaminados.Aplication.Handlers;
using Contaminados.Aplication.Queries;
using Contaminados.Aplication.Commands;
using Microsoft.AspNetCore.Mvc;
using Contaminados.Models.Common;
using Models.gameModels;
using Models.playersModels;
using Models.roundModels;
using Models.roundVoteModels;
using Models.roundGroupModels;

namespace Contaminados.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GetGameByIdByPasswordByPlayerHandler _getGameByIdByPasswordByOwnerHandler;
        private readonly CreateGameHandler _createGameHandler;
        private readonly GetPlayersByGameIdHandler _getPlayersByGameIdHandler;
        private readonly GetPlayerByIdHandler _getPlayerByIdHandler;
        private readonly GetAllRoundByGameIdHandler _getAllRoundByGameIdHandler;
        private readonly GetAllRoundGroupByRoundIdHandler _getAllRoundGroupByRoundIdHandler;
        private readonly GetAllRoundVoteByRoundIdHandler _getAllRoundVoteByRoundIdHandler;
        private readonly GetRoundByIdHandler _getRoundByIdHandler;
        public GameController(
            GetGameByIdByPasswordByPlayerHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetPlayersByGameIdHandler getPlayersByGameIdHandler,
            GetPlayerByIdHandler getPlayerByIdHandler,
            GetAllRoundByGameIdHandler getAllRoundByGameIdHandler,
            GetAllRoundGroupByRoundIdHandler getAllRoundGroupByRoundIdHandler,
            GetAllRoundVoteByRoundIdHandler getAllRoundVoteByRoundIdHandler,
            GetRoundByIdHandler getRoundByIdHandler)
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getPlayersByGameIdHandler = getPlayersByGameIdHandler;
            _getPlayerByIdHandler = getPlayerByIdHandler;
            _getAllRoundByGameIdHandler = getAllRoundByGameIdHandler;
            _getAllRoundGroupByRoundIdHandler = getAllRoundGroupByRoundIdHandler;
            _getAllRoundVoteByRoundIdHandler = getAllRoundVoteByRoundIdHandler;
            _getRoundByIdHandler = getRoundByIdHandler;
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(Guid gameId, string? password, string player)
        {
            try
            {
                var query = new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player);
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(query);
                var players = await _getPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));

                var result = CreateResult(game, players);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new { message = ex.Message, status = ex.Status });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
        {
            try
            {
                var game = await _createGameHandler.HandleAsync(command);
                var players = await _getPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(game.Id));

                var result = CreateResult(game, players);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new { message = ex.Message, status = ex.Status });
            }
        }

        [HttpGet("{gameId}/rounds")]
        public async Task<IActionResult> GetRounds(Guid gameId, string? password, string player)
        {
            try
            {
                var query = new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player);
                //Informacion de la ronda
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(gameId));
                //Informacion de los votos
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(round.Id));
                //Informacion de los grupos
                var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(round.Id));
                var playerName = await Task.WhenAll(group.Select(async g =>
                {
                    var player = await _getPlayerByIdHandler.HandleAsync(new GetPlayerByIdQuery(g.PlayerId));
                    return player.PlayerName;
                }));
                
                var result = CreateResultRounds(round, votes, playerName);
                return Ok();

            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new
                {
                    message = ex.Message,
                    status = ex.Status
                });
            }
        }

        //-------------------------------------------------------------------------------------
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

        private StatusCodesOkRounds CreateResultRounds(Round round, IEnumerable<RoundVote>? votes, IEnumerable<string>? group)
        {

            return new StatusCodesOkRounds
            {
                Status = 200,
                Msg = "Rounds Found",
                Data = new DataRounds
                {
                    Id = round.Id,
                    Leader = round.Leader,
                    Status = round.Status.ToString(),
                    Result = round.Result.ToString(),
                    Phase = round.Phase.ToString(),
                    Group = group?.Select(g => g.ToString()).ToArray() ?? Array.Empty<string>(),
                    Votes = votes?.Select(v => v.Vote).ToArray() ?? Array.Empty<bool>()
                }
            };
        }
    }
}