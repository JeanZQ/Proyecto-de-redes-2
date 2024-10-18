namespace Application.Commands.Common
{
    public class GroupCommon
    {
        public IEnumerable<string> Group { get; set; }
        public GroupCommon(IEnumerable<string> group)
        {
            Group = group;
        }
    }
}