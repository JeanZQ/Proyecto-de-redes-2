using Contaminados.Application.Handlers;
using Contaminados.Application.Queries;
using Contaminados.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Contaminados.Models.Common;
using Models.gameModels;
using Models.playersModels;
using Application.Commands.Common;

namespace Contaminados.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly GetGameByIdByPasswordByPlayerHandler _getGameByIdByPasswordByOwnerHandler;
        private readonly CreateGameHandler _createGameHandler;
        private readonly GetAllPlayersByGameIdHandler _getAllPlayersByGameIdHandler;
        private readonly GetAllRoundByGameIdHandler _getAllRoundByGameIdHandler;
        private readonly GetAllRoundGroupByRoundIdHandler _getAllRoundGroupByRoundIdHandler;
        private readonly GetAllRoundVoteByRoundIdHandler _getAllRoundVoteByRoundIdHandler;
        private readonly GetRoundByIdHandler _getRoundByIdHandler;
        private readonly CreateRoundGroupHandler _createRoundGroupHandler;
        private readonly CreateRoundVoteHandler _createRoundVoteHandler;
        private readonly CreatePlayerHandler _createPlayerHandler;
        public GameController(
            GetGameByIdByPasswordByPlayerHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetAllPlayersByGameIdHandler getAllPlayersByGameIdHandler,
            GetAllRoundByGameIdHandler getAllRoundByGameIdHandler,
            GetAllRoundGroupByRoundIdHandler getAllRoundGroupByRoundIdHandler,
            GetAllRoundVoteByRoundIdHandler getAllRoundVoteByRoundIdHandler,
            GetRoundByIdHandler getRoundByIdHandler,
            CreateRoundGroupHandler createRoundGroupHandler,
            CreateRoundVoteHandler createRoundVoteHandler,
            CreatePlayerHandler createPlayerHandler)
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getAllPlayersByGameIdHandler = getAllPlayersByGameIdHandler;
            _getAllRoundByGameIdHandler = getAllRoundByGameIdHandler;
            _getAllRoundGroupByRoundIdHandler = getAllRoundGroupByRoundIdHandler;
            _getAllRoundVoteByRoundIdHandler = getAllRoundVoteByRoundIdHandler;
            _getRoundByIdHandler = getRoundByIdHandler;
            _createRoundGroupHandler = createRoundGroupHandler;
            _createRoundVoteHandler = createRoundVoteHandler;
            _createPlayerHandler = createPlayerHandler;
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(Guid gameId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player)
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
                await _createPlayerHandler.HandleAsync(new CreatePlayerCommand(command.Owner, game.Id));
                var players = new List<Players> { new Players { PlayerName = command.Owner, GameId = game.Id } };
                var result = CreateResult(game, players);
                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new { message = ex.Message, status = ex.Status });
            }
        }

        [HttpGet("{gameId}/rounds")]
        public async Task<IActionResult> GetRounds(Guid gameId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player)
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
                        var playerName = await Task.WhenAll(group.Select(g =>
                        {
                            return Task.FromResult(g.Player);
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
        public async Task<IActionResult> ShowRound(Guid gameId, Guid roundId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Variables para la respuesta
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
                var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(roundId));

                return Ok(new StatusCodesOkRounds
                {
                    Status = 200,
                    Msg = "Joined Game",
                    Data = new DataRounds
                    {
                        Id = round.Id,
                        Leader = round.Leader,
                        Status = round.Status.ToString(),
                        Result = round.Result.ToString(),
                        Phase = round.Phase.ToString(),
                        Group = group.Select(g => g.Player).ToArray(),
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
        [HttpPatch("{gameId}/rounds/{roundId}")]
        public async Task<IActionResult> ProposeGroup(Guid gameId, Guid roundId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player, [FromBody] GroupCommon group)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Guardar el grupo
                await Task.WhenAll(group.Group.Select(async p =>
                {
                    await _createRoundGroupHandler.HandleAsync(new CreateRoundGroupCommand(roundId, player));
                }));

                //Variables para la respuesta
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));

                return Ok(new StatusCodesOkRounds
                {
                    Status = 200,
                    Msg = "Group Created",
                    Data = new DataRounds
                    {
                        Id = round.Id,
                        Leader = round.Leader,
                        Status = round.Status.ToString(),
                        Result = round.Result.ToString(),
                        Phase = round.Phase.ToString(),
                        Group = group.Group.ToArray(),
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

        [HttpPost("{gameId}/rounds/{roundId}")]
        public async Task<IActionResult> VoteGroup(Guid gameId, Guid roundId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player, [FromBody] GroupVoteCommon vote)
        {
            //Validar credenciales
            await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

            //Guardar el voto
            await _createRoundVoteHandler.HandleAsync(new CreateRoundVoteCommand(roundId, vote.Vote));

            //Variables para la respuesta
            var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));
            var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
            var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(roundId));

            return Ok(
                new StatusCodesOk
                {
                    Status = 200,
                    Msg = "Vote Created",
                    Data = new DataRounds
                    {
                        Id = round.Id,
                        Leader = round.Leader,
                        Status = round.Status.ToString(),
                        Result = round.Result.ToString(),
                        Phase = round.Phase.ToString(),
                        Group = group.Select(g => g.Player).ToArray(),
                        Votes = votes.Select(v => v.Vote).ToArray()
                    }
                }
            );
        }

        [HttpPut("{gameId}")]
        public async Task<IActionResult> JoinGame(Guid gameId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player, [FromBody] PlayersCommon players)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Guardar el jugador
                await _createPlayerHandler.HandleAsync(new CreatePlayerCommand(players.Player, gameId));

                //Variables para la respuesta
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));
                var playerlist = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));
                return Ok(CreateResult(game, playerlist));
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