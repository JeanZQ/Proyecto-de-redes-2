using Contaminados.Models.Common;
using Models.roundGroupModels;
using Models.roundModels;
using Models.roundVoteModels;

namespace API.Controllers.Common
{
    public class StatusCodesOkRoundsCommon
    {
        private int Status { get; set; }
        private string Msg { get; set; }
        private DataRounds Data { get; set; }
        public StatusCodesOkRoundsCommon(Round round, IEnumerable<RoundGroup> group, IEnumerable<RoundVote> votes, string msg)
        {
            Status = 200;
            Msg = msg;
            Data = new DataRounds
            {
                Id = round.Id,
                Leader = round.Leader,
                Status = round.Status.ToString(),
                Result = round.Result.ToString(),
                Phase = round.Phase.ToString(),
                //CreatedAt = round.CreatedAt,
                //UpdateAt = round.UpdateAt,
                Group = group.Select(g => g.Player).ToArray(),
                Votes = votes.Select(v => v.GroupVote == Vote.Yes ? true : false).ToArray()
            };
        }
        public StatusCodesOkRounds GetStatusCodesOkRounds()
        {
            return new StatusCodesOkRounds
            {
                Status = Status,
                Msg = Msg,
                Data = Data
            };
        }
    }
}