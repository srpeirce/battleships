namespace Battleships.Core
{
    public class Square
    {
        public Coordinates Coordinates { get; }
        public bool IsOccupied { get; private set; }
        public ShotState State { get; private set; }

        public Square(int rowNumber, char columnId)
        {
            Coordinates = new Coordinates(columnId, rowNumber);
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