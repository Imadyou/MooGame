using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace MooGame
{
    public class DataAccess : IDataAccess
    {

        private List<Player> players = new List<Player>();

       
        public DataAccess()
        {
         
        }
        /// <summary>
        /// Posts the players list in the Json file
        /// </summary>
        /// <param name="plyers"></param>
        public void PostPlayersList(List<Player> players)
        {
            string JsonString = JsonConvert.SerializeObject(players);
            File.WriteAllText("Data.json", JsonString);
        }
        /// <summary>
        /// Gets the players list from Json file.
        /// </summary>
        /// <returns></returns>
        public List<Player> GetPlayersList()
        {
            List<Player> playersList = new List<Player>();
            try
            {

                if (playersList != null || playersList?.Count != 0)
                {
                   playersList = JsonConvert.DeserializeObject<List<Player>>(ReadAllFromJson());
                }
                return playersList;
            }
            catch (Exception)
            {

                throw new Exception("We could not find the players information!");
            }
       
        }
        /// <summary>
        /// Saves the Updated player list to the Json file.
        /// </summary>
        /// <param name="playersToUpdate"></param>
        public void UpdatePlayersList(List<Player> playersToUpdate) 
        {
            // Save the updated list of players to the JSON file
            string updatedJsonString = JsonConvert.SerializeObject(playersToUpdate);
            File.WriteAllText("Data.json", updatedJsonString);
        }
        public string ReadAllFromJson()
        {
          return  File.ReadAllText("Data.json");
        }   

    }


}
