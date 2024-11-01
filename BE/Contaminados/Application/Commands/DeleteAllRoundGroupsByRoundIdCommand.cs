namespace Contaminados.Application.Commands
{
    public class DeleteAllRoundGroupsByRoundIdCommand
    {
        public Guid RoundId { get; set; }
        public DeleteAllRoundGroupsByRoundIdCommand(Guid roundId)
        {
            RoundId = roundId;
        }
    }
}