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
        private int totalGuesses;


        public Player(string playerName, int totalGuesses)
        {
            this.PlayerName = playerName;
            TotalGames = 1;
            this.totalGuesses = totalGuesses;
        }

        public void UpdatePlayerGuesses(int guesses)
        {
            totalGuesses += guesses;
            TotalGames++;
        }

        public double GetAverage()
        {
            return (double)totalGuesses / TotalGames;
        }

        //ToDo handle the exception.
        public override bool Equals(Object? player)
        {
            try
            {
                if (player == null)
                {
                    return false;
                }
                return PlayerName.Equals(((Player)player).PlayerName);
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        public override int GetHashCode()
        {
            return PlayerName.GetHashCode();
        }
    }
}
