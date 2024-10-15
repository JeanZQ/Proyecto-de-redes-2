using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.roundGroupModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetAllRoundGroupByRoundIdHandler
    {
        private readonly IRoundGroupRepository<RoundGroup> _roundGroupRepository;
        public GetAllRoundGroupByRoundIdHandler(IRoundGroupRepository<RoundGroup> roundGroupRepository)
        {
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
        }
        public async Task<IEnumerable<RoundGroup>> HandleAsync(GetAllRoundGroupByRoundIdQuery request)
        {
            //Falta validaciones-------------------------------------
            var roundGroups = await _roundGroupRepository.GetAllRoundGroupByRoundIdAsync(request.RoundId);
            return roundGroups;
        }
    }
}