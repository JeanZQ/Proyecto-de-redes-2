using Contaminados.Application.Queries;
using Contaminados.Repositories.IRepository;
using Models.gameModels;

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
            //Falta validaciones-------------------------------------

            if (query.Name == null && query.Status == null && query.Limit == null && query.Page == null)
            {
                var games = await _gameRepository.GetAllGamesAsync();
                return games;
            }
            else {
                var games = await _gameRepository.GetAllGamesAsync();

                List<Game> filteredGames = new List<Game>();

                foreach (var game in games)
                {
                    // Funciona con el contains en not, no se porque
                    if (query.Name != null && !game.Name.Contains(query.Name))
                    {
                        continue;
                    }

                    if (query.Status != null && game.GameStatus != query.Status)
                    {
                        continue;
                    }

                    if (query.Page != null && query.Limit != null)
                    {
                        if (filteredGames.Count >= query.Limit)
                        {
                            break;
                        }
                    }

                    filteredGames.Add(game);
                }




                return filteredGames;
            }

            
        }
    }
}
