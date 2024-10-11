using Contaminados.Aplication.Commands;
using Contaminados.Repositories.IRepository;
using Models.gameModels;

namespace Contaminados.Aplication.Handlers
{
    public class CreateGameHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        public CreateGameHandler(IGameRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public async Task<Guid> HandleAsync(CreateGameCommand command)
        {
            var game = new Game
            {
                Name = command.Name,
                Password = command.Password,
                GameStatus = Models.Common.Status.Lobby
            };
            await _gameRepository.CreateGameAsync(game);
            return game.Id;
        }
    }
}