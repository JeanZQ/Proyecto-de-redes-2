using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.gameModels;
using Models.playersModels;

namespace Contaminados.Application.Handlers
{
    public class CreatePlayerHandler
    {
        private readonly IPlayerRepository<Players> _playerRepository;
        private readonly IGameRepository<Game> _gameRepository;
        public CreatePlayerHandler(IPlayerRepository<Players> playerRepository, IGameRepository<Game> gameRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }
        public async Task<Guid> HandleAsync(CreatePlayerCommand command)
        {
            var player = new Players{
                PlayerName = command.Name,
                GameId = command.GameId
            };

            // valida si el jugador ya existe
            var allPlayers = await _playerRepository.GetAllPlayersByGameIdAsync(command.GameId);

            if (allPlayers.Select(p => p.PlayerName).Contains(command.Name))
            {
                throw new ConflictException();
            }

            // valida si el juego es distinto de lobby o hay mas de 10 players, no deja unirse
            var game = await _gameRepository.GetGameByIdAsync(command.GameId);
            if (game.GameStatus != Status.lobby || allPlayers.Count() > 9)
            {
                throw new PreconditionRequiredException();
            }

            await _playerRepository.CreatePlayerAsync(player);
            return player.Id;
        }

    };
}