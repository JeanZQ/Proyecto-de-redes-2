using Contaminados.Application.Queries;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{
    public class GetRoundVoteByGameIdByPlayerNameHandler
    {
        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;
        public GetRoundVoteByGameIdByPlayerNameHandler(IRoundVoteRepository<RoundVote> roundVoteRepository)
        {
            _roundVoteRepository = _roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
        }
        public async Task<RoundVote> HandleAsync(GetRoundVoteByGameIdByPlayerNameQuery request)
        {
            var round = await _roundVoteRepository.GetRoundVoteByGameIdByPlayerNameAsync(request.GameId, request.PlayerName) ?? throw new NotFoundException();

            return round;
        }
    }




}