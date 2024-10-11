using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.gameModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetGameByIdHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        public GetGameByIdHandler(IGameRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public async Task<Game> HandleAsync(GetGameByIdQuery request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("El ID del juego no puede estar vacío.", nameof(request.Id));
            }

            var game = await _gameRepository.GetGameByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Game with id {request.Id} not found.");
            
            return game;
        }
    }
}