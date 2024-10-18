using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{
    public class CreateRoundVoteHandler
    {
        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;
        public CreateRoundVoteHandler(IRoundVoteRepository<RoundVote> roundVoteRepository)
        {
            _roundVoteRepository = roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
        }
        public async Task<RoundVote> HandleAsync(CreateRoundVoteCommand command)
        {
            if (command.RoundId == Guid.Empty)
            {
                throw new ClientException(); //Revizar si es la excepcion correcta
            }
            var roundVote = new RoundVote
            {
                RoundId = command.RoundId,
                Vote = command.Vote
            };
            try
            {
                await _roundVoteRepository.CreateRoundVoteAsync(roundVote);
                return roundVote;
            }
            catch (Exception)
            {
                throw new ConflictException(); //Revizar si es la excepcion correcta
            }
        }
    }
}