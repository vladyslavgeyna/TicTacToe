namespace TicTacToeClassLibrary
{
    public class Field
    {
        private const int _SIZE = 3;
        public Cell[,] Cells { get; set; }
        public Field()
        {
            Cells = new Cell[_SIZE, _SIZE];
            int counter = 1;
            for (int i = 0; i < _SIZE; i++)
                for (int j = 0; j < _SIZE; j++)
                    Cells[i, j] = new Cell(counter++.ToString());
        }
        public void ReplaceCellValueByCurrentValue(string currentValue, string newValue)
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].Value == currentValue)
                    {
                        Cells[i, j].Value = newValue;
                        return;
                    }
                }
            }
        }
        public Cell[] GetCellRow(int rowIndex)
        {
            Cell[] cellRow = new Cell[_SIZE];
            for (int j = 0; j < _SIZE; j++)
                cellRow[j] = new Cell(Cells[rowIndex, j]);
            return cellRow;
        }
        public Cell[] GetCellColumn(int columnIndex)
        {
            Cell[] cellColumn = new Cell[_SIZE];
            for (int i = 0; i < _SIZE; i++)
                cellColumn[i] = new Cell(Cells[i, columnIndex]);
            return cellColumn;
        }
        public Cell[] GetMainDiagonal()
        {
            Cell[] cellMainDiagonal = new Cell[_SIZE];
            for (int i = 0; i < _SIZE; i++)
                 cellMainDiagonal[i] = new Cell(Cells[i, i]);
            return cellMainDiagonal;
        }
        public Cell[] GetSecondaryDiagonal()
        {
            Cell[] cellSecondaryDiagonal = new Cell[_SIZE];
            for (int i = _SIZE - 1; i >= 0; i--)
                cellSecondaryDiagonal[i] = new Cell(Cells[i, _SIZE - 1 - i]);
            return cellSecondaryDiagonal;
        }
        public Cell[] ConvertCellsToArray()
        {
            Cell[] cellsArray = new Cell[_SIZE * _SIZE];
            int counter = 0;
            for (int i = 0; i < _SIZE; i++)
                for (int j = 0; j < _SIZE; j++)
                    cellsArray[counter++] = new Cell(Cells[i, j]);
            return cellsArray;
        }
    }
}