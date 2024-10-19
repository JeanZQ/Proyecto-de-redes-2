namespace Contaminados.Application.Commands
{
    public class CreatePlayerCommand
    {
        public string Name { get; set; }
        public Guid GameId { get; set; }
        public CreatePlayerCommand(string name, Guid gameId)
        {
            Name = name;
            GameId = gameId;
        }
    }
}