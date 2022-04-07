using System;
using ConsoleTables;

namespace RPS123
{
    static class Table
    {
        public static void ShowTable(string[] moves, Logic gameLogic)
        {
            var ruleTable = new ConsoleTable(new string[] { "PC\\USER" });
            ruleTable.AddColumn(moves);
            for (int i = 0; i < moves.Length; i++)
            {
                var tableRow = new string[moves.Length + 1];
                tableRow[0] = moves[i];
                for (int j = 0; j < moves.Length; j++)
                {
                    switch (gameLogic.DefineWinner(j, i))
                    {
                        case Win.User:
                            tableRow[j + 1] = "Win";
                            break;
                        case Win.Computer:
                            tableRow[j + 1] = "Lose";
                            break;
                        case Win.Draw:
                            tableRow[j + 1] = "Draw";
                            break;
                    }
                }
                ruleTable.AddRow(tableRow);
            }
            ruleTable.Options.EnableCount = false;
            Console.WriteLine(ruleTable);
        }
    }
}