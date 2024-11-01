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
            try
            {
                //busca en la base de datos el round
                var round = await _roundRepository.GetRoundByIdAsync(command.Id);

                round.Phase = command.Phase;
                round.Result = command.Result;
                round.Status = command.Status;
                
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