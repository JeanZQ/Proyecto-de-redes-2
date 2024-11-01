using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundGroupModels;
using Models.roundModels;

namespace Contaminados.Application.Handlers
{
    public class CreateRoundGroupHandler
    {
        private readonly IRoundGroupRepository<RoundGroup> _roundGroupRepository;
        private readonly IRoundRepository<Round> _roundRepository;
        public CreateRoundGroupHandler(IRoundGroupRepository<RoundGroup> roundGroupRepository, IRoundRepository<Round> roundRepository)
        {
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
        }
        public async Task<RoundGroup> HandleAsync(CreateRoundGroupCommand command)
        {   
            try
            {
                var round = await _roundRepository.GetRoundByIdAsync(command.RoundId);

                //Verifica si el estado del round es WaitingOnLeader
                if(round.Status != RoundsStatus.WaitingOnLeader)
                {
                    throw new ConflictException();
                }
              
                var roundGroup = new RoundGroup
                {
                    RoundId = command.RoundId,
                    Player = command.Player
                };

                await _roundGroupRepository.CreateRoundGroupAsync(roundGroup);

                return roundGroup;
            }
            catch (CustomException)
            {
                throw new NotFoundException();
            }
        }
    }
}