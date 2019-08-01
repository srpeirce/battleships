using Battleships.Core.Ship;

namespace Battleships.Core
{
    public interface IGrid
    {
        Square[] Squares { get; }
        void PlaceShipAtRandom(ShipBase ship);
        ShotState ShotFired(char column, int row);
    }
}