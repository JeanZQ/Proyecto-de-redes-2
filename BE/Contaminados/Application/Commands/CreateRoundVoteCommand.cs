using Contaminados.Models.Common;
using Models.roundModels;

namespace Contaminados.Application.Commands
{
    public class CreateRoundVoteCommand
    {
        public Round Round { get; set; }
        public string PlayerName { get; set; }
        public Vote Vote { get; set; }
        public Vote GroupVote { get; set; }
        public CreateRoundVoteCommand(Round round, string playerName, Vote vote, Vote groupVote)
        {
            Round = round;
            PlayerName = playerName;
            Vote = vote;
            GroupVote = groupVote;
        }
    }
}