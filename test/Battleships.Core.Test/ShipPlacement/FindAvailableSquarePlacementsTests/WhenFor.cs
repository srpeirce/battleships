using System.Collections.Generic;
using System.Linq;
using Battleships.Core.ShipPlacement;
using Xunit;

namespace Battleships.Core.Test.ShipPlacement.FindAvailableSquarePlacementsTests
{
    public class WhenFor
    {
        private readonly FindAvailableSquarePlacements _findAvailableSquarePlacements;

        public WhenFor()
        {
            _findAvailableSquarePlacements = new FindAvailableSquarePlacements();
        }

        [Theory]
        [MemberData(nameof(RowPlacementCoordinatesForEmptyGrid))]
        public void AndGridIsEmpty_ThenReturnsAllPossibleSquarePlacementsForRows(params Coordinates[] coords)
        {
            var gridSquares = Create4x4dGridSquares();
            
            var placements = _findAvailableSquarePlacements.For(gridSquares, 2);

            var actualCoords = placements.Select(p => p.Squares.Select(s => s.Coordinates).ToArray());
            Assert.Contains(coords, actualCoords);
        }

        [Theory]
        [MemberData(nameof(ColumnPlacementCoordinatesForEmptyGrid))]
        public void AndGridIsEmpty_ThenReturnsAllPossibleSquarePlacementsForColumns(params Coordinates[] coords)
        {
            var gridSquares = Create4x4dGridSquares();

            var placements = _findAvailableSquarePlacements.For(gridSquares, 2);

            var actualCoords = placements.Select(p => p.Squares.Select(s => s.Coordinates).ToArray());
            Assert.Contains(coords, actualCoords);
        }
        
        [Fact]
        public void AndGridIsEmpty_ThenReturnsCorrectNumberOfPossiblePlacements()
        {
            var gridSquares = Create4x4dGridSquares();

            var placements = _findAvailableSquarePlacements.For(gridSquares, 2);
            
            Assert.Equal(12, placements.Length);
        }
        
        [Theory]
        [MemberData(nameof(RowPlacementCoordinatesForOccupiedGrid))]
        public void AndIsOccupied_ThenReturnsAllPossibleSquarePlacementsForRows(params Coordinates[] coords)
        {
            var gridSquares = CreateOccupiedx4dGridSquares();
            
            var placements = _findAvailableSquarePlacements.For(gridSquares, 2);

            var actualCoords = placements.Select(p => p.Squares.Select(s => s.Coordinates).ToArray());
            Assert.Contains(coords, actualCoords);
        }
        
        [Theory]
        [MemberData(nameof(ColumnPlacementCoordinatesForOccupiedGrid))]
        public void AndIsOccupied_ThenReturnsAllPossibleSquarePlacementsForColumns(params Coordinates[] coords)
        {
            var gridSquares = CreateOccupiedx4dGridSquares();
            
            var placements = _findAvailableSquarePlacements.For(gridSquares, 2);

            var actualCoords = placements.Select(p => p.Squares.Select(s => s.Coordinates).ToArray());
            Assert.Contains(coords, actualCoords);
        }
        
        [Fact]
        public void AndIsOccupied_ThenReturnsCorrectNumberOfPossiblePlacements()
        {
            var gridSquares = CreateOccupiedx4dGridSquares();

            var placements = _findAvailableSquarePlacements.For(gridSquares, 2);
            
            Assert.Equal(4, placements.Length);
        }
        
        public static IEnumerable<object[]> RowPlacementCoordinatesForOccupiedGrid()
        {
            yield return new object[]
            {
                new Coordinates('B', 1),
                new Coordinates('C', 1)
            };
            yield return new object[]
            {
                new Coordinates('A', 3),
                new Coordinates('B', 3)
            };
        }
        
        public static IEnumerable<object[]> ColumnPlacementCoordinatesForOccupiedGrid()
        {
            yield return new object[]
            {
                new Coordinates('A', 2),
                new Coordinates('A', 3)
            };
            yield return new object[]
            {
                new Coordinates('C', 1),
                new Coordinates('C', 2)
            };
        }
        
        public static IEnumerable<object[]> RowPlacementCoordinatesForEmptyGrid()
        {
            yield return new object[]
            {
                new Coordinates('A', 1),
                new Coordinates('B', 1)
            };

            yield return new object[]
            {
                new Coordinates('B', 1),
                new Coordinates('C', 1)
            };
            yield return new object[]
            {
                new Coordinates('A', 2),
                new Coordinates('B', 2)
            };

            yield return new object[]
            {
                new Coordinates('B', 2),
                new Coordinates('C', 2)
            };
            yield return new object[]
            {
                new Coordinates('A', 3),
                new Coordinates('B', 3)
            };

            yield return new object[]
            {
                new Coordinates('B', 3),
                new Coordinates('C', 3)
            };
        }
        
        public static IEnumerable<object[]> ColumnPlacementCoordinatesForEmptyGrid()
        {
            yield return new object[]
            {
                new Coordinates('A', 1),
                new Coordinates('A', 2)
            };

            yield return new object[]
            {
                new Coordinates('A', 2),
                new Coordinates('A', 3)
            };
            yield return new object[]
            {
                new Coordinates('B', 1),
                new Coordinates('B', 2)
            };

            yield return new object[]
            {
                new Coordinates('B', 2),
                new Coordinates('B', 3)
            };
            yield return new object[]
            {
                new Coordinates('C', 1),
                new Coordinates('C', 2)
            };

            yield return new object[]
            {
                new Coordinates('C', 2),
                new Coordinates('C', 3)
            };
        }
        
        /// <summary>
        /// Simplified grid for simpler testing
        /// </summary>
        /// <returns></returns>
        private Square[] Create4x4dGridSquares()
        {
            // Duplicated from Grid class for testing purposes.
            // I would consider removing this duplication given more time.
            var rows = Enumerable.Range(1, 3);
            var columns = "ABC".ToArray();

            return (
                from rowNumber in rows
                from column in columns
                select new Square(rowNumber, column)
            ).ToArray();
        }
        
        /// <summary>
        /// Simplified grid for simpler testing
        /// </summary>
        /// <returns></returns>
        private Square[] CreateOccupiedx4dGridSquares()
        {
            var gridSquares = Create4x4dGridSquares();
            
            gridSquares
                .Single(s => s.Coordinates
                    .Equals(new Coordinates('A', 1)))
                .Occupy();
            
            gridSquares
                .Single(s => s.Coordinates
                    .Equals(new Coordinates('B', 2)))
                .Occupy();
            
            gridSquares
                .Single(s => s.Coordinates
                    .Equals(new Coordinates('C', 3)))
                .Occupy();

            return gridSquares;
        }
    }

    
}