using Contaminados.Models.Common;

namespace Contaminados.Application.Commands
{
    public class CreateRoundCommand
    {
        public string Leader { get; set; }
        public RoundsStatus Status { get; set; }
        public RoundsResult Result { get; set; }
        public RoundsPhase Phase { get; set; }
        public Guid GameId { get; set; }
        public CreateRoundCommand( string leader, RoundsStatus status, RoundsResult result, RoundsPhase phase, Guid gameId)
        {
            Leader = leader;
            Status = status;
            Result = result;
            Phase = phase;
            GameId = gameId;
        }
    }
}