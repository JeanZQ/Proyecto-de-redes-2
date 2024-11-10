using System.Text.Json.Serialization;

namespace Contaminados.Models.Common
{
    public class StatusCodesOk
    {
        [JsonPropertyOrder(1)]
        public required int Status { get; set; }

        [JsonPropertyOrder(2)]
        public required string Msg { get; set; }

        [JsonPropertyOrder(3)]
        public required Data Data { get; set; }

    }
    public class Data
    {
        [JsonPropertyOrder(10)]
        public required string Id { get; set; }

        [JsonPropertyOrder(2)]
        public required string Owner { get; set; }

        [JsonPropertyOrder(1)]
        public required string Name { get; set; }

        [JsonPropertyOrder(3)]
        public required string Status { get; set; }

        [JsonPropertyOrder(6)]
        public required bool Password { get; set; }

        [JsonPropertyOrder(4)]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyOrder(5)]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyOrder(9)]
        public required Guid? CurrentRound { get; set; }

        [JsonPropertyOrder(7)]
        public required IEnumerable<string> Players { get; set; }

        [JsonPropertyOrder(8)]
        public required IEnumerable<string> Enemies { get; set; }

    }
}