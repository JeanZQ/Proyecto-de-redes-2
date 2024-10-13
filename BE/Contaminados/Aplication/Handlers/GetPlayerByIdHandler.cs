using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.playersModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetPlayerByIdHandler
    {
        private readonly IPlayerRepository<Players> _playerRepository;
        public GetPlayerByIdHandler(IPlayerRepository<Players> playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }
        public async Task<Players> HandleAsync(GetPlayerByIdQuery request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new ArgumentException("El ID del jugador no puede estar vacío.", nameof(request.Id));
            }
            var player = await _playerRepository.GetPlayerByIdAsync(request.Id) ?? throw new KeyNotFoundException($"Player with id {request.Id} not found.");
            return player;
        }
    }
}