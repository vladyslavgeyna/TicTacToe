namespace TicTacToeClassLibrary
{
    public class Game
    {
        private const int _COUNT_OF_PLAYERS = 2;
        public Player[] Players { get; set; }
        public Field Field { get; set; }
        public Game()
        {
            string[] signs = new string[] { CellSign.X.ToString(), CellSign.O.ToString() };
            Players = new Player[_COUNT_OF_PLAYERS];
            for (int i = 0; i < _COUNT_OF_PLAYERS; i++)
                Players[i] = new Player(i + 1, signs[i]);
            Field = new Field();
        }
        public Player? GetWinnerOrDefault()
        {
            for (int i = 0; i < Field.Cells.GetLength(0); i++)
            {
                if (Field.GetCellRow(i).All(cell => cell.Value == CellSign.X.ToString()) || Field.GetCellColumn(i).All(cell => cell.Value == CellSign.X.ToString()))
                    return Players.Where(player => player.Sign == CellSign.X.ToString()).First();
                else if (Field.GetCellColumn(i).All(cell => cell.Value == CellSign.O.ToString()) || Field.GetCellColumn(i).All(cell => cell.Value == CellSign.O.ToString()))
                    return Players.Where(player => player.Sign == CellSign.O.ToString()).First();
            }
            if (Field.GetMainDiagonal().All(cell => cell.Value == CellSign.X.ToString()) || Field.GetSecondaryDiagonal().All(cell => cell.Value == CellSign.X.ToString()))
                return Players.Where(player => player.Sign == CellSign.X.ToString()).First();
            else if (Field.GetMainDiagonal().All(cell => cell.Value == CellSign.O.ToString()) || Field.GetSecondaryDiagonal().All(cell => cell.Value == CellSign.O.ToString()))
                return Players.Where(player => player.Sign == CellSign.O.ToString()).First();
            return null;
        }
        public bool IsTheEnd() => Field.ConvertCellsToArray().All(cell => cell.Value == CellSign.X.ToString() || cell.Value == CellSign.O.ToString());
    }
}
