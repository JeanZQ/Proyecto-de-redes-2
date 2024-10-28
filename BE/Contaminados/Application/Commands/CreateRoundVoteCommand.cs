using Models.roundModels;

namespace Contaminados.Application.Commands
{
    public class CreateRoundVoteCommand
    {
        public Round Round { get; set; }
        public bool Vote { get; set; }
        public CreateRoundVoteCommand(Round round, bool vote)
        {
            Round = round;
            Vote = vote;
        }
    }
}