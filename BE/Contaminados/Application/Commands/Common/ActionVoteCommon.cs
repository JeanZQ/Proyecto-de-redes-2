namespace Application.Commands.Common
{
    public class ActionVoteCommon
    {
        public bool Action { get; set; }
        public ActionVoteCommon(bool action)
        {
            Action = action;
        }
    }
}