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
            if (command.RoundId == Guid.Empty || string.IsNullOrWhiteSpace(command.Player))
            {
                throw new ClientException(); //Revizar si es la excepcion correcta
            }
            var roundGroup = new RoundGroup
            {
                RoundId = command.RoundId,
                Player = command.Player
            };
            try
            {
                await _roundGroupRepository.CreateRoundGroupAsync(roundGroup);
                return roundGroup;
            }
            catch (Exception){
                throw new ConflictException(); //Revizar si es la excepcion correcta
            }
        }
    }
}