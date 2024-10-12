namespace Contaminados.Aplication.Commands
{
    public class CreatePlayerCommand
    {
        public required string Name { get; set; }
        public required Guid GameId { get; set; }
        public CreatePlayerCommand(string name, Guid gameId)
        {
            Name = name;
            GameId = gameId;
        }
    }
}