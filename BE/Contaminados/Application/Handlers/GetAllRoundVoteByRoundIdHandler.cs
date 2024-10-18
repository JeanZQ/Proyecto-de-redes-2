using Contaminados.Application.Queries;
using Contaminados.Repositories.IRepository;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{
    public class GetAllRoundVoteByRoundIdHandler
    {
        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;
        public GetAllRoundVoteByRoundIdHandler(IRoundVoteRepository<RoundVote> roundVoteRepository)
        {
            _roundVoteRepository = roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
        }
        public async Task<IEnumerable<RoundVote>> HandleAsync(GetAllRoundVoteByRoundIdQuery request)
        {
            //Falta validaciones-------------------------------------
            var roundVotes = await _roundVoteRepository.GetAllRoundVoteByRoundIdAsync(request.RoundId);
            return roundVotes;
        }
    }
}