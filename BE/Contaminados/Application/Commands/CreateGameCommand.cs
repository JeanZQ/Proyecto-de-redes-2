namespace Contaminados.Application.Commands
{
    public class CreateGameCommand
    {
        public required string Name { get; set; }
        public string? Password { get; set; }
        public required string Owner { get; set; }
        public CreateGameCommand(string name, string? password, string owner)
        {
            Name = name;
            Password = password;
            Owner = owner;
        }
    }
}