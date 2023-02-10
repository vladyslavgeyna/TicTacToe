namespace TicTacToeClassLibrary
{
    public class Cell
    {
        public string Value { get; set; } = "";
        public Cell (string value)
        {
            Value = value;
        }
        public Cell(Cell cell)
        {
            Value = cell.Value;
        }
        public Cell() { }
    }
}
