using Contaminados.Application.Queries;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.gameModels;

namespace Contaminados.Application.Handlers
{
    public class GetGameByIdByPasswordByPlayerHandler
    {
        private readonly IGameRepository<Game> _gameRepository;
        public GetGameByIdByPasswordByPlayerHandler(IGameRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public async Task<Game> HandleAsync(GetGameByIdByPasswordByPlayerQuery request)
        {
            if (request.Id == Guid.Empty
            || request.Player.Length is < 3 or > 20)
            {
                throw new UnauthorizedException();
            }

            var game = await _gameRepository.GetGameByIdAsync(request.Id) ?? throw new NotFoundException();

            if (!(game.Password != null && game.Password.Length == 0 && request.Password == null))
            {
                if (game.Password != request.Password)
                {
                    throw new UnauthorizedException();
                }
            }

            if (game.Owner != request.Player)
            {
                throw new ForbiddenException();
            }


            return game;
        }
    }
}