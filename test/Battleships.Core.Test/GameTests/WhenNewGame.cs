using Battleships.Core.Ship;
using NSubstitute;
using Xunit;

namespace Battleships.Core.Test.GameTests
{
    public class WhenNewGame
    {
        private readonly IGrid _grid;
        private readonly Game _game;

        public WhenNewGame()
        {
            _grid = Substitute.For<IGrid>();
            _game = new Game(_grid);
        }
        
        [Fact]
        public void ThenBattleshipIsAddedToGrid()
        {
            _grid.Received(1).PlaceShipAtRandom(Arg.Any<Battleship>());
        }
        
        [Fact]
        public void Then2DestroyersAreAddedToGrid()
        {
            _grid.Received(2).PlaceShipAtRandom(Arg.Any<Destroyer>());
        }
    }
}