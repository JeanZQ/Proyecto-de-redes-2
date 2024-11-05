using Contaminados.Application.Commands;
using Contaminados.Repositories.IRepository;
using Contaminados.Models.Common;
using Models.gameModels;
using Models.playersModels;

namespace Contaminados.Application.Handlers
{
    public class UpdateGameHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        private readonly IPlayerRepository<Players> _playerRepository;
        public UpdateGameHandler(IGameRepository<Game> gameRepository, IPlayerRepository<Players> playerRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task HandleAsync(UpdateGameCommand command)
        {
            // busca en la base de datos el juego
            var game = await _gameRepository.GetGameByIdAsync(command.Id);


            // Unauthorized
            if (!(game.Password != null && game.Password.Length == 0 && command.Password == null))
            {
                if (game.Password != command.Password)
                {
                    throw new UnauthorizedStartExeption();
                }
            }

            // Forbidden (El ultimo que del actionVote le tira error)
            // if (game.Owner != command.Player)
            // {
            //    throw new ForbiddenException();
            // }

            // Si el juego ya empezo no se puede volver a empezar (No actualiza currentRoundId de Game)
            //if (game.GameStatus == Status.Rounds || game.GameStatus == Status.Ended)
            //{
            //    throw new GameAlreadyStartedStartExeption();
            //}

            // Si no hay suficientes jugadores no se puede empezar
            var players = await _playerRepository.GetAllPlayersByGameIdAsync(game.Id);
            if (players.Count() < 5)
            {
                throw new NeedPlayerStartExeption();
            }

            game.GameStatus = command.GameStatus;
            game.CurrentRoundId = command.CurrentRoundId; // cambia el roundId
            await _gameRepository.UpdateGameAsync(game);
        }
    }
}