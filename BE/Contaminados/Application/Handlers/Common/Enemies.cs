namespace Application.Handlers.Common

{

    public sealed class Enemies
    {

        private static readonly Enemies instance = new Enemies();

        private Enemies()
        {
        }

        public static Enemies Instance
        {
            get
            {
                return Instance;
            }
        }

        private static readonly int[] enemies = new int[6]
        {
            2, 2, 3, 3, 3, 4  // Enemigos
        };

        public int GetEnemies(int totalPlayers)
        {
            return enemies[totalPlayers - 5];
        }

    }

}

