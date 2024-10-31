using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.roundModels;

namespace Contaminados.Application.Handlers
{
    public class UpdateRoundHandler
    {
        private readonly IRoundRepository<Round> _roundRepository;
        public UpdateRoundHandler(IRoundRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
        }
        public async Task<Round> HandleAsync(UpdateRoundCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                throw new ClientException(); //Revizar si es la excepcion correcta
            }
            var round = new Round
            {
                Id = command.Id,
                Leader = command.Leader,
                Status = command.Status,
                Result = command.Result,
                Phase = command.Phase,
                GameId = command.GameId
            };
            try
            {
                await _roundRepository.UpdateRoundAsync(round);
                return round;
            }
            catch (Exception)
            {
                throw new ConflictException();//Revizar si es la excepcion correcta
            }
        }
    }
}