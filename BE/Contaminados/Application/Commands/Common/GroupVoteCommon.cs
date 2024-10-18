namespace Application.Commands.Common
{
    public class GroupVoteCommon
    {
        public bool Vote { get; set; }
        public GroupVoteCommon(bool vote)
        {
            Vote = vote;
        }
    }
}