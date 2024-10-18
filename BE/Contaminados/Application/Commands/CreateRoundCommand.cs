using Contaminados.Models.Common;

namespace Contaminados.Application.Commands
{
    public class CreateRoundCommand
    {
        public required string Leader { get; set; }
        public required RoundsStatus Status { get; set; }
        public required RoundsResult Result { get; set; }
        public required RoundsPhase Phase { get; set; }
        public required Guid GameId { get; set; }
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