using Contaminados.Application.Commands;
using Contaminados.Repositories.IRepository;
using Models.playersModels;

namespace Contaminados.Application.Handlers
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
            //Falta validaciones-------------------------------------

            var player = new Players{
                PlayerName = command.Name,
                GameId = command.GameId
            };
            await _playerRepository.CreatePlayerAsync(player);
            return player.Id;
        }
    };
}