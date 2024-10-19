using Contaminados.Application.Queries;
using Contaminados.Repositories.IRepository;
using Models.playersModels;

namespace Contaminados.Application.Handlers
{
    public class GetAllPlayersByGameIdHandler
    {
        private readonly IPlayerRepository<Players> _playerRepository;
        public GetAllPlayersByGameIdHandler(IPlayerRepository<Players> playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }
        public async Task<IEnumerable<Players>> HandleAsync(GetAllPlayersByGameIdQuery request)
        {
            //Falta validaciones-------------------------------------

            var players = await _playerRepository.GetAllPlayersByGameIdAsync(request.GameId);
            return players;
        }
    }
}