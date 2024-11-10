using Contaminados.Application.Commands;
using Contaminados.Repositories.IRepository;
using Models.roundGroupModels;

namespace Contaminados.Application.Handlers
{
    public class DeleteAllRoundGroupsByRoundIdHandler
    {
        private readonly IRoundGroupRepository<RoundGroup> _roundGroupRepository;
        public DeleteAllRoundGroupsByRoundIdHandler(IRoundGroupRepository<RoundGroup> roundGroupRepository)
        {
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
        }
        public async Task HandleAsync(DeleteAllRoundGroupsByRoundIdCommand command)
        {
            // Falta validaciones
            try
            {
                await _roundGroupRepository.DeleteAllRoundGroupsByRoundIdAsync(command.RoundId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}