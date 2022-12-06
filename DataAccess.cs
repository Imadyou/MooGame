using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MooGame
{
    public class DataAccess : IDataAccess
    {

        private List<Player> players = new List<Player>();


        public DataAccess()
        {

        }

        public void PostPlayersList(List<Player> plyers)
        {
            string JsonString = JsonSerializer.Serialize(plyers);
            File.WriteAllText("Data.json", JsonString);
        }

        public List<Player> GetplayersList()
        {
            return players = JsonSerializer.Deserialize<List<Player>>(ReadAllFromJson());
        }

        public string ReadAllFromJson()
        {
          return  File.ReadAllText("Data.json");
        }
    }


}
