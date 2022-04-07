using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS123
{
    class Logic
    {
        private readonly List<string> Moves;

        private int computerMoveID;

        public Logic(string[] moves)
        {
            Moves = new List<string>(moves);
        }

        public Win DefineWinner(int userMoveIndex, int computerMoveIndex)
        {
            if (userMoveIndex == computerMoveIndex) return Win.Draw;
            int rightDistance = 0, leftDistance = 0;
            for (int i = userMoveIndex; ; i++)
            {
                if (i == Moves.Count) i = 0;
                if (Moves[i] == Moves[computerMoveIndex]) break;
                rightDistance++;
            }

            for (int i = userMoveIndex; ; i--)
            {
                if (i == -1) i = Moves.Count - 1;
                if (Moves[i] == Moves[computerMoveIndex]) break;
                leftDistance++;
            }

            return leftDistance < rightDistance ? Win.User : Win.Computer;
        }

        public (int, string) Generate_Computer()
        {
            var rnd = new Random();
            computerMoveID = rnd.Next(Moves.Count);
            return (computerMoveID, Moves[computerMoveID]);
        }

        public void Show_Available_Moves()
        {
            int i = 0;
            for (; i < Moves.Count; i++)
            {
                Console.WriteLine(i + 1 + $" - {Moves[i]}");
            }
            Console.WriteLine("0 - Exit\n? - Help");
        }

        public string Get_Result_Message(Win winner)
        {
            var resultMessage = new StringBuilder();
            resultMessage.AppendLine($"Computer move: {Moves[computerMoveID]}");
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