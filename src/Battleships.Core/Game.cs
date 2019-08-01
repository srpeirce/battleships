using Battleships.Core.Ship;

namespace Battleships.Core
{
    public class Game
    {
        public IGrid Grid { get; }
        
        public Game(IGrid grid)
        {
            Grid = grid;
            grid.PlaceShipAtRandom(new Battleship());
            grid.PlaceShipAtRandom(new Destroyer());
            grid.PlaceShipAtRandom(new Destroyer());
        }

        public ShotState Shoot(char column, int row)
        {
            return Grid.ShotFired(column, row);
        }
    }
}