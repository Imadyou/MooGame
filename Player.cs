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
        public int TotalGuesses { get; set; }

        public Player()
        {
       
        }

        public double GetAverage()
        {
            return (double)TotalGuesses / TotalGames;
        }

        public void UpdatePlayersRecord(int guesses) 
        {   
            TotalGames++;    
        }

        public override string ToString()
        {
            return  string.Format("{0,-9}{1,5:D}{2,9:F2}", PlayerName, TotalGames, GetAverage());
        }

   }
}
