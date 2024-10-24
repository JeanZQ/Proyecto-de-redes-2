using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundGroupModels;

namespace Contaminados.Application.Handlers
{
    public class CreateRoundGroupHandler
    {
        private readonly IRoundGroupRepository<RoundGroup> _roundGroupRepository;
        public CreateRoundGroupHandler(IRoundGroupRepository<RoundGroup> roundGroupRepository)
        {
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
        }
        public async Task<RoundGroup> HandleAsync(CreateRoundGroupCommand command)
        {
            if (command.Round.Id == Guid.Empty || string.IsNullOrWhiteSpace(command.Player))
            {
                throw new NotFoundException();
            }
            try
            {
                //Verifica si el estado del round es WaitingOnGroup
                if(command.Round.Status != RoundsStatus.WaitingOnGroup)
                {
                    throw new ConflictException();
                }

                var roundGroup = new RoundGroup
                {
                    RoundId = command.Round.Id,
                    Player = command.Player
                };

                await _roundGroupRepository.CreateRoundGroupAsync(roundGroup);

                return roundGroup;
            }
            catch (Exception)
            {
                throw new PreconditionRequiredException();//revizar
            }
        }
    }
}