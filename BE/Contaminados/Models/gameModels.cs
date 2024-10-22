using System.ComponentModel.DataAnnotations;
using Contaminados.Models.Common;

namespace Models.gameModels
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(20, MinimumLength = 3)]
        public required string Name { get; set; }
        public required Status GameStatus { get; set; }

        public required DateTime CreatedAt { get; set; } = DateTime.Now;

        public required DateTime UpdatedAt { get; set; } = DateTime.Now;

        [StringLength(20, MinimumLength = 3)]
        public string? Password { get; set; }

        public Guid? CurrentRoundId { get; set; }
        public required string Owner { get; set; }
    }
}