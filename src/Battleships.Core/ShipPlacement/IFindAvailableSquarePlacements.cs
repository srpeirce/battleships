namespace Battleships.Core.ShipPlacement
{
    public interface IFindAvailableSquarePlacements
    {
        SquarePlacement[] For(Square[] gridSquares, int size);
    }
}