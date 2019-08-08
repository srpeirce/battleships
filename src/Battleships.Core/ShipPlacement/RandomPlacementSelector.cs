using System;

namespace Battleships.Core.ShipPlacement
{
    public class RandomPlacementSelector : IRandomPlacementSelector
    {
        private readonly IFindAvailableSquarePlacements _findAvailableSquarePlacements;
        private readonly Random _random;

        public RandomPlacementSelector(IFindAvailableSquarePlacements findAvailableSquarePlacements, Random random)
        {
            _findAvailableSquarePlacements = findAvailableSquarePlacements;
            _random = random;
        }

        public SquarePlacement Select(Square[] allSquares, int size)
        {
            var placements = _findAvailableSquarePlacements.For(allSquares, size);
            var randomIndex = _random.Next(0, placements.Length);

            return placements[randomIndex];
        }
    }
}