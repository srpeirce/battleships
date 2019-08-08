namespace Battleships.Core.ShipPlacement
{
    public interface IRandomPlacementSelector
    {
        SquarePlacement Select(Square[] allSquares, int size);
    }
}