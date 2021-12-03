using CyberSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSharp
{
    class Game : IGame
    {
        Player _player;
        Target _target;
        bool _gameRunning = false;
        int CurrentSuccess { get; set; }

        private const string POOR = "You are poor and don't have enough money for this!";
        private const string HELP = "Commands: find, hack, send, bribe, learn, info, win, surrender, help";

        public static Random RNG = new(DateTime.Now.GetHashCode());

        private void HandleInput(string input)
        {
            switch (input)
            {
                case "find":
                    Find();
                    break;
                case "hack":
                    Hack();
                    break;
                case "send":
                    Send();
                    break;
                case "bribe":
                    Bribe();
                    break;
                case "learn":
                    Learn();
                    break;
                case "info":
                    Console.WriteLine(_player);
                    break;
                case "win":
                    Win();
                    break;
                case "surrender":
                    Console.WriteLine("I guess you were a sucker afterall...");
                    _gameRunning = false;
                    break;
                case "help":
                    Console.WriteLine(HELP);
                    break;
                default:
                    Console.WriteLine("Wrong command... Please play by the rules!");
                    break;
            }
        }

        private void Hack()
        {           
            if (_target == null)
            {
                Console.WriteLine("Noone to hack my duude. Find someone first.");
                return;
            }

            // record old chance for later checks
            int oldChance = CurrentSuccess;

            // increment the CurrentSuccess
            CurrentSuccess += RNG.Next(_player.HackingSkill + 1);
            CurrentSuccess -= _target.Defend();

            // Fail state
            if (CurrentSuccess < 0)
            {
                _target = null;
                _player.CriminalityLevel++;
                Console.WriteLine("You suck at hacking and were found! Your criminality level is: " + _player.CriminalityLevel.ToString());
                return;
            }

            CurrentSuccess = Math.Clamp(CurrentSuccess, 0, 100);

            Console.WriteLine("Current success: " + CurrentSuccess.ToString());

            // check for boundry crossing
            if (oldChance < 30 && CurrentSuccess >= 30)
            {
                Console.WriteLine("You obtained new information about the target: " + _target.WalletAddress);
            }

            // check for boundry crossing
            if (oldChance < 60 && CurrentSuccess >= 60)
            {
                Console.WriteLine("You obtained new information about the target: " + _target.WalletPassword);
            }
        }

        private void Send()
        {
            // check if there is any target
            if (_target == null)
            {
                Console.WriteLine("Noone to hack my duude. Find someone first.");
                return;
            }

            // calculate hack value and rob the person accordingly
            int hackChance = RNG.Next(100);
            if (hackChance < CurrentSuccess)
            {
                _player.Pay(-_target.WalletBTC);
                Console.WriteLine("Sending\n" + _target.WalletAddress + "\n" + _target.WalletBTC + " BTC\n" + _player.WalletAddress);
            }
            else
            {
                _player.CriminalityLevel++;
                Console.WriteLine("You were discovered! You new criminality level: " + _player.CriminalityLevel.ToString());
            }
            _target = null;
        }

        private void Win()
        {
            if (_player.Win)
            {
                Console.WriteLine("You won! Congratulations!");
                _gameRunning = false;
            }
            else
            {
                Console.WriteLine("Not enough BTC to win. Keep hacking!");
            }
        }

        private void Find()
        {
            // Check if we can finda new target
            if (!_player.Pay(0.01))
            {
                Console.WriteLine(POOR);
                return;
            }

            // Generate a random new target
            int personTypeChance = RNG.Next(10);
            PersonType personType = PersonType.COMMON;

            if (personTypeChance < 1)
            {
                personType = PersonType.EPIC;
            }
            else if (personTypeChance < 4)
            {
                personType = PersonType.RARE;
            }
            
            // Reset game data to track the progress on the new target
            _target = new Target(personType);
            CurrentSuccess = 0;
            Console.WriteLine("You found " + _target.ToString());
        }

        private void Bribe()
        {
            // Check if you are not poor and can bribe
            if (!_player.Pay(0.05))
            {
                Console.WriteLine(POOR);
                return;
            }

            // If you have enough money you can keep trying to lower your CriminalityLevel but the value is clamped
            // Its like paying for something you get no value from
            _player.CriminalityLevel--;
            Console.WriteLine("Your criminality level decreased to: " + _player.CriminalityLevel.ToString());
        }

        private void Learn()
        {
            // Check if you are poor
            if (!_player.Pay(0.005))
            {
                Console.WriteLine(POOR);
                return;
            }

            // If you have enough money you can keeptrying to increase your HackingSkill but the value is clamped
            // Its like paying for something you get no value from
            _player.HackingSkill++;
            Console.WriteLine("Your hacking skill increased to: " + _player.HackingSkill.ToString());
        }

        private bool PlayerLost()
        {
            // After each iteration check if the player can continue playing the game
            bool result = false;
            result = result || _player.CriminalityLevel == 5;
            result = result || (_target == null && _player.WalletBTC < 0.01);
            return result;
        }

        public void Start()
        {
            // welcome message and get the name of the user
            Console.WriteLine("Welcome to the world of Hacking.");
            string name;
            do
            {
                Console.WriteLine("Please enter your name: ");
                name = Console.ReadLine();
            } while (name.Length == 0);
            // instantiate the player information
            _player = new Player(name);
            _gameRunning = true;

            Console.WriteLine(HELP);
            Console.WriteLine("Your BTC address is: " + _player.WalletAddress);
            Console.WriteLine(_player);

            // running loop
            while (_gameRunning)
            {
                // check for losing state
                if (PlayerLost())
                {
                    Console.WriteLine("You suck and thats why you lost. Maybe be careful next time");
                    Console.WriteLine("Player stats:");
                    Console.WriteLine(_player);
                    _gameRunning = false;
                    continue;
                }

                // Read user input
                Console.Write("[" + _player.Name + "] ");
                string line = Console.ReadLine();
                // Handle user input
                HandleInput(line);
            }

            // goodbye message
            Console.WriteLine("Thank you for playing :)");
        }
    }
}
