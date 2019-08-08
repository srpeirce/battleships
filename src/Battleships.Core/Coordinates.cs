namespace Battleships.Core
{
    public struct Coordinates
    {
        public char ColumnId { get; }
        public int RowNumber { get; }

        public Coordinates(char columnId, int rowNumber)
        {
            RowNumber = rowNumber;
            ColumnId = columnId;
        }
    }
}