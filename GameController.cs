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
        public static Player player = new Player();
        private static string correctNumber = "";
        public GameController(IUI ui, IDataAccess dataAccess)
        {
            _ui = ui;
            _dataAccess = dataAccess;
        }

        public void Run()
        {
            string answer="";
            _ui.PutString("Enter your user name:\n");
            string input= _ui.GetInputString(); 
            player.PlayerName = ValidateInput(input);

            while (answer != "n")
            {
                 correctNumber = GenerateRandomNumber();
                _ui.PutString("New game:\n");
                /// <summary>
                /// Should be removed or commented out to play real games!
                /// </summary>
                _ui.PutString(ShowTheCorrectNumber(correctNumber));
                string playerGuess = GetFourDigitString();
                string guessToCheck = HandlePlayerGuess(correctNumber, playerGuess);
                _ui.PutString(guessToCheck + "\n");
                ValidatePlayersGuess(guessToCheck);
                player.UpdatePlayersRecord(player.TotalGuesses);
                SavePlayersInfo(player);
                ShowTopPlayersList();
                answer = AskPlayerToContinue();
            } 
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
        public string GenerateRandomNumber()
        {
            Random randomNumberGenerator = new Random();
            string correctNumber = "";
            for (int i = 0; i < 4; i++)
            {
                string randomDigit = "";
                while (correctNumber.Contains(randomDigit))
                {
                    int randomNumber = randomNumberGenerator.Next(10);
                    randomDigit = "" + randomNumber;
                }
                correctNumber = correctNumber + randomDigit;
            }
            return correctNumber;
        }

        /// <summary>
        /// Compars the correct number and the player guess and returns string ro controll it. 
        /// </summary>
        /// <param name="correctNumber"></param>
        /// <param name="playerGuess"></param>
        /// <returns name="guessToCheck">returns string </returns>
        public string HandlePlayerGuess(string correctNumber, string playerGuess)
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

        /// <summary>
        /// while the players guess is not correct keep playing
        /// </summary>
        /// <param name="guessToCheck"></param>
        /// <returns></returns>
        public string ValidatePlayersGuess(string guessToCheck)
        {
            while (guessToCheck != "BBBB,")
            {
                player.TotalGuesses++;
                string playerGuess = ValidateInput(_ui.GetInputString());
                _ui.PutString("your guess: "+playerGuess + "\n");
                guessToCheck = HandlePlayerGuess(correctNumber, playerGuess);
            }
            return guessToCheck;
        }

        public void ShowTopPlayersList()
        {
        List<Player> players = _dataAccess.GetPlayersList();

                _ui.PutString("Player   games   average");
                foreach (Player player in players)
                {
                    _ui.PutString(player.ToString());
                } 
        }

        public void SavePlayersInfo(Player playerToSave)
        {          
            List<Player> playersInfo = _dataAccess.GetPlayersList();
            foreach (var playerInfo in playersInfo)
            {
                if (playerInfo.PlayerName == playerToSave.PlayerName)
                {
                    playerInfo.TotalGames += playerToSave.TotalGames;
                    playerInfo.TotalGuesses += playerToSave.TotalGuesses;
                    _dataAccess.UpdatePlayersList(playersInfo);
                }
                else
                {
                    playersInfo.Add(playerToSave);
                    _dataAccess.PostPlayersList(playersInfo);
                }
            }
        }
   
        public string AskPlayerToContinue()
        {
            _ui.PutString($"Correct, it took {player.TotalGuesses} guesses\nContinue? n/y: ");

            string input = _ui.GetInputString();
            while (true)
            {
                if (input.Length == 1 && (input == "n" || input == "y"))
                {
                    return input;
                }

                _ui.PutString("Invalid input, enter one character n or y! ");
                input = _ui.GetInputString();
            }
        }

        public string GetFourDigitString()
        {
            do
            {
                _ui.PutString("Please enter a number with four digits: ");
                string input = _ui.GetInputString();

                if (input.Length == 4 && input.All(char.IsDigit))
                {
                    return input;
                }
                else
                {
                    _ui.PutString("Invalid input. Please try again.");
                }
            } while (true);
        }

        public string ValidateInput(string input)
        { 
            bool isValid = IsValidString(input);
            while (isValid)
            {
                _ui.PutString("Invalid Input. Input kan not be empty!");
                input = _ui.GetInputString();
                isValid = IsValidString(input);
            }
                return input;
        }

        public bool IsValidString(string input)
        {
            return string.IsNullOrEmpty(input);
        }
    }
}