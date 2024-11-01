namespace Contaminados.Application.Commands
{
    public class DeleteAllRoundVotesByRoundIdCommand
    {
        public Guid RoundId { get; set; }
        public DeleteAllRoundVotesByRoundIdCommand(Guid roundId)
        {
            RoundId = roundId;
        }
    }
}