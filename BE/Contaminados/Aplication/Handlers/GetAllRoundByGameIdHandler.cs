using Contaminados.Aplication.Queries;
using Contaminados.Repositories.IRepository;
using Models.roundModels;

namespace Contaminados.Aplication.Handlers
{
    public class GetAllRoundByGameIdHandler
    {
        private readonly IRoundRepository<Round> _roundRepository;
        public GetAllRoundByGameIdHandler(IRoundRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
        }
        public async Task<IEnumerable<Round>> HandleAsync(GetAllRoundByGameIdQuery query)
        {
            //Falta validaciones-------------------------------------
            var rounds = await _roundRepository.GetAllRoundByGameIdAsync(query.GameId);
            return rounds;
        }
    }
}