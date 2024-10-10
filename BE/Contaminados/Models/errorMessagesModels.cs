using System.ComponentModel.DataAnnotations;

namespace Models.errorMessagesModels
{
    public class ErrorMessages
    {
        [Key]
        public Guid Id { get; set; }
        public required string ErrorMessage { get; set; }
        public int Code { get; set; }
    }
}