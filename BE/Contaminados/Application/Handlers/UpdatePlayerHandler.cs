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
            try
            {
                var player = await _playerRepository.GetPlayerByIdAsync(command.Id);
                player.IsEnemy = command.IsEnemy;
                player.PlayerName = command.PlayerName;
                
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