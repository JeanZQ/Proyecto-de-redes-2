using Contaminados.Models.Common;

namespace Contaminados.Aplication.Commands
{
    public class UpdateRoundCommand
    {
        public Guid Id { get; set; }
        public string Leader { get; set; }
        public RoundsStatus Status { get; set; }
        public RoundsResult Result { get; set; }
        public RoundsPhase Phase { get; set; }
        public Guid GameId { get; set; }
        public UpdateRoundCommand(Guid id, string leader, RoundsStatus status, RoundsResult result, RoundsPhase phase, Guid gameId)
        {
            Id = id;
            Leader = leader;
            Status = status;
            Result = result;
            Phase = phase;
            GameId = gameId;
        }
    }
}