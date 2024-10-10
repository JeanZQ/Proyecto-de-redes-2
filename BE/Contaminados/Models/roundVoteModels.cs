using System.ComponentModel.DataAnnotations;

namespace Models.roundVoteModels
{
    public class RoundVote
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid RoundId { get; set; }
        public bool Vote { get; set; }
    }
}