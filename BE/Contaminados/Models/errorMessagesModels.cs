using System.ComponentModel.DataAnnotations;

namespace Models.errorMessagesModels
{
    public class ErrorMessages
    {
        [Key]
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string ErrorMessage { get; set; }
        public required int Code { get; set; }
    }
}