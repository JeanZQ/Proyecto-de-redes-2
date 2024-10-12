using System.ComponentModel.DataAnnotations;

namespace Models.playersModels
{
    public class Players
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid GameId { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public required string PlayerName { get; set; }
        public bool? IsEnemy { get; set; }
    }
}