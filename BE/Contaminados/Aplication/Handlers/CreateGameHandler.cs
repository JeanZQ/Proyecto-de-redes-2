using Contaminados.Aplication.Commands;
using Contaminados.Repositories.IRepository;
using Contaminados.Repositories.Repository;
using Models.gameModels;
using Models.playersModels;

namespace Contaminados.Aplication.Handlers
{
    public class CreateGameHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        private readonly IPlayerRepository<Players> _playerRepository;
        public CreateGameHandler(IGameRepository<Game> gameRepository, IPlayerRepository<Players> playerRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<Guid> HandleAsync(CreateGameCommand command)
        {
            var game = new Game
            {
                Name = command.Name,
                Password = command.Password,
                GameStatus = Models.Common.Status.Lobby
            };

            var players = new Players{
                PlayerName = command.Owner,
                GameId = game.Id
            };

            await _gameRepository.CreateGameAsync(game);
            await _playerRepository.CreatePlayerAsync(players);
            
            return game.Id;
        }
    }
}