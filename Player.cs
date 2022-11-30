using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooGame
{
   public class Player
    {
        public string PlayerName { get; private set; }
        public int TotalGames { get; private set; }
        int totalGuesses;


        public Player(string playerName, int totalGuesses)
        {
            this.PlayerName = playerName;
            TotalGames = 1;
            this.totalGuesses = totalGuesses;
        }

        public void Update(int guesses)
        {
            totalGuesses += guesses;
            TotalGames++;
        }

        public double Average()
        {
            return (double)totalGuesses / TotalGames;
        }


        public override bool Equals(Object player)
        {
            return PlayerName.Equals(((Player)player).PlayerName);
        }


        public override int GetHashCode()
        {
            return PlayerName.GetHashCode();
        }
    }
}
