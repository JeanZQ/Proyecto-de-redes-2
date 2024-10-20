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

        public async Task HandleAsync(UpdateGameCommand command)
        {

            try
            {

                if (command.Id == Guid.Empty)
                {
                    throw new ClientException();
                }


                // busca en la base de datos el juego
                var game = await _gameRepository.GetGameByIdAsync(command.Id);

                game.GameStatus = command.GameStatus;
                game.CurrentRoundId = command.CurrentRoundId; // cambia el roundId
                await _gameRepository.UpdateGameAsync(game);
;

            }
            catch (Exception)
            {
                throw new ConflictException();
            }
          


        }


    }
}