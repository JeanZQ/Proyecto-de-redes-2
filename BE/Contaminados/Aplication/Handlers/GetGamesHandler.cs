using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.gameModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetGamesHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        public GetGamesHandler(IGameRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
        }
        public async Task<IEnumerable<Game>> HandleAsync(GetGamesPossibleQuery query)
        {
            //Falta validaciones-------------------------------------

             System.Console.WriteLine(query);

            var games = await _gameRepository.GetAllGamesAsync();
            return games;
        }   
    }
}
