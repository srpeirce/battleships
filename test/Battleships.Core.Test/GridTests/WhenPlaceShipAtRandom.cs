using System.Linq;
using Battleships.Core.Ship;
using Battleships.Core.ShipPlacement;
using NSubstitute;
using Xunit;

namespace Battleships.Core.Test.GridTests
{
    public class WhenPlaceShipAtRandom
    {
        private readonly Grid _grid;
        private readonly IRandomPlacementSelector _randomPlacementSelector;
        private readonly SquarePlacement _squarePlacement;

        public WhenPlaceShipAtRandom()
        {
            _randomPlacementSelector = Substitute.For<IRandomPlacementSelector>();
            _grid = new Grid(_randomPlacementSelector);
            
            var squaresToPlaceBattleship = _grid.Squares.Take(TestShip.TestSize);
            _squarePlacement = new SquarePlacement(squaresToPlaceBattleship.ToArray());
            
            _randomPlacementSelector.Select(_grid.Squares, TestShip.TestSize)
                .Returns(_squarePlacement);
        }

        [Fact]
        public void ThenShipIsAddedToGrid()
        {
            var ship = new TestShip();
            _grid.PlaceShipAtRandom(ship);

            Assert.Contains(ship, _grid.Ships);
        }
        
        [Fact]
        public void ThenARandomPlacementIsSelected()
        {
            var ship = new TestShip();
            _grid.PlaceShipAtRandom(ship);

            _randomPlacementSelector.Received(1).Select(_grid.Squares, ship.Size);
        }

        [Fact]
        public void ThenTheShipOccupiesTheSquares()
        {
            var ship = new TestShip();
            _grid.PlaceShipAtRandom(ship);

            Assert.Same(_squarePlacement, ship.SquarePlacement);
        }

        [Fact]
        public void ThenTheSquaresWithoutShipsAreNotOccupied()
        {
            _grid.PlaceShipAtRandom(new TestShip());

            var notOccupiedSquares = _grid.Squares.Except(_squarePlacement.Squares);
            Assert.All(notOccupiedSquares, s => Assert.False(s.IsOccupied));
        }
        
        [Fact]
        public void ThenTheSquaresWithShipsAreOccupied()
        {
            _grid.PlaceShipAtRandom(new TestShip());

            Assert.All(_squarePlacement.Squares, s => Assert.True(s.IsOccupied));
        }
    }

    public class TestShip : ShipBase
    {
        public static int TestSize = 5;
        public TestShip() : base(TestSize)
        {
        }
    }
}