using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.playersModels;
using Models.roundGroupModels;
using Models.roundVoteModels;

namespace Contaminados.Application.Handlers
{

    public class UpdateRoundVoteHandler
    {

        private readonly IRoundVoteRepository<RoundVote> _roundVoteRepository;
        private readonly IRoundGroupRepository<RoundGroup> _roundGroupRepository;
        public UpdateRoundVoteHandler(IRoundVoteRepository<RoundVote> roundVoteRepository, IRoundGroupRepository<RoundGroup> roundGroupRepository)
        {
            _roundVoteRepository = roundVoteRepository ?? throw new ArgumentNullException(nameof(roundVoteRepository));
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
        }

        public async Task HandleAsync(UpdateRoundVoteCommand command)
        {

            var roundVote = await _roundVoteRepository.GetRoundVoteByGameIdByPlayerNameAsync(command.RoundId, command.PlayerName) ?? throw new NotFoundException();
            var roundGroup = await _roundGroupRepository.GetAllRoundGroupByRoundIdAsync(command.RoundId) ?? throw new NotFoundException();
            //Verifica si el jugador pertenece a un grupo y que no haya votado
            if (roundGroup.Any(x => x.Player == command.PlayerName) && roundVote.Vote == Vote.NA)
            {
                roundVote.Vote = command.Vote;
                roundVote.GroupVote = command.GroupVote != null ? command.GroupVote : roundVote.GroupVote;
                await _roundVoteRepository.UpdateRoundVoteAsync(roundVote);
            }
            else
            {
                throw new PreconditionRequiredException();
            }


        }

    }

}