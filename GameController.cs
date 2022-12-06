using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

namespace MooGame
{
   public class GameController
   {
        private IDataAccess _dataAccess;
        private IUI _ui;
        static private string correctNumber = "";
   
        static public  Player player = new Player();
        public GameController(IUI ui, IDataAccess dataAccess)
        {
            _ui = ui;
            _dataAccess = dataAccess;
        }

        public void Run()
        {
            bool GameISRunning = true;
            player.PlayerName = GetPlayerName();

            while (GameISRunning)
            {
                correctNumber = GenerateNumber();

                _ui.PutString("New game:\n");

                ShowTheCorrectNumber(correctNumber);
                string playerGuess = _ui.GetInputString();

                player.TotalGuesses = 1;
                string checkedGuess = CheckPlayerGuess(correctNumber, playerGuess);
                _ui.PutString(checkedGuess + "\n");
                while (checkedGuess != "BBBB,")
                {
                    player.TotalGuesses++;
                    playerGuess = _ui.GetInputString();
                    _ui.PutString(playerGuess + "\n");
                    checkedGuess = CheckPlayerGuess(correctNumber, playerGuess);
                    _ui.PutString(checkedGuess + "\n");
                    //lägg till guess again sträng kanske.
                }
                player.UpdatePlayersRecord(player.TotalGuesses);
                SavePlayerInfo(player);
                ShowTopPlayersList();
                _ui.PutString("Correct, it took " + player.TotalGuesses + " guesses\nContinue?");
                string answer = _ui.GetInputString();
                GameISRunning = ValidateAnswer(answer);
            }
        }

       public bool ValidateAnswer(string answer)
        {
            return (string.IsNullOrEmpty(answer) || answer.Substring(0, 1) != "n") ? true : false;
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
            List<Player> players = _dataAccess.GetplayersList();
            _ui.PutString("Player   games   average");
            foreach (Player player in players)
            {
                _ui.PutString(player.ToString());
            }           
        }
        void SavePlayerInfo(Player player)
        {
            List<Player> playersData = _dataAccess.GetplayersList();
            for (int i = 0; i < playersData.Count; i++)
            {
                if (playersData[i].PlayerName == player.PlayerName)
                {
                    playersData.Insert(i,player);
                }
            }
            playersData.Add(player);
            _dataAccess.PostPlayersList(playersData);
        }
    }
}