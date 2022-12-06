using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MooGame
{
   public class Player
    {
        public string PlayerName { get; set; }
        public int TotalGames { get;  set; }
        public int totalGuesses { get; set; }

        public Player()
        {
            TotalGames = 1;
        }
        public void UpdatePlayersRecord(int guesses)
        {
            totalGuesses += guesses;
            TotalGames++;
        }

        public double GetAverage()
        {
            return (double)totalGuesses / TotalGames;
        }

        
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
        public override string ToString()
        {
            return  string.Format("{0,-9}{1,5:D}{2,9:F2}", PlayerName, TotalGames, GetAverage());
        }

        //public override int gethashcode()
        //{
        //    return PlayerName.gethashcode();
        //}
    }
}
