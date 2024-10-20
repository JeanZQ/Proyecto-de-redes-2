using Contaminados.Application.Commands;
using Contaminados.Repositories.IRepository;
using Contaminados.Models.Common;
using Models.gameModels;

namespace Contaminados.Application.Handlers
{
    public class UpdateGameHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        public UpdateGameHandler(IGameRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }

        public async Task<Game> HandleAsync(UpdateGameCommand command)
        {

            try
            {

                if (command.id == Guid.Empty)
                {
                    throw new ClientException();
                }


                // busca en la base de datos el juego
                var game = await _gameRepository.GetGameByIdAsync(command.id);

                game.gameStatus = command.gameStatus;
                game.currentRoundId = command.currentRoundId; // cambia el roundId
                await _gameRepository.UpdateGameAsync(game);

            }
            catch (Exception)
            {
                throw new ConflictException();
            }
          


        }


    }
}