using Models.gameModels;
using Models.roundModels;

namespace Contaminados.Application.Commands
{
    public class CreateRoundGroupCommand
    {
        public Round Round { get; set; }
        public string Player { get; set; }
        public CreateRoundGroupCommand(Round round, string player)
        {
            Round = round;
            Player = player;
        }
    }
}