using Contaminados.Application.Queries;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundModels;

namespace Contaminados.Application.Handlers
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

            try
            {
                var rounds = await _roundRepository.GetAllRoundByGameIdAsync(query.GameId);
                return rounds;
            }
            catch (CustomException)
            {
                throw new NotFoundException();
            }
        }
    }
}