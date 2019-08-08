using System.Linq;
using Battleships.Core.ShipPlacement;
using NSubstitute;
using Xunit;

namespace Battleships.Core.Test.GridTests
{
    public class WhenShotFired
    {
        private readonly Grid _grid;

        public WhenShotFired()
        {
            var randomPlacementSelector = Substitute.For<IRandomPlacementSelector>();
            _grid = new Grid(randomPlacementSelector);
            
            var squaresToPlaceBattleship = _grid.Squares.Take(TestShip.TestSize);
            var squarePlacement = new SquarePlacement(squaresToPlaceBattleship.ToArray());
            
            randomPlacementSelector.Select(_grid.Squares, TestShip.TestSize)
                .Returns(squarePlacement);
            
            _grid.PlaceShipAtRandom(new TestShip());
        }

        [Fact]
        public void AndSquareIsNotFound_ThenMissIsReturned()
        {
            var result = _grid.ShotFired('X', 100);
            
            Assert.Equal(ShotState.Miss, result);
        }

        [Fact]
        public void AndSquareIsNotOccupied_ThenMissIsReturned()
        {
            var result = _grid.ShotFired('A', 2);
            
            Assert.Equal(ShotState.Miss, result);
        }
        
        [Fact]
        public void AndSquareIsNotOccupied_ThenSquareStateIsMiss()
        {
            _grid.ShotFired('A', 2);

            var square = _grid.Squares.Single(s => s.Coordinates.Equals(new Coordinates('A', 2)));
            Assert.Equal(ShotState.Miss, square.State);
        }
        
        [Fact]
        public void AndSquareIsOccupied_ThenSquareStateIsHit()
        {
            _grid.ShotFired('A', 1);

            var square = _grid.Squares.Single(s => s.Coordinates.Equals(new Coordinates('A',1)));
            Assert.Equal(ShotState.Hit, square.State);
        }
    }
}