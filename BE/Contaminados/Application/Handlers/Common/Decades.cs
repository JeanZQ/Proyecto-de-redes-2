using Contaminados.Models.Common;

namespace Application.Handlers.Common
{
    public sealed class Decades
    {

        private static readonly Decades instance = new Decades();

        private Decades()
        {
        }

        public static Decades Instance
        {
            get
            {
                return instance;
            }
        }


        private static readonly int[,] groups = new int[5, 6]
        {
            { 2, 2, 2, 3, 3, 3 }, // Grupo para década 1
            { 3, 3, 3, 4, 4, 4 }, // Grupo para década 2
            { 2, 4, 3, 4, 4, 4 }, // Grupo para década 3
            { 3, 3, 4, 5, 5, 5 }, // Grupo para década 4
            { 3, 4, 4, 5, 5, 5 }  // Grupo para década 5
        };


        public int GetGroups(int decade, int totalPlayers)
        {

            if (totalPlayers < 5 || totalPlayers > 10)
            {
                throw new sizeOfGroupExeption();
            }

            return groups[decade, totalPlayers - 5];
        }
    }
}
