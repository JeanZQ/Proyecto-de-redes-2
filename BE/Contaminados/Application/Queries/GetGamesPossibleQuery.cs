using Contaminados.Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Contaminados.Application.Queries
{

    public class GetGamesPossibleQuery
    {
        [StringLength(20, MinimumLength = 3)]
        public string? Name { get; }

        public Status? Status { get; }

        [Range(1, int.MaxValue)]
        public int? Page { get; }

        [Range(0, 50)]
        public int? Limit { get; }

        public GetGamesPossibleQuery(string? name, Status? status, int? page, int? limit)
        {
            Name = name;
            Status = status;
            Page = page;
            Limit = limit;
        }
    }
}
