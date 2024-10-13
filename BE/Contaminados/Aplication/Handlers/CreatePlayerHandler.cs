using Contaminados.Aplication.Commands;
using Contaminados.Repositories.IRepository;
using Models.playersModels;

namespace Contaminados.Aplication.Handlers
{
    public class CreatePlayerHandler
    {
        private readonly IPlayerRepository<Players> _playerRepository;
        public CreatePlayerHandler(IPlayerRepository<Players> playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }
        public async Task<Guid> HandleAsync(CreatePlayerCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);
            if(command.Name == null){
                throw new ArgumentNullException("Name or GameId is null");
            } else if(command.Name == "" || command.GameId == Guid.Empty){
                throw new ArgumentNullException("Name or GameId is empty");
            } else if(command.Name.Length < 3){
                throw new ArgumentNullException("Name is too short");
            } else if(command.Name.Length > 20){
                throw new ArgumentNullException("Name is too long");
            }

            var player = new Players{
                PlayerName = command.Name,
                GameId = command.GameId
            };
            await _playerRepository.CreatePlayerAsync(player);
            return player.Id;
        }
    };
}