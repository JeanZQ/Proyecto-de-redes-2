using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.playersModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetPlayersByGameIdHandler
    {
        private readonly IPlayerRepository<Players> _playerRepository;
        public GetPlayersByGameIdHandler(IPlayerRepository<Players> playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }
        public async Task<IEnumerable<Players>> HandleAsync(GetPlayersByGameIdQuery request)
        {
            //Falta validaciones-------------------------------------

            var players = await _playerRepository.GetPlayersByGameIdAsync(request.GameId);
            return players;
        }
    }
}