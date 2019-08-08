using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Core.ShipPlacement;
using NSubstitute;
using Xunit;

namespace Battleships.Core.Test.ShipPlacement.RandomPlacementSelectorTests
{
    public class WhenSelect
    {
        private readonly Square[] _gridSquares;
        private readonly IFindAvailableSquarePlacements _availablePlacements;
        private readonly RandomPlacementSelector _selector;
        private readonly Random _random;
        private readonly List<SquarePlacement> _squarePlacements;

        public WhenSelect()
        {
            _gridSquares = CreateGridSquares();
            _availablePlacements = Substitute.For<IFindAvailableSquarePlacements>();
            _squarePlacements = new List<SquarePlacement>()
            {
                new SquarePlacement(_gridSquares.Take(5).ToArray()),
                new SquarePlacement(_gridSquares.Skip(5).Take(5).ToArray()),
                new SquarePlacement(_gridSquares.Skip(10).Take(5).ToArray()),
                new SquarePlacement(_gridSquares.Skip(15).Take(5).ToArray()),
                new SquarePlacement(_gridSquares.Skip(20).Take(5).ToArray())
            };
            _availablePlacements.For(_gridSquares, 5).Returns(_squarePlacements.ToArray());
            
            // Returns 0, 2, 3, 3 with this seed
            _random = Substitute.For<Random>(1);

            _selector = new RandomPlacementSelector(_availablePlacements, _random);
        }
        
        [Fact]
        public void ThenAllPossiblePlacementsAreObtained()
        {
            _selector.Select(_gridSquares, 5);

            _availablePlacements.Received(1).For(_gridSquares, 5);
        }

        [Fact]
        public void ThenARandomIndexIsChosen()
        {
            _selector.Select(_gridSquares, 5);

            var randomIndex = _random.Received(1).Next(0, 5);
            Assert.Equal(0, randomIndex);
        }

        [Fact]
        public void ThenPlacementIsReturned()
        {
            var placement = _selector.Select(_gridSquares, 5);
            
            Assert.Same(_squarePlacements.First(), placement);
        }

        private Square[] CreateGridSquares()
        {
            // Duplicated from Grid class for testing purposes.
            // I would consider removing this duplication given more time.
            var rows = Enumerable.Range(1, 10);
            var columns = "ABCDEFGHIJ".ToArray();

            return (
                from rowNumber in rows
                from column in columns
                select new Square(rowNumber, column)
            ).ToArray();
        }
    }
}