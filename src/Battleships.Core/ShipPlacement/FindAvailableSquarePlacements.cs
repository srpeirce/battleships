using System.Linq;

namespace Battleships.Core.ShipPlacement
{
    public class FindAvailableSquarePlacements : IFindAvailableSquarePlacements
    {
        public SquarePlacement[] For(Square[] gridSquares, int size)
        {
            var rowPlacements = gridSquares
                .GroupBy(s => s.Coordinates.RowNumber)
                .SelectMany(r => GetAvailablePlacements(size, r.ToArray()));
            
            var columnPlacements = gridSquares
                .GroupBy(s => s.Coordinates.ColumnId)
                .SelectMany(r => GetAvailablePlacements(size, r.ToArray()));

            return rowPlacements.Concat(columnPlacements).ToArray();
        }

        private static SquarePlacement[] GetAvailablePlacements(int size,  Square[] rowSquares)
        {
            var availablePlacements = 
                from placementSquares in rowSquares
                    .Select((t, i) => rowSquares
                        .Skip(i)
                        .Take(size)
                        .ToArray()) 
                where placementSquares.Length == size 
                      && placementSquares.All(s => !s.IsOccupied) 
                select new SquarePlacement(placementSquares);

            return availablePlacements.ToArray();
        }
    }
}