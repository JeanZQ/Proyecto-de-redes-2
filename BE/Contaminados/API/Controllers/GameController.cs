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
        private readonly GetAllPlayersByGameIdHandler _getAllPlayersByGameIdHandler;
        private readonly GetPlayerByIdHandler _getPlayerByIdHandler;
        private readonly GetAllRoundByGameIdHandler _getAllRoundByGameIdHandler;
        private readonly GetAllRoundGroupByRoundIdHandler _getAllRoundGroupByRoundIdHandler;
        private readonly GetAllRoundVoteByRoundIdHandler _getAllRoundVoteByRoundIdHandler;
        private readonly GetRoundByIdHandler _getRoundByIdHandler;
        public GameController(
            GetGameByIdByPasswordByPlayerHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetAllPlayersByGameIdHandler getAllPlayersByGameIdHandler,
            GetPlayerByIdHandler getPlayerByIdHandler,
            GetAllRoundByGameIdHandler getAllRoundByGameIdHandler,
            GetAllRoundGroupByRoundIdHandler getAllRoundGroupByRoundIdHandler,
            GetAllRoundVoteByRoundIdHandler getAllRoundVoteByRoundIdHandler,
            GetRoundByIdHandler getRoundByIdHandler)
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getAllPlayersByGameIdHandler = getAllPlayersByGameIdHandler;
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
                var players = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));

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
                var players = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(game.Id));

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
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Informacion de todas las rondas con el mismo gameId
                var query = new GetAllRoundByGameIdQuery(gameId);
                var rounds = await _getAllRoundByGameIdHandler.HandleAsync(new GetAllRoundByGameIdQuery(gameId));

                var result = new StatusCodesOkRounds
                {
                    Status = 200,
                    Msg = "Rounds Found",
                    Data = await Task.WhenAll(rounds.Select(async r =>
                    {
                        var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(r.Id));
                        var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(r.Id));
                        var playerName = await Task.WhenAll(group.Select(async g =>
                        {
                            var player = await _getPlayerByIdHandler.HandleAsync(new GetPlayerByIdQuery(g.PlayerId));
                            return player.PlayerName;
                        }));
                        return new DataRounds
                        {
                            Id = r.Id,
                            Leader = r.Leader,
                            Status = r.Status.ToString(),
                            Result = r.Result.ToString(),
                            Phase = r.Phase.ToString(),
                            Group = playerName.ToArray(),
                            Votes = votes.Select(v => v.Vote).ToArray()
                        };
                    }))
                };
                return Ok(result);
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

        [HttpGet("{gameId}/rounds/{roundId}")]
        public async Task<IActionResult> ShowRound(Guid gameId, Guid roundId, string? password, string player)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Variables para la respuesta
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
                var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(roundId));
                var playerName = await Task.WhenAll(group.Select(async g =>
                {
                    var player = await _getPlayerByIdHandler.HandleAsync(new GetPlayerByIdQuery(g.PlayerId));
                    return player.PlayerName;
                }));

                return Ok(new StatusCodesOkRounds
                {
                    Status = 200,
                    Msg = "Round Found",
                    Data = new DataRounds
                    {
                        Id = round.Id,
                        Leader = round.Leader,
                        Status = round.Status.ToString(),
                        Result = round.Result.ToString(),
                        Phase = round.Phase.ToString(),
                        Group = playerName.ToArray(),
                        Votes = votes.Select(v => v.Vote).ToArray()
                    }
                });
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
        //-------------------------------------------------------------------------------------
        //No hacer el metodo ASYNC ni llamar a ningun Handler
        /*Futuras actualizaciones, por favor companeros no me reganen :c
        private StatusCodesOkRounds CreateResultRounds()
        {
            return new StatusCodesOkRounds
            {
                Status = 200,
                Msg = "Rounds Found",
                Data =
                [
                    new DataRounds
                    {
                        Id = round.Id,
                        Leader = round.Leader,
                        Status = round.Status.ToString(),
                        Result = round.Result.ToString(),
                        Phase = round.Phase.ToString(),
                        Group = group?.ToArray() ?? Array.Empty<string>(),
                        Votes = votes?.Select(v => v.Vote).ToArray() ?? Array.Empty<bool>()
                    }
                ]
            };
        }*/

    }
}