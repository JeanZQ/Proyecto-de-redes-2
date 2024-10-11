namespace Contaminados.Aplication.Commands
{
    public class CreateGameCommand
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
        public CreateGameCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}