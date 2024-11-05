namespace Contaminados.Models.Common
{
    public class StatusCodesOk
    {
        public required int Status { get; set; }
        public required string Msg { get; set; }
        public required Data Data { get; set; }
    }
    public class Data
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required bool Password { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdateAt { get; set; }
        public required Guid? CurrentRound { get; set; }
        public required IEnumerable<string> Players { get; set; }
        public required IEnumerable<string> Enemies { get; set; }

    }
}