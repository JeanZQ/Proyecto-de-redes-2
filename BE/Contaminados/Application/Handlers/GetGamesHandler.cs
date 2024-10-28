using Contaminados.Application.Queries;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.gameModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contaminados.Application.Handlers
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

    
            if (query.Name != null && query.Name.Length is < 3 or > 20)
            {
                throw new ClientException();
            } else if (query.Limit != null && query.Limit <= 0 || query.Limit > 50)
            {
                throw new ClientException();
            } else if (query.Page != null && query.Page < 0) {
                throw new ClientException();
            }


            int page = query.Page ?? 0;
            int limit = query.Limit ?? 250; 

            var games = await _gameRepository.GetGamesByDate();



             IEnumerable<Game> filteredGames = games
            .Where(game => (query.Name == null || game.Name.Contains(query.Name)) &&
                   (query.Status == null || game.GameStatus == query.Status))
            .ToList();


            int skip = page * limit;
            filteredGames = filteredGames.Skip(skip).Take(limit).ToList();


            return filteredGames;


        }
    }
}
