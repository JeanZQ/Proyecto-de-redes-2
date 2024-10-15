using Contaminados.Aplication.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundModels;

namespace Contaminados.Aplication.Handlers
{
    public class CreateRoundHandler
    {
        private readonly IRoundRepository<Round> _roundRepository;
        public CreateRoundHandler(IRoundRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
        }
        public async Task<Round> HandleAsync(CreateRoundCommand command)
        {
            // Faltan validaciones
            var round = new Round
            {
                Leader = command.Leader,
                Status = command.Status,
                Result = command.Result,
                Phase = command.Phase,
                GameId = command.GameId
            };
            try
            {
                await _roundRepository.CreateRoundAsync(round);
                return round;
            }
            catch (Exception)
            {
                throw new ConflictException();//Revisar
            }
        }
    }
}