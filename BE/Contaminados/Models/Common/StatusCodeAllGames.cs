namespace Contaminados.Models.Common
{
    public class StatusCodeAllGames
    {
        
            public required int Status { get; set; }
            public required string Msg { get; set; }
            public required IEnumerable<Data> Data { get; set; }

    }
}
