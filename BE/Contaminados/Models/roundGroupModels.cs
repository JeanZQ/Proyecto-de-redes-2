using System.ComponentModel.DataAnnotations;

namespace Models.roundGroupModels
{
    public class RoundGroup
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid RoundId { get; set; }
        public required Guid PlayerId { get; set; }
    }
}