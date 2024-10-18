namespace Contaminados.Application.Commands
{
    public class CreateRoundGroupCommand
    {
        public Guid RoundId { get; set; }
        public string Player { get; set; }
        public CreateRoundGroupCommand(Guid roundId, string player)
        {
            RoundId = roundId;
            Player = player;
        }
    }
}