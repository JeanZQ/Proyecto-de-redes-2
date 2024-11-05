namespace Contaminados.Models.Common
{
    public class StatusCodesOkRounds
    {
        public required int Status { get; set; }
        public required string Msg { get; set; }
        public required object? Data { get; set; }
    }
    public class DataRounds
    {
        public required Guid Id { get; set; }
        public required string Leader { get; set; }
        public required string Status { get; set; }
        public required string Result { get; set; }
        public required string Phase { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public required IEnumerable<string> Group { get; set; }
        public required IEnumerable<bool> Votes { get; set; }
    }
}