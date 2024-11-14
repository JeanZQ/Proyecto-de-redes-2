using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.gameModels;
using Models.playersModels;

namespace Contaminados.Application.Handlers
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

        public async Task<Game> HandleAsync(CreateGameCommand command)
        {
            if (command.Name == null || command.Owner == null
            || command.Name.Length is < 3 or > 20
            || command.Owner.Length is < 3 or > 20)
            {
                throw new ClientException();
            }

            if (command.Password != null && command.Password.Length != 0 && command.Password.Length is < 3 or > 20)
            {
                throw new ClientException();
            }

            var game = new Game
            {
                Name = command.Name,
                Password = command.Password ?? string.Empty,
                GameStatus = Status.lobby,
                Owner = command.Owner,
                CurrentRoundId = Guid.Empty
            };

            try
            {
                await _gameRepository.CreateGameAsync(game);
                return game;
            }
            catch (Exception)
            {
                throw new ConflictException();
            }
        }
    }
}