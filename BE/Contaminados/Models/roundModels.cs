using System.ComponentModel.DataAnnotations;

namespace Models.roundModels
{
    public class Round
    {
        [Key]
        public Guid Id { get; set; }
        public required Guid LeaderId { get; set; }
        public required RoundStatus Status { get; set; }
        public required RoundResult Result { get; set; }
        public required RoundPhase Phase { get; set; }
        public required Guid GameId { get; set; }
    }

    public enum RoundStatus
    {
        WaitingOnLeader,
        Voting,
        WaitingOnGroup,
        Ended
    }

    public enum RoundResult
    {
        None,
        Citizens,
        Enemies
    }

    public enum RoundPhase
    {
        Vote1,
        Vote2,
        Vote3
    }
}