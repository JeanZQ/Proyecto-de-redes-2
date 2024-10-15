using Contaminados.Aplication.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.Repository;
using Models.roundGroupModels;

namespace Contaminados.Aplication.Handlers
{
    public class CreateRoundGroupHandler
    {
        private readonly RoundGroupRepository _roundGroupRepository;
        public CreateRoundGroupHandler(RoundGroupRepository roundGroupRepository)
        {
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
        }
        public async Task<RoundGroup> HandleAsync(CreateRoundGroupCommand command)
        {
            if (command.RoundId == Guid.Empty || command.PlayerId == Guid.Empty)
            {
                throw new ClientException(); //Revizar si es la excepcion correcta
            }
            var roundGroup = new RoundGroup
            {
                RoundId = command.RoundId,
                PlayerId = command.PlayerId
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