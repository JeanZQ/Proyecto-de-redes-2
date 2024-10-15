using System.ComponentModel.DataAnnotations;
using Contaminados.Models.Common;

namespace Models.roundModels
{
    public class Round
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Leader { get; set; }
        public required RoundsStatus Status { get; set; }
        public required RoundsResult Result { get; set; }
        public required RoundsPhase Phase { get; set; }
        public required Guid GameId { get; set; }
    }
}