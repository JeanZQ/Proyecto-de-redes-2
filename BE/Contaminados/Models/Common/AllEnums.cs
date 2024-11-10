using System.ComponentModel;
using System.Reflection;

namespace Contaminados.Models.Common
{
    public enum Status
    {
        lobby,
        rounds,
        ended
    }

    public enum RoundsStatus
    {
        [Description("waiting-on-leader")]
        WaitingOnLeader,
        [Description("voting")]
        voting,
        [Description("waiting-on-group")]
        WaitingOnGroup,
        [Description("ended")]
        ended
    }

    public enum RoundsResult
    {
        none,
        citizens,
        enemies
    }
    
    public enum RoundsPhase
    {
        vote1,
        vote2,
        vote3,
    }

    public enum Vote
    {
        Yes,
        No,
        NA
    }


}