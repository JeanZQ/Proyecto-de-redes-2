using System.ComponentModel.DataAnnotations;

namespace Models.roundVoteModels
{
    public class RoundVote
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid RoundId { get; set; }
        public required bool Vote { get; set; }
    }
}