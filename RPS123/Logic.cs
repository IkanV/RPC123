using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS123
{
    class Logic
    {
        private readonly List<string> possibleMoves;

        private int computerMoveindex;

        public Logic(string[] moves)
        {
            possibleMoves = new List<string>(moves);
        }

        public Win DefineWinner(int userMoveIndex, int computerMoveIndex)
        {
            if (userMoveIndex == computerMoveIndex) return Win.Draw;
            int rightDistance = 0, leftDistance = 0;
            for (int i = userMoveIndex; ; i++)
            {
                if (i == possibleMoves.Count) i = 0;
                if (possibleMoves[i] == possibleMoves[computerMoveIndex]) break;
                rightDistance++;
            }

            for (int i = userMoveIndex; ; i--)
            {
                if (i == -1) i = possibleMoves.Count - 1;
                if (possibleMoves[i] == possibleMoves[computerMoveIndex]) break;
                leftDistance++;
            }

            return leftDistance < rightDistance ? Win.User : Win.Computer;
        }

        public (int, string) GenerateComputerMove()
        {
            var rnd = new Random();
            computerMoveindex = rnd.Next(possibleMoves.Count);
            return (computerMoveindex, possibleMoves[computerMoveindex]);
        }

        public void ShowAvailableMoves()
        {
            int i = 0;
            for (; i < possibleMoves.Count; i++)
            {
                Console.WriteLine(i + 1 + $" - {possibleMoves[i]}");
            }
            Console.WriteLine("0 - Exit\n? - Help");
        }

        public string GetGameResultMessage(Win winner)
        {
            var resultMessage = new StringBuilder();
            resultMessage.AppendLine($"Computer move: {possibleMoves[computerMoveindex]}");
            switch (winner)
            {
                case Win.User:
                    resultMessage.AppendLine("User win!");
                    break;
                case Win.Computer:
                    resultMessage.AppendLine("Computer win!");
                    break;
                case Win.Draw:
                    resultMessage.AppendLine("Draw!");
                    break;
            }
            return Convert.ToString(resultMessage);
        }

        public static bool ValidateArgs(string[] argv)
        {
            if (argv.Length < 3)
            {
                Console.WriteLine("Error! Uncorrect count of parameters! Count of parameters must be 3 or more!");
                return false;
            }
            else if (argv.Length % 2 == 0)
            {
                Console.WriteLine("Error! Uncorrect count of parameters! Add more parameters!");
                return false;
            }
            else if (argv.Distinct().Count() != argv.Length)
            {
                Console.WriteLine("Error! Moves in list shouldn't repeat");
                return false;
            }
            return true;
        }

    }
}