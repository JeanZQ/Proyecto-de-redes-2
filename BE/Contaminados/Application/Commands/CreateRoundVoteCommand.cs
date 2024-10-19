namespace Contaminados.Application.Commands
{
    public class CreateRoundVoteCommand
    {
        public Guid RoundId { get; set; }
        public bool Vote { get; set; }
        public CreateRoundVoteCommand(Guid roundId, bool vote)
        {
            RoundId = roundId;
            Vote = vote;
        }
    }
}