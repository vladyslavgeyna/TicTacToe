using System.Text.Json;
using Newtonsoft.Json;
using TicTacToeClassLibrary;

namespace TicTacToeConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game? game = JsonConvert.DeserializeObject<Game>(File.ReadAllText("game.json"));
                if (game == null)
                {
                    PrintWarning("No saved game was found!");
                    game = new Game();
                }
                string[] fieldArgs = new string[9];
                for (int i = 0; i < fieldArgs.Length; i++)
                    fieldArgs[i] = game.Field.ConvertCellsToArray()[i].Value;
                PrintHeader(game.Players);
                string cellValue;
                bool isOk, check = true;
                int currentPlayerId;
                PrintField(fieldArgs);
                Player? winnerPlayer = null;
                while (!game.IsTheEnd())
                {
                    currentPlayerId = check ? 1 : 2;
                    Console.WriteLine($"Player {currentPlayerId}'s turn. Select from 1 to 9 from the game board.");
                    do
                    {
                        cellValue = Console.ReadLine() ?? "";
                        if (!(isOk = fieldArgs.Contains(cellValue)) && cellValue != "S")
                            PrintWarning($"There's no cell {cellValue} on the field. Try to enter again.");
                    } while (!isOk && cellValue != "S");
                    if (cellValue == "S")
                    {
                        string jsonString = JsonConvert.SerializeObject(game);
                        File.WriteAllText("game.json", jsonString);
                    }
                    else
                    {
                        string sign = game.Players.Where(player => player.Id == currentPlayerId).First().Sign;
                        fieldArgs[fieldArgs.ToList().IndexOf(cellValue)] = sign;
                        game.Field.ReplaceCellValueByCurrentValue(cellValue, sign);
                        Console.Clear();
                        PrintHeader(game.Players);
                        PrintField(fieldArgs);
                        check = !check;
                        winnerPlayer = game.GetWinnerOrDefault();
                        if (winnerPlayer != null)
                            break;
                    }
                }
                if (winnerPlayer != null)
                    Console.WriteLine($"Player {winnerPlayer.Id} wins");
                else
                    Console.WriteLine("It’s a draw");
            }
            catch
            {
                Console.WriteLine("Something went wrong...(");
            }
        }

        static void PrintHeader(Player[] players)
        {
            Console.WriteLine("Let's play Tic Tac Toe!");
            foreach (var player in players)
                Console.WriteLine($"Player {player.Id}: {player.Sign}");
        }

        static void PrintField(string[] args)
        {
            Console.WriteLine(" " + args[0] + " | " + args[1] + " | " + args[2]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" " + args[3] + " | " + args[4] + " | " + args[5]);
            Console.WriteLine("---+---+---");
            Console.WriteLine(" " + args[6] + " | " + args[7] + " | " + args[8]);
        }

        static void PrintWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}