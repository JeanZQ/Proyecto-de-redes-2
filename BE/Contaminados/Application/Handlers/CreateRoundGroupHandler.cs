using Application.Handlers.Common;
using Contaminados.Application.Commands;
using Contaminados.Models.Common;
using Contaminados.Repositories.IRepository;
using Models.playersModels;
using Models.roundGroupModels;
using Models.roundModels;

namespace Contaminados.Application.Handlers
{
    public class CreateRoundGroupHandler
    {
        private readonly IRoundGroupRepository<RoundGroup> _roundGroupRepository;
        private readonly IRoundRepository<Round> _roundRepository;
        private readonly IPlayerRepository<Players> _playerRepository;
        public CreateRoundGroupHandler(IRoundGroupRepository<RoundGroup> roundGroupRepository, IRoundRepository<Round> roundRepository, IPlayerRepository<Players> playerRepository)
        {
            _roundGroupRepository = roundGroupRepository ?? throw new ArgumentNullException(nameof(roundGroupRepository));
            _roundRepository = roundRepository ?? throw new ArgumentNullException(nameof(roundRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }
        public async Task HandleAsync(CreateRoundGroupCommand command)
        {

            //var round = await _roundRepository.GetRoundByIdAsync(command.RoundId);
            var rounds  = await _roundRepository.GetAllRoundByGameIdAsync(command.GameId);
            var round = rounds.Select(r => r).Where(r => r.Id == command.RoundId).FirstOrDefault();
            var playerList = await _playerRepository.GetAllPlayersByGameIdAsync(command.GameId);
            Decades decades = Decades.Instance;

            //Verifica si el estado del round es WaitingOnLeader
            if (round.Status != RoundsStatus.WaitingOnLeader)
            {
                throw new PreconditionRequiredException();
            }

            //Verificar si la consulta es del lider
            if (string.Compare(round.Leader, command.Leader) != 0)
            {
                throw new PreconditionRequiredException();
            }

            //Verificamos que el numero de jugadores sea el correcto
            if (command.Players.Count() != decades.GetGroups(rounds.Count(), playerList.Count()))
            {
                throw new sizeOfGroupExeption();
            }

            //Verificamos que los jugadores esten en la lista de jugadores
            if (!command.Players.All(p => playerList.Any(pl => pl.PlayerName == p)))
            {
                throw new ForbiddenException();
            }

            //Guarda los jugadores en la tabla RoundGroup
            foreach (var p in command.Players)
            {
                var roundGroup = new RoundGroup
                {
                    RoundId = command.RoundId,
                    Player = p
                };

                await _roundGroupRepository.CreateRoundGroupAsync(roundGroup);
            }
        }
    }
}