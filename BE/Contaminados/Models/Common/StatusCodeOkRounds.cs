namespace Contaminados.Models.Common
{
    public class StatusCodesOkRounds
    {
        public required int Status { get; set; }
        public required string Msg { get; set; }
        public required DataRounds Data { get; set; }
    }
    public class DataRounds
    {
        public required Guid Id { get; set; }
        public required string Leader { get; set; }
        public required RoundsStatus Status { get; set; }
        public required RoundsResult Result { get; set; }
        public required RoundsPhase Phase { get; set; }
        public required string[] Group { get; set; }
        public required bool[] Votes { get; set; }
    }
}