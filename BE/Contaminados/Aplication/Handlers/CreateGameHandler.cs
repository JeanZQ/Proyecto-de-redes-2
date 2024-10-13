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
            ArgumentNullException.ThrowIfNull(command);
            if(command.Name == null || command.Password == null || command.Owner == null){
                throw new ArgumentNullException("Name, Password or Owner is null");
            } else if(command.Name == "" || command.Password == "" || command.Owner == ""){
                throw new ArgumentNullException("Name, Password or Owner is empty");
            } else if(command.Name.Length < 3 || command.Password.Length < 3 || command.Owner.Length < 3){
                throw new ArgumentNullException("Name, Password or Owner is too short");
            } else if(command.Name.Length > 20 || command.Password.Length > 20 || command.Owner.Length > 20){
                throw new ArgumentNullException("Name, Password or Owner is too long");
            }
            
            var game = new Game
            {
                Name = command.Name,
                Password = command.Password,
                GameStatus = Models.Common.Status.Lobby,
                Owner = command.Owner   
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