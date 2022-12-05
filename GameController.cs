using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

namespace MooGame
{
   public class GameController
   {    private DataAccess dataAccess = new DataAccess();
        private IUI _ui;
        static private string correctNumber = "";
        static private string? playerName;
        static private int totalGuesses;
        static private Player player = new Player(playerName,totalGuesses);
        public GameController(IUI ui)
        {
            _ui = ui;
        }

        public void Run()
        {

            bool GameISRunning = true;
            playerName = GetPlayerName();

            while (GameISRunning)
            {
                correctNumber = GenerateNumber();

                _ui.PutString("New game:\n");

                ShowTheCorrectNumber(correctNumber);
                string playerGuess = _ui.GetInputString();

                totalGuesses = 1;
                string checkedGuess = CheckPlayerGuess(correctNumber, playerGuess);
                _ui.PutString(checkedGuess + "\n");
                while (checkedGuess != "BBBB,")
                {
                    totalGuesses++;
                    playerGuess = _ui.GetInputString();
                    _ui.PutString(playerGuess + "\n");
                    checkedGuess = CheckPlayerGuess(correctNumber, playerGuess);
                    _ui.PutString(checkedGuess + "\n");
                    //lägg till guess again sträng kanske.
                }

                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(playerName + "#&#" + totalGuesses);
                output.Close();
                ShowTopPlayersList();
                _ui.PutString("Correct, it took " + totalGuesses + " guesses\nContinue?");
                string answer = _ui.GetInputString();
                GameISRunning = ValidateAnswer(answer);
            }
        }

        private static bool ValidateAnswer(string answer)
        {
            if (answer == null || answer == "" || answer.Substring(0, 1) != "n")
            {
                return true;
            }

            return false;
        }

        string GetPlayerName()
        {
            _ui.PutString("Enter your user name:\n");
            string playerName = _ui.GetInputString();
            return playerName;
        }

        /// <summary>
        /// Should be removed or commented out to play real games!
        /// </summary>
        /// <param name="correctNumber"></param>
        void ShowTheCorrectNumber(string? correctNumber)
        {
            _ui.PutString("For practice, number is: " + correctNumber + "\n");
        }

        /// <summary>
        /// Generts the correct number the plyer should guess.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if the pl
        /// </summary>
        /// <param name="correctNumber"></param>
        /// <param name="playerGuess"></param>
        /// <returns></returns>
        static string CheckPlayerGuess(string correctNumber, string playerGuess)
        {
            int cows = 0, bulls = 0;
            playerGuess += "    ";     // if player entered less than 4 chars, ToDo: do this in the input method of player guesses

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (correctNumber[i] == playerGuess[j])
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

        void ShowTopPlayersList()
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
                    players[index].UpdatePlayerGuesses(totalGuesses);
                }
            }

            players.Sort((player1, player2) => player1.GetAverage().CompareTo(player2.GetAverage()));
            _ui.PutString("Player   games   average");
            foreach (Player player in players)
            {
                _ui.PutString(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.PlayerName, player.TotalGames, player.GetAverage()));
            }
            dataFile.Close();
        }

        
    }
}