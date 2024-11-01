using Contaminados.Models.Common;
using Models.roundModels;

namespace Contaminados.Application.Commands
{

    public class UpdateRoundVoteCommand
    {
        public Guid RoundId { get; set; }
        public string PlayerName { get; set; }
        public Vote Vote { get; set; }
        public Vote? GroupVote { get; set; }
        public UpdateRoundVoteCommand(string playerName, Guid roundId, Vote vote, Vote? groupVote)
        {
            RoundId = roundId;
            PlayerName = playerName;
            Vote = vote;
            GroupVote = groupVote;
        }
    }
}