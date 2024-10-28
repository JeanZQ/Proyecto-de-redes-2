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
            if (command.Round.Id == Guid.Empty)
            {
                throw new NotFoundException();
            }
            try
            {
                //Verifica si el estado del round es Voting
                if (command.Round.Status != RoundsStatus.Voting)
                {
                    throw new ConflictException();
                }
                
                var roundVote = new RoundVote
                {
                    RoundId = command.Round.Id,
                    Vote = command.Vote
                };

                await _roundVoteRepository.CreateRoundVoteAsync(roundVote);
                return roundVote;
            }
            catch (Exception)
            {
                throw new ConflictException(); //Revisar si es la excepcion correcta
            }
        }
    }
}