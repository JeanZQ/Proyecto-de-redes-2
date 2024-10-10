using System.ComponentModel.DataAnnotations;

namespace Models.gameModels
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        public required string GameName { get; set; }

        public required Status GameStatus { get; set; }
        
        [StringLength(20, MinimumLength = 3)]
        public required string GamePassword { get; set; }

        public Guid? CurrentRoundId { get; set; }

        public enum Status
        {
            Lobby,
            Rounds,
            Ended
        }
    }
}