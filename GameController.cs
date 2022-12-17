﻿using System;
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
            //checkIfExsist to get players info
            player=GetPlayerIfExsist();
            do
            {
               string  correctNumber = GenerateRandomNumber();
                _ui.PutString("New game:\n");
                /// <summary>
                /// Should be removed or commented out to play real games!
                /// </summary>
                _ui.PutString(ShowTheCorrectNumber(correctNumber));
                string playerGuess = GetValidString(_ui.GetInputString());
                string guessToCheck = CheckPlayerGuess(correctNumber, playerGuess);
                _ui.PutString(guessToCheck + "\n");
                ControllResult(guessToCheck);
                //player.UpdatePlayersRecord(player.TotalGuesses);
                SavePlayerInfo(player);
                answer = AskPlayerToContinue();
                ShowTopPlayersList();
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
        public string GenerateRandomNumber()
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
        public string CheckPlayerGuess(string correctNumber, string playerGuess)
        {
            int cows = 0, bulls = 0;
            playerGuess += "    ";
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

        public string ControllResult(string guessToCheck)
        {
            while (guessToCheck != "BBBB,")
            {
                player.TotalGuesses++;
                string playerGuess = GetValidString(_ui.GetInputString());
                _ui.PutString(playerGuess + "\n");
                guessToCheck = CheckPlayerGuess(correctNumber, playerGuess);
                _ui.PutString(guessToCheck + "\n");
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

        public void SavePlayerInfo(Player playerToSave)
        {          
            List<Player> playersInfo = _dataAccess.GetPlayersList();
            foreach (var item in playersInfo)
            {
                if (item.PlayerName == playerToSave.PlayerName)
                {
                    item.TotalGames = playerToSave.TotalGames;
                    item.TotalGuesses = playerToSave.TotalGuesses;
                    _dataAccess.UpdatePlayersList(playersInfo);
                }
                else
                {
                    playersInfo.Add(playerToSave);
                    _dataAccess.PostPlayersList(playersInfo);
                }
            }
        }
        public Player GetPlayerIfExsist()
        {
            List<Player> playersInfo = _dataAccess.GetPlayersList();
            if (playersInfo is null ||playersInfo.Count()==0)
            {
                playersInfo?.Add(player);
                _dataAccess.PostPlayersList(playersInfo);
            }
            foreach (var item in playersInfo)
            {
                if (player.PlayerName == item.PlayerName)
                {
                    player.TotalGames= item.TotalGames;
                }
            }

            return player;
        }

        public string AskPlayerToContinue()
        {
            _ui.PutString("Correct, it took " + player.TotalGuesses + " guesses\nContinue? n/y: ");
            string input = GetValidString(_ui.GetInputString());
            bool isValid = false;
            while (!isValid)
            {
                if (input.Length != 1)
                { 
                    _ui.PutString("Invalid input, enter one character n or y! ");
                    input = GetValidString(_ui.GetInputString());
                }
                if (input.Length == 1)
                {
                  
                    if (input == "n")
                    {
                        break;
                    }
                    else if (input == "y")
                    {
                        isValid= true;
                    }
                    else 
                    {
                        _ui.PutString("Invalid input, enter n or y! ");
                        input = GetValidString(_ui.GetInputString());
                    }
                }
            }

            return input;
        }
     

        public string GetValidString(string input)
        { 
            bool isValid = IsValidString(input);
            while (!isValid)
            {
                _ui.PutString("Invalid Input. Input kan not be empty!");
                input = _ui.GetInputString();
                isValid = IsValidString(input);
            }
                return input;
        }

        public bool IsValidString(string input)
        {
            return string.IsNullOrEmpty(input) ? false : true;
        }
    }
}