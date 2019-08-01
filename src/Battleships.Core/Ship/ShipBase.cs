using System.Linq;

namespace Battleships.Core.Ship
{
    public abstract class ShipBase
    {
        public int Size { get; }
        public SquarePlacement SquarePlacement { get; private set; } = new SquarePlacement();

        public ShipBase(int size)
        {
            Size = size;
        }

        public void Occupy(SquarePlacement squarePlacement)
        {
            squarePlacement.Squares.ToList().ForEach(s => s.Occupy());
            SquarePlacement = squarePlacement;
        }
    }
}