using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MooGame
{
    public class DataAccess
    {

        private List<Player> players=new List<Player>();
        //public Player player = new Player();
        
        public DataAccess()
        {
            
        }

        //public string GetDataInfoAsString()
        //{
        //    StreamReader dataFile = new StreamReader("result.txt");
        //    if(dataFile!=null) return (dataFile.ReadToEnd());
        //    dataFile.Close();
        //    return "Data file is empty!"; 
        //}
        //public void PostDataInfo(string dataInfo) 
        //{
        //    StreamWriter output = new StreamWriter("result.txt", append: true);
        //    output.WriteLine(playerName + "#&#" + totalGuesses);
        //    output.Close();
        //}
        public void WriteToJson(List<Player> plyers)
        {            
                string JsonString = JsonSerializer.Serialize(plyers);
                File.WriteAllText("Data.json", JsonString);
        }

        public List<Player> ReadFromJson()
        {           
                return players = JsonSerializer.Deserialize<List<Player>>(File.ReadAllText("Data.json"));         
        }
    }


}
