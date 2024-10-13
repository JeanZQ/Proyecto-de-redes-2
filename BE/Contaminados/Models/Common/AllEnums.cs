namespace Contaminados.Models.Common
{
    public enum Status
    {
        Lobby,
        Rounds,
        Ended
    }

    public enum RoundsStatus
    {
        WaitingOnLeader,
        Voting,
        WaitingOnGroup,
        Ended
    }

    public enum RoundsResult
    {
        none,
        Citizens,
        Enemies
    }
    
    public enum RoundsPhase
    {
        Vote1,
        Vote2,
        Vote3
    }
}