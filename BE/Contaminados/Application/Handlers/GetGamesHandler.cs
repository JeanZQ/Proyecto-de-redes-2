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


            if (query.Name == null  
            || query.Name.Length is < 3 or > 20
            || query.Limit < 0 || query.Page < 0 || query.Page > 50)
            {
                throw new ClientException();
            }

            int page = 1;
            if (query.Page < 2 || query.Page == null)
            {
                page = 1;
            }
            else { 
                page = query.Page.Value;
            }
            int limit = query.Limit ?? 250; 

            var games = await _gameRepository.GetGamesByDate();
            List<Game> filteredGames = new List<Game>();

 
            foreach (var game in games)
            {
                if (query.Name != null && !game.Name.Contains(query.Name))
                {
                    continue;
                }

                if (query.Status != null && game.GameStatus != query.Status)
                {
                    continue;
                }

                filteredGames.Add(game);
            }

            int skip = (page - 1) * limit;
            filteredGames = filteredGames.Skip(skip).Take(limit).ToList();


            return filteredGames;


        }
    }
}
