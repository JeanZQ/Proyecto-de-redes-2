using Contaminados.Application.Handlers;
using Contaminados.Application.Queries;
using Contaminados.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Contaminados.Models.Common;
using Models.gameModels;
using Models.playersModels;
using Application.Commands.Common;
using Application.Handlers.Common;
using Models.roundGroupModels;
using API.Controllers.Common;

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
        private readonly GetGamesHandler _getGamesHandler;
        private readonly UpdateGameHandler _updateGameHandler;
        private readonly CreateRoundHandler _createRoundHandler;
        private readonly UpdatePlayerHandler _updatePlayerHandler;
        private readonly UpdateRoundVoteHandler _updateRoundVoteHandler;
        private readonly UpdateRoundHandler _updateRoundHandler;
        private readonly DeleteAllRoundGroupsByRoundIdHandler deleteAllRoundGroupsByRoundIdHandler;
        private readonly DeleteAllRoundVotesByRoundIdHandler deleteAllRoundVotesByRoundIdHandler;
        public GameController(
            GetGameByIdByPasswordByPlayerHandler getGameByIdByPasswordByOwnerHandler,
            CreateGameHandler createGameHandler,
            GetGamesHandler getGamesHandler,
            GetAllPlayersByGameIdHandler getAllPlayersByGameIdHandler,
            GetAllRoundByGameIdHandler getAllRoundByGameIdHandler,
            GetAllRoundGroupByRoundIdHandler getAllRoundGroupByRoundIdHandler,
            GetAllRoundVoteByRoundIdHandler getAllRoundVoteByRoundIdHandler,
            GetRoundByIdHandler getRoundByIdHandler,
            CreateRoundGroupHandler createRoundGroupHandler,
            CreateRoundVoteHandler createRoundVoteHandler,
            CreatePlayerHandler createPlayerHandler,
            UpdateGameHandler updateGameHandler,
            UpdatePlayerHandler updatePlayerHandler,
            UpdateRoundVoteHandler updateRoundVoteHandler,
            CreateRoundHandler createRoundHandler,
            UpdateRoundHandler updateRoundHandler,
            DeleteAllRoundGroupsByRoundIdHandler deleteAllRoundGroupsByRoundIdCommand,
            DeleteAllRoundVotesByRoundIdHandler deleteAllRoundVotesByRoundIdCommand)
        {
            _getGameByIdByPasswordByOwnerHandler = getGameByIdByPasswordByOwnerHandler;
            _createGameHandler = createGameHandler;
            _getGamesHandler = getGamesHandler;
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
            _updateGameHandler = updateGameHandler;
            _createRoundHandler = createRoundHandler;
            _updatePlayerHandler = updatePlayerHandler;
            _updateRoundVoteHandler = updateRoundVoteHandler;
            _updateRoundHandler = updateRoundHandler;
            deleteAllRoundGroupsByRoundIdHandler = deleteAllRoundGroupsByRoundIdCommand;
            deleteAllRoundVotesByRoundIdHandler = deleteAllRoundVotesByRoundIdCommand;
        }

        /// <summary>
        /// Game Search
        /// </summary>
        /// <returns></returns>
        //Obtener los juegos que hayan
        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] string? name, Status? status, int? page, int? limit)
        {
            try
            {
                GetGamesPossibleQuery query = new GetGamesPossibleQuery(name, status, page, limit);
                List<Game> games = (List<Game>)await _getGamesHandler.HandleAsync(query);

                var result = GamesList(games);

                return Ok(result);
            }
            catch (CustomException ex)
            {
                return BadRequest(new { message = ex.Message, status = ex.Status });
            }
        }

        /// <summary>
        /// Get Game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(Guid gameId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player)
        {
            try
            {
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));
                var players = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));

                return Ok(new StatusCodesOkCommon(game, players, "Game Found").GetStatusCodesOk());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new { msg = ex.Message, status = ex.Status });
            }
        }

        /// <summary>
        /// Game Create
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
        {
            try
            {
                var game = await _createGameHandler.HandleAsync(command);
                await _createPlayerHandler.HandleAsync(new CreatePlayerCommand(command.Owner, game.Id));
                var players = new List<Players> { new Players { PlayerName = command.Owner, GameId = game.Id } };
                return Ok(new StatusCodesOkCommon(game, players, "Game Created").GetStatusCodesOk());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new { msg = ex.Message, status = ex.Status });
            }
        }

        /// <summary>
        /// Get Rounds
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpGet("{gameId}/rounds")]
        public async Task<IActionResult> GetRounds(Guid gameId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player)
        {
            try
            {
                // Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                // Informaci�n de todas las rondas con el mismo gameId
                var rounds = await _getAllRoundByGameIdHandler.HandleAsync(new GetAllRoundByGameIdQuery(gameId));
                var dataRoundsList = new List<DataRounds>();

                foreach (var r in rounds)
                {
                    var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(r.Id));
                    var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(r.Id));
                    var playerName = new List<string>();

                    foreach (var g in group)
                    {
                        playerName.Add(g.Player);
                    }

                    var dataRound = new DataRounds
                    {
                        Id = r.Id,
                        Leader = r.Leader,
                        Status = r.Status.ToString(),
                        Result = r.Result.ToString(),
                        Phase = r.Phase.ToString(),
                        Group = playerName.ToArray(),
                        Votes = votes.Select(v => v.GroupVote == Vote.Yes ? true : false).ToArray()
                    };

                    dataRoundsList.Add(dataRound);
                }

                var result = new StatusCodesOkRounds
                {
                    Status = 200,
                    Msg = "Rounds Found",
                    Data = dataRoundsList.ToArray()
                };

                return Ok(result);
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new
                {
                    msg = ex.Message,
                    status = ex.Status
                });
            }
        }

        /// <summary>
        /// Start Game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        //StartGame
        [HttpHead("{gameId}/start")]
        public async Task<IActionResult> StartGame(Guid gameId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player)
        {

            try
            {
                //Validar credenciales
                var game = await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Crear la ronda
                var leader = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));
                var random = new Random();
                int index = random.Next(leader.Count());
                var leaderName = leader.ElementAt(index).PlayerName;

                var round = await _createRoundHandler.HandleAsync(new CreateRoundCommand(leaderName, RoundsStatus.WaitingOnLeader, RoundsResult.none, RoundsPhase.Vote1, gameId));//revizar

                //Iniciar el juego
                await _updateGameHandler.HandleAsync(new UpdateGameCommand(game.Id, Status.Rounds, round.Id, player, password ?? string.Empty));

                //Asignar enemiges
                List<Players> playerList = (List<Players>)await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));
                Enemies enemies = Enemies.Instance;
                var amountEnemies = enemies.GetEnemies(playerList.Count());

                // asignar enemigos aleatorios
                for (int i = 0; i < amountEnemies; i++)//pasar a lambda
                {
                    var randomEnemie = random.Next(playerList.Count());
                    var enemy = playerList.ElementAt(randomEnemie);
                    playerList.Remove(enemy);
                    await _updatePlayerHandler.HandleAsync(new UpdatePlayerCommand(enemy.Id, enemy.GameId, enemy.PlayerName, true));
                }

                return Ok(new { Code = 200, Description = "Game started" });
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

        /// <summary>
        /// Show Round
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="roundId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <returns></returns>
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

                return Ok(new StatusCodesOkRoundsCommon(round, group, votes, "Round Found").GetStatusCodesOkRounds());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new
                {
                    msg = ex.Message,
                    status = ex.Status
                });
            }
        }

        /// <summary>
        /// Propose a Group
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="roundId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPatch("{gameId}/rounds/{roundId}")]
        public async Task<IActionResult> ProposeGroup(Guid gameId, Guid roundId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player, [FromBody] GroupCommon group)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Variables para la respuesta
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));

                //Guardar el grupo
                await _createRoundGroupHandler.HandleAsync(new CreateRoundGroupCommand(round.Id, group.Group, player, gameId));

                //Cambio del status de la ronda a Voting
                await _updateRoundHandler.HandleAsync(new UpdateRoundCommand(round.Id, round.Leader, RoundsStatus.Voting, round.Result, round.Phase, round.GameId));

                var newGroup = group.Group.Select(g => new RoundGroup { Player = g, RoundId = roundId }).ToList();
                return Ok(new StatusCodesOkRoundsCommon(round, newGroup, votes, "Group Created").GetStatusCodesOkRounds());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.Status, new
                {
                    msg = ex.Message,
                    status = ex.Status
                });
            }
        }

        /// <summary>
        /// Vote for group
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="roundId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <param name="vote"></param>
        /// <returns></returns>
        [HttpPost("{gameId}/rounds/{roundId}")]
        public async Task<IActionResult> VoteGroup(Guid gameId, Guid roundId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player, [FromBody] GroupVoteCommon vote)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Guardar el voto
                await _createRoundVoteHandler.HandleAsync(new CreateRoundVoteCommand(roundId, player, Vote.NA, vote.Vote == true ? Vote.Yes : Vote.No));

                //Variables para la respuesta
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
                var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(roundId));
                var game = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));

                // Eliminar todos los grupos y votos de la ronda si la mayoria de los votos son NO
                if (votes.Count() == game.Count() && votes.Count(x => x.GroupVote == Vote.No) > game.Count() / 2)
                {
                    await deleteAllRoundGroupsByRoundIdHandler.HandleAsync(new DeleteAllRoundGroupsByRoundIdCommand(roundId));
                    await deleteAllRoundVotesByRoundIdHandler.HandleAsync(new DeleteAllRoundVotesByRoundIdCommand(roundId));
                    await _updateRoundHandler.HandleAsync(new UpdateRoundCommand(round.Id, round.Leader, RoundsStatus.WaitingOnLeader, round.Result, round.Phase, round.GameId));
                }

                //Cambiamos el status de la ronda a Waiting on group si la mayoria de los votos son SI
                else if (votes.Count() == game.Count() && votes.Count(x => x.GroupVote == Vote.Yes) > game.Count() / 2)
                {
                    await _updateRoundHandler.HandleAsync(new UpdateRoundCommand(round.Id, round.Leader, RoundsStatus.WaitingOnGroup, round.Result, round.Phase, round.GameId));
                }

                return Ok(new StatusCodesOkRoundsCommon(round, group, votes, "Vote registered").GetStatusCodesOkRounds());
            }
            catch (CustomException e)
            {
                return StatusCode(e.Status, new
                {
                    message = e.Message,
                    status = e.Status
                });
            }
        }

        /// <summary>
        /// Join Game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <param name="players"></param>
        /// <returns></returns>
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
                return Ok(new StatusCodesOkCommon(game, playerlist, "Joined Game").GetStatusCodesOk());
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

        /// <summary>
        /// Action Vote
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="roundId"></param>
        /// <param name="password"></param>
        /// <param name="player"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPut("{gameId}/rounds/{roundId}")]
        public async Task<IActionResult> ActionVote(Guid gameId, Guid roundId, [FromHeader(Name = "password")] string? password, [FromHeader(Name = "player")] string player, [FromBody] ActionVoteCommon action)
        {
            try
            {
                //Validar credenciales
                await _getGameByIdByPasswordByOwnerHandler.HandleAsync(new GetGameByIdByPasswordByPlayerQuery(gameId, password ?? string.Empty, player));

                //Variables para la respuesta
                var round = await _getRoundByIdHandler.HandleAsync(new GetRoundByIdQuery(roundId));
                var votes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
                var group = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(roundId));
                //Guardar el voto
                await _updateRoundVoteHandler.HandleAsync(new UpdateRoundVoteCommand(player, roundId, action.Action ? Vote.Yes : Vote.No, null));

                //----------------------------------------------------------------------------------------------
                //Acciones cuando todos los jugadores han votado------------------------------------------------
                //----------------------------------------------------------------------------------------------

                //Todos los votos de la ronda
                var roundVotes = await _getAllRoundVoteByRoundIdHandler.HandleAsync(new GetAllRoundVoteByRoundIdQuery(roundId));
                //Todos los jugadores que pueden votar
                var playersGroup = await _getAllRoundGroupByRoundIdHandler.HandleAsync(new GetAllRoundGroupByRoundIdQuery(roundId));

                //Si todos votaron
                if (roundVotes.Count(x => x.Vote != Vote.NA) == playersGroup.Count())
                {
                    //Si todos aportaron en la ronda
                    if (!roundVotes.Any(x => x.Vote == Vote.No))
                    {
                        //Ganan los Citizens
                        await _updateRoundHandler.HandleAsync(new UpdateRoundCommand(round.Id, round.Leader, RoundsStatus.Ended, RoundsResult.Citizens, round.Phase, round.GameId));
                    }
                    else
                    {
                        //Ganan los Enemies
                        await _updateRoundHandler.HandleAsync(new UpdateRoundCommand(round.Id, round.Leader, RoundsStatus.Ended, RoundsResult.Enemies, round.Phase, round.GameId));
                    }

                    //Todas las rondas
                    var rounds = await _getAllRoundByGameIdHandler.HandleAsync(new GetAllRoundByGameIdQuery(gameId));
                    //El juego no ha terminado
                    if (round.Phase != RoundsPhase.Vote5 && (rounds.Count(x => x.Result == RoundsResult.Citizens) <= 3 || rounds.Count(x => x.Result == RoundsResult.Enemies) <= 3))
                    {
                        //Pasamos a la siguiente ronda y actualizamos Game
                        var leader = await _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(gameId));
                        var random = new Random();
                        int index = random.Next(leader.Count());
                        var leaderName = leader.ElementAt(index).PlayerName;

                        var nextRound = await _createRoundHandler.HandleAsync(new CreateRoundCommand(leaderName, RoundsStatus.WaitingOnLeader, RoundsResult.none, round.Phase + 1, gameId));
                        await _updateGameHandler.HandleAsync(new UpdateGameCommand(gameId, Status.Rounds, nextRound.Id, player, password ?? string.Empty));
                    }
                    //Ya termino el juego
                    else
                    {
                        await _updateGameHandler.HandleAsync(new UpdateGameCommand(gameId, Status.Ended, round.Id, player, password ?? string.Empty));
                    }
                }
                return Ok(new StatusCodesOkRoundsCommon(round, group, votes, "Action registered").GetStatusCodesOkRounds());
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
                    CurrentRound = g.CurrentRoundId,
                    Players = _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(g.Id)).Result.Select(p => p.PlayerName.ToString()).ToArray(),
                    Enemies = _getAllPlayersByGameIdHandler.HandleAsync(new GetAllPlayersByGameIdQuery(g.Id)).Result.Where(p => p.IsEnemy == true).Select(p => p.PlayerName.ToString()).ToArray()
                }).ToArray()
            };
        }
    }
}