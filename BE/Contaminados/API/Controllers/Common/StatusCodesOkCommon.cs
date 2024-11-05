using Contaminados.Models.Common;
using Models.gameModels;
using Models.playersModels;
using Models.roundModels;

namespace API.Controllers.Common
{
    public class StatusCodesOkCommon
    {
        private int Status { get; set; }
        private string Msg { get; set; }
        private Data Data { get; set; }
        public StatusCodesOkCommon(Game game, IEnumerable<Players> players, string msg)
        {
            Status = 200;
            Msg = msg;
            Data = new Data
            {
                Id = game.Id.ToString(),
                Name = game.Name,
                Status = game.GameStatus.ToString(),
                Password = game.Password?.Length != 0,
                CurrentRound = game.CurrentRoundId,
                CreatedAt = game.CreatedAt,
                UpdateAt = game.UpdatedAt,
                Players = players.Select(p => p.PlayerName.ToString()).ToArray(),
                Enemies = players.Where(p => p.IsEnemy == true).Select(p => p.PlayerName.ToString()).ToArray()
            };
        }
        public StatusCodesOk GetStatusCodesOk()
        {
            return new StatusCodesOk
            {
                Status = Status,
                Msg = Msg,
                Data = Data
            };
        }
    }
}