using System.ComponentModel.DataAnnotations;
using Models.Common;

namespace Models.gameModels
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(20, MinimumLength = 3)]
        public required string Name { get; set; }
        public required Status GameStatus { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public string? Password { get; set; }

        public Guid? CurrentRoundId { get; set; }
    }
}