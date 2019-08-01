using System.Collections.Generic;

namespace Battleships.Core
{
    public interface IRandomPlacementSelector
    {
        SquarePlacement Select(IEnumerable<Square> allSquares, int size);
    }
}