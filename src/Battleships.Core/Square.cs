namespace Battleships.Core
{
    public class Square
    {
        public int RowNumber { get; }
        public char ColumnId { get; }
        public bool IsOccupied { get; private set; }
        public ShotState State { get; private set; }

        public Square(int rowNumber, char columnId)
        {
            RowNumber = rowNumber;
            ColumnId = columnId;
            State = ShotState.NotShot;
        }

        public void Occupy()
        {
            IsOccupied = true;
        }

        public ShotState Shoot()
        {
            State = IsOccupied ? ShotState.Hit : ShotState.Miss;
            return State;
        }
    }
}