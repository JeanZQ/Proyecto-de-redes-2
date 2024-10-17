namespace Contaminados.Aplication.Commands
{
    public class CreateRoundGroupCommand
    {
        public Guid RoundId { get; set; }
        public Guid PlayerId { get; set; }
        public CreateRoundGroupCommand(Guid roundId, Guid playerId)
        {
            RoundId = roundId;
            PlayerId = playerId;
        }
    }
}