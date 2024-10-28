using Contaminados.Application.Queries;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.gameModels;
using Models.playersModels;

namespace Contaminados.Application.Handlers
{
    public class GetGameByIdByPasswordByPlayerHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        private readonly IPlayerRepository<Players> _playerRepository;

        public GetGameByIdByPasswordByPlayerHandler(IGameRepository<Game> gameRepository, IPlayerRepository<Players> playerRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
        }
        public async Task<Game> HandleAsync(GetGameByIdByPasswordByPlayerQuery request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new NotFoundException();
            }

            if (request.Player.Length is < 3 or > 20)
            {
                throw new UnauthorizedException();
            }

            var game = await _gameRepository.GetGameByIdAsync(request.Id) ?? throw new NotFoundException();
            IEnumerable<Players> players = await _playerRepository.GetAllPlayersByGameIdAsync(request.Id);

            if (!(game.Password != null && game.Password.Length == 0 && request.Password == null))
            {
                if (game.Password != request.Password)
                {
                    throw new UnauthorizedException();
                }
            }

            if (game.Owner == request.Player || players.Any(p => p.PlayerName == request.Player))
            {
                return game; ;
            }

            throw new ForbiddenException();
        }
    }
}