using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{

    public class UpdateRoundVoteHandler
    {

        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;

        public UpdateRoundVoteHandler(IRoundVoteRepository<RoundVote> roundVoteRepository)
        {
            _roundVoteRepository = roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
        }

        public async Task<RoundVote> HandleAsync(UpdateRoundVoteCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                throw new ClientException(); //Revisar si es la excepcion correcta
            }
            var roundVote = new RoundVote
            {
                Id = command.Id,
                RoundId = command.RoundId,
                PlayerName = command.PlayerName,
                Vote = command.Vote,
                GroupVote = command.GroupVote
            };
            try
            {
                await _roundVoteRepository.UpdateRoundVoteAsync(roundVote);
                return roundVote;
            }
            catch (Exception)
            {
                throw new ConflictException();//Revisar si es la excepcion correcta
            }
        }

    }

}