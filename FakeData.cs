using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGame
{
    public static class FakeData
    {
      
         static List<Player> listOfPlayers = new List<Player>
         {
                new Player
                {               
                    PlayerName = "imad",
                    TotalGames = 1,
                    TotalGuesses =10
                } ,new Player
                {
                    PlayerName = "Adam",
                    TotalGames = 8,
                    TotalGuesses = 2
                },new Player
                {
                    PlayerName = "Fredrik",
                    TotalGames = 1,
                    TotalGuesses = 15
                },new Player
                {
                    PlayerName = "Tim",
                    TotalGames = 8,
                    TotalGuesses = 4
                }
         };

        public static List<Player> GetFakeListOfPlayers()
        {
            return listOfPlayers;
        }

        public static List<Player> GetEmptyPlayersList()
        {
            List<Player> list = new List<Player>();
            return list;
        }
    }
}
