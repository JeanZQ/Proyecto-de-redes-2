using System.ComponentModel.DataAnnotations;
using Contaminados.Models.Common;

namespace Models.roundVoteModels
{
    public class RoundVote
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid RoundId { get; set; }
        public string PlayerName { get; set; }
        public required Vote Vote { get; set; }
        public required Vote? GroupVote { get; set; }
    }
}