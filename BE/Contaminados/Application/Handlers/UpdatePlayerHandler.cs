using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.playersModels;


namespace Contaminados.Application.Handlers
{

    public class UpdatePlayerHandler
    {
        private readonly IPlayerRepository<Players> _playerRepository;
        public UpdatePlayerHandler(IPlayerRepository<Players> playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task HandleAsync(UpdatePlayerCommand command)
        {

            if (command.Id == Guid.Empty)
            {
                throw new ClientException(); //Revizar si es la excepcion correcta
            }

            var player = new Players
            {
                Id = command.Id,
                GameId = command.GameId,
                PlayerName = command.PlayerName,
                IsEnemy = command.IsEnemy,
            };

            try
            {
                // actualiza en la base de datos el jugador
                await _playerRepository.UpdatePlayerAsync(player);
            }
            catch (CustomException)
            {
                throw new ForbiddenException(); //revisar
            }
        }


    }










}