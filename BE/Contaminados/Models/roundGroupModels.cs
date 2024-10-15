using System.ComponentModel.DataAnnotations;

namespace Models.roundGroupModels
{
    public class RoundGroup
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid RoundId { get; set; }
        public required Guid PlayerId { get; set; }
    }
}