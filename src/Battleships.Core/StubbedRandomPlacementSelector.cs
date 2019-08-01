using System.Collections.Generic;
using System.Linq;

namespace Battleships.Core
{
    public class StubbedRandomPlacementSelector : IRandomPlacementSelector
    {
        // I didn't have time to write proper logic to place the ships randomly. This always places the ships
        // in the same place.
        private int _count = 0;
        
        public SquarePlacement Select(IEnumerable<Square> allSquares, int size)
        {
            var c = ++_count;
            var squares = allSquares
                .Where(s => s.RowNumber == c)
                .Take(size)
                .ToArray();
            
            return new SquarePlacement(squares);
        }
    }
}