using Contaminados.Application.Commands;
using Contaminados.Repositories.IRepository;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{
    public class DeleteAllRoundVotesByRoundIdHandler
    {
        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;
        public DeleteAllRoundVotesByRoundIdHandler(IRoundVoteRepository<RoundVote> roundVoteRepository)
        {
            _roundVoteRepository = roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
        }
        public async Task HandleAsync(DeleteAllRoundVotesByRoundIdCommand command)
        {
            // Falta validaciones
            try
            {
                await _roundVoteRepository.DeleteAllRoundVotesByRoundIdAsync(command.RoundId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}