using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.roundModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetRoundByIdHandler
    {
        private readonly IRoundRepository<Round> _roundRepository;
        public GetRoundByIdHandler(IRoundRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
        }
        public async Task<Round> HandleAsync(GetRoundByIdQuery request)
        {
            //Falta validaciones-------------------------------------
            var round = await _roundRepository.GetRoundByIdAsync(request.Id);
            return round;
        }
    }
}