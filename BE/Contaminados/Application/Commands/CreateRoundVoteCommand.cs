using Contaminados.Models.Common;
using Models.roundModels;

namespace Contaminados.Application.Commands
{
    public class CreateRoundVoteCommand
    {
        public Guid RoundId { get; set; }
        public string PlayerName { get; set; }
        public Vote Vote { get; set; }
        public Vote GroupVote { get; set; }
        public CreateRoundVoteCommand(Guid roundId, string playerName, Vote vote, Vote groupVote)
        {
            RoundId = roundId;
            PlayerName = playerName;
            Vote = vote;
            GroupVote = groupVote;
        }
    }
}