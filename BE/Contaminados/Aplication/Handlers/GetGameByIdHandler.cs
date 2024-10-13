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
            ArgumentNullException.ThrowIfNull(request);
            if(request.Id == Guid.Empty){
                throw new ArgumentNullException("Id is empty");
            }

            var game = await _gameRepository.GetGameByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Game with id {request.Id} not found.");

            return game;
        }
    }
}