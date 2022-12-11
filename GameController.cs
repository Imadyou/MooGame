using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Numerics;
using static System.Net.WebRequestMethods;

namespace MooGame
{
    public class GameController
    {
        private IDataAccess _dataAccess;
        private IUI _ui;
        static private string correctNumber = "";
        public Message message = new Message();

        static public Player player = new Player();
        public GameController(IUI ui, IDataAccess dataAccess)
        {
            _ui = ui;
            _dataAccess = dataAccess;
        }

        public void Run()
        {
            string answer;
            _ui.PutString("Enter your user name:\n");
            player.PlayerName = GetValidString(_ui.GetInputString());
            player= CheckIfExsist(player);

            do
            {
                correctNumber = GenerateRandomNumber();
                _ui.PutString("New game:\n");
                /// <summary>
                /// Should be removed or commented out to play real games!
                /// </summary>
                _ui.PutString(ShowTheCorrectNumber(correctNumber));
                string playerGuess = GetValidString(_ui.GetInputString());
                player.TotalGuesses=1;
                string checkedGuess = CheckPlayerGuess(correctNumber, playerGuess);
                _ui.PutString(checkedGuess + "\n");
                
                while (checkedGuess != "BBBB,")
                {
                    player.TotalGuesses++;
                    playerGuess = _ui.GetInputString();
                    _ui.PutString(playerGuess + "\n");
                    checkedGuess = CheckPlayerGuess(correctNumber, playerGuess);
                    _ui.PutString(checkedGuess + " guess again!\n");
                    //lägg till guess again sträng kanske.
                }
                player.UpdatePlayersRecord(player.TotalGuesses);
                SavePlayerInfo(player);
                ShowTopPlayersList();
                _ui.PutString("Correct, it took " + player.TotalGuesses + " guesses\nContinue? n/y: ");
                answer= GetValidString(_ui.GetInputString());
            } while (answer!="n");
        }    

        /// <summary>
        /// Should be removed or commented out to play real games!
        /// </summary>
        /// <param name="correctNumber"></param>
        public string ShowTheCorrectNumber(string? correctNumber)
        {
            return "For practice, number is: " + correctNumber + "\n";
        }

        /// <summary>
        /// Generts the correct number the plyer should guess.
        /// </summary>
        /// <returns></returns>
        static string GenerateRandomNumber()
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
        /// <returns name="result"></returns>
        static string CheckPlayerGuess(string correctNumber, string playerGuess)
        {
            int cows = 0, bulls = 0;

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

        static string ComparePlayerGuess(int correctNumber, string playerGuess)
        {

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
        public Player CheckIfExsist(Player player)
        {
            List<Player> playersData = _dataAccess.GetplayersList();
            var playerToPlay= playersData.FirstOrDefault(p=>p.PlayerName== player.PlayerName); 
            if(playerToPlay==null) { return player; }
            return playerToPlay;
            
        }
        void SavePlayerInfo(Player player)
        {
            List<Player> playersData = _dataAccess.GetplayersList();    
             playersData.Add(player);           
            _dataAccess.PostPlayersList(playersData);
        }

        public bool IsValid(string input)
        {
            return string.IsNullOrEmpty(input) ? true : false;
        }

        public string GetValidString(string input)
        {
            try
            {
            
            bool success = IsValid(input);
            while (success)
            {
               _ui.PutString("Invalid Input. Input kan not be empty...");
                    input = _ui.GetInputString();
                    success= IsValid(input);   
            }
                return input;
            }
            catch (ArgumentException )
            {

               return "Invalid Input.";
            }
        }
        public int GetValidInt(string inputAsString)
        {
            int number;
            while (!int.TryParse(inputAsString, out number))
            {
                _ui.PutString("This is not a number! please input valid number");
                inputAsString = GetValidString(_ui.GetInputString());
            }
            return number;
        }
    }
}