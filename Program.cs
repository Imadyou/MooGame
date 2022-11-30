using System;
using System.IO;
using System.Collections.Generic;
namespace MooGame 
{
    class MainClass
    {

        public static void Main(string[] args)
        {

            bool GameISRunning = true;
            Console.WriteLine("Enter your user name:\n");
            string playerName = Console.ReadLine(); //.ToLower();

            while (GameISRunning)
            {
                string correctNumber = GenerateNumber();


                Console.WriteLine("New game:\n");
                //comment out or remove next line to play real games!
                Console.WriteLine("For practice, number is: " + correctNumber + "\n");
                string playerGuess = Console.ReadLine();

                int TotalGuesses = 1;
                string checkedResult = CheckTheGuess(correctNumber, playerGuess);
                Console.WriteLine(checkedResult + "\n");
                while (checkedResult != "BBBB,")
                {
                    TotalGuesses++;
                     playerGuess = Console.ReadLine();// ska kontroleras om får ha null eller boksav iställer?
                    Console.WriteLine(playerGuess + "\n");// kanske behövs inte.
                    checkedResult = CheckTheGuess(correctNumber, playerGuess);
                    Console.WriteLine(checkedResult + "\n"); 
                    //lägg till guess again sträng kanske.
                }
                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(playerName + "#&#" + TotalGuesses);
                output.Close();
                ShowTopPlayersList();
                Console.WriteLine("Correct, it took " + TotalGuesses + " guesses\nContinue?");
                string answer = Console.ReadLine();//ToLower()
                if (answer != null && answer != "" && answer.Substring(0, 1) == "n")// behöver n eller y också just nu den kör även om man klicker bara på enter.
                {
                    GameISRunning = false;
                }
            }
        }
        static string GenerateNumber()
        {
            Random randomNumberGenerator = new Random();
            string correctNumber = "";
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = randomNumberGenerator.Next(10);
                string randomDigit = "" + randomNumber;
                while (correctNumber.Contains(randomDigit))
                {
                    randomNumber = randomNumberGenerator.Next(10);
                    randomDigit = "" + randomNumber;
                }
                correctNumber = correctNumber + randomDigit;
            }
            return correctNumber;
        }

        static string CheckTheGuess(string correctNumber, string plyerGuess)
        {
            int cows = 0, bulls = 0;
            plyerGuess+= "    ";     // if player entered less than 4 chars, ToDo: do this in the input method of player guesses

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (correctNumber[i] == plyerGuess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
        }


        static void ShowTopPlayersList()
        {
            StreamReader dataFile = new StreamReader("result.txt");
            List<Player> players = new List<Player>();
            string dataLine;
            while ((dataLine = dataFile.ReadLine()) != null)
            {
                string[] namesAndScores = dataLine.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string playerName = namesAndScores[0];
                int totalGuesses = Convert.ToInt32(namesAndScores[1]);
                Player player = new Player(playerName, totalGuesses);
                int index = players.IndexOf(player);
                if (index < 0)
                {
                    players.Add(player);
                }
                else
                {
                    players[index].Update(totalGuesses);
                }
            }

            players.Sort((player1, player2) => player1.Average().CompareTo(player2.Average()));
            Console.WriteLine("Player   games average");
            foreach (Player player in players)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.PlayerName, player.TotalGames, player.Average()));
            }
            dataFile.Close();
        }
    }

    class Player
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

