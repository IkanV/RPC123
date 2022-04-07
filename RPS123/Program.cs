using System;
using System.Linq;
using System.Security.Cryptography;

namespace RPS123
{
    class Program
    {
        static void Main(string[] argv)
        {
            if (!Logic.ValidateArgs(argv)) Environment.Exit(1);

            Logic gameLogic = new Logic(argv);
            HMACKey keyGenerator1 = new HMACKey();
            keyGenerator1.GenerateKey();

            var (computerMoveIndex, computerMoveString) = gameLogic.GenerateComputerMove();
            var hash = keyGenerator1.GenerateHMAC(computerMoveString);
            Console.WriteLine($"HMAC: {BitConverter.ToString(hash).Replace("-", "")}");

            Console.WriteLine("Available moves: ");
            gameLogic.ShowAvailableMoves();

            do
            {
                Console.Write("Enter your move: ");
                string userInput = Console.ReadLine();
                int userChoice;
                if (int.TryParse(userInput, out userChoice) && userChoice <= argv.Length)
                {
                    if (userChoice == 0)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine($"Your move: {argv[userChoice - 1]}");
                        var winer = gameLogic.DefineWinner(userChoice - 1, computerMoveIndex);
                        Console.WriteLine(gameLogic.GetGameResultMessage(winer));
                        Console.WriteLine($"HMAC key: {BitConverter.ToString(keyGenerator1.Key).Replace("-", "")}");
                        break;
                    }
                }
                else if (userInput.Length == 1 && userInput[0] == '?')
                {
                    Table.ShowTable(argv, gameLogic);
                }
                else
                {
                    Console.WriteLine("Uncorrect value! Repeat input!");
                    continue;
                }
            } while (true);
        }
    }
}
