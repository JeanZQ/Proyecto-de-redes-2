using System.Text.Json.Serialization;

namespace Contaminados.Models.Common
{
    public class StatusCodesOkRounds
    {
        [JsonPropertyOrder(1)]
        public required int Status { get; set; }

        [JsonPropertyOrder(2)]
        public required string Msg { get; set; }

        [JsonPropertyOrder(3)]
        public required object? Data { get; set; }

    }
    public class DataRounds
    {
        [JsonPropertyOrder(9)]
        public required Guid Id { get; set; }

        [JsonPropertyOrder(4)]
        public required string Leader { get; set; }

        [JsonPropertyOrder(1)]
        public required string Status { get; set; }

        [JsonPropertyOrder(3)]
        public required string Result { get; set; }

        [JsonPropertyOrder(2)]
        public required string Phase { get; set; }

        [JsonPropertyOrder(5)]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyOrder(6)]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyOrder(7)]
        public required IEnumerable<string> Group { get; set; }

        [JsonPropertyOrder(8)]
        public required IEnumerable<bool> Votes { get; set; }

    }
}