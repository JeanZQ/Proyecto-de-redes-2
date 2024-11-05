namespace Utilities
{
    public class GetRandomLeader
    {
        public static string GetRandomLeaderName(string[] players)
        {
            Random random = new Random();
            int index = random.Next(players.Length);
            return players[index];
        }
    }
}