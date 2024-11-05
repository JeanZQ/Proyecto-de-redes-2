using Contaminados.Models.Common;

namespace Contaminados.Application.Commands
{

    public class UpdateRoundVoteCommand
    {
        public Guid GameId { get; set; }
        public Guid RoundId { get; set; }
        public string PlayerName { get; set; }
        public Vote Vote { get; set; }
        public Vote? GroupVote { get; set; }
        public UpdateRoundVoteCommand(Guid gameId, string playerName, Guid roundId, Vote vote, Vote? groupVote)
        {
            GameId = gameId;
            RoundId = roundId;
            PlayerName = playerName;
            Vote = vote;
            GroupVote = groupVote;
        }
    }
}