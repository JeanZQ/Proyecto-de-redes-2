using Contaminados.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contaminados.Aplication.Queries
{

    public class GetGamesPossibleQuery
    {
        public string Player { get; }

        public Status Status { get; }

        public int Page { get; }

        public int Limit { get; }

        public GetGamesPossibleQuery(string player, Status status, int page, int limit)
        {
            Player = player;
            Status = status;
            Page = page;
            Limit = limit;
        }
    }
}
