using TicTacToeClassLibrary;

namespace TicTacToeConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            string saveGameKey = "S", savedGameFilePath = Directory.GetCurrentDirectory() + "\\game.json";
            try
            {
                JsonFileConvertorHelper jsonFileConvertorHelper = new JsonFileConvertorHelper(savedGameFilePath);
                Game? game = jsonFileConvertorHelper.DeserializeObjectFromFile<Game>();
                if (game == null)
                {
                    PrintWarning("No saved game was found!");
                    game = new Game();
                }
                PrintHeader(game.Players);
                string cellValue;
                bool isOk, check = true;
                int currentPlayerId;
                PrintField(game.Field.GetCellsValues());
                Player? winnerPlayer = null;
                while (!game.IsTheEnd())
                {
                    currentPlayerId = check ? game.Players[0].Id : game.Players[1].Id;
                    Console.WriteLine($"Player {currentPlayerId}'s turn. Select from 1 to 9 from the game board.");
                    do
                    {
                        cellValue = Console.ReadLine() ?? "";
                        if ((!(isOk = int.TryParse(cellValue, out int _))) && cellValue != saveGameKey)
                        {
                            PrintWarning("Please enter a valid number between 1 and 9.");
                        }
                        else if (cellValue != saveGameKey && (int.Parse(cellValue) <= 9 && int.Parse(cellValue) >= 1))
                        {
                            if (!(isOk = game.Field.GetCellsValues().Contains(cellValue)))
                            {
                                PrintWarning($"Cell \"{cellValue}\" is already set.");
                            }
                        }
                        else if (cellValue != saveGameKey && (int.Parse(cellValue) > 9 || int.Parse(cellValue) < 1))
                        {
                            if (!(isOk = game.Field.GetCellsValues().Contains(cellValue)))
                                PrintWarning($"There's no cell \"{cellValue}\" on the field. Try to enter again.");
                        }
                    } while (!isOk && cellValue != saveGameKey);
                    if (cellValue == saveGameKey)
                    {
                        jsonFileConvertorHelper.SerializeObjectToFile(game);
                        Console.Clear();
                        PrintHeader(game.Players);
                        PrintField(game.Field.GetCellsValues());
                    }
                    else
                    {
                        string sign = game.Players.Where(player => player.Id == currentPlayerId).First().Sign;
                        game.Field.ReplaceCellValueByCurrentValue(cellValue, sign);
                        Console.Clear();
                        PrintHeader(game.Players);
                        PrintField(game.Field.GetCellsValues());
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
                jsonFileConvertorHelper.ClearFileContent();
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