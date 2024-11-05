using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundModels;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{
    public class CreateRoundVoteHandler
    {
        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;
        private readonly IRoundRepository<Round> _roundRepository;

        public CreateRoundVoteHandler(IRoundVoteRepository<RoundVote> roundVoteRepository, IRoundRepository<Round> roundRepository)
        {
            _roundVoteRepository = roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
        }
        public async Task<RoundVote> HandleAsync(CreateRoundVoteCommand command)
        {
            // Verifica si el estado del round es Voting
            var round = await _roundRepository.GetRoundByIdAsync(command.RoundId);
            if (round.Status != RoundsStatus.Voting)
            {
                throw new ConflictException();
            }

            // Verifica si el jugador ya voto
            if (await _roundVoteRepository.GetRoundVoteByGameIdByPlayerNameAsync(command.RoundId, command.PlayerName) != null)
            {
                throw new PreconditionRequiredException();
            }

            // Crea el voto
            var roundVote = new RoundVote
            {
                RoundId = command.RoundId,
                PlayerName = command.PlayerName,
                Vote = command.Vote,
                GroupVote = command.GroupVote
            };

            await _roundVoteRepository.CreateRoundVoteAsync(roundVote);
            return roundVote;

        }
    }
}