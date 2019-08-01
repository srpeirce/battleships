using NSubstitute;
using Xunit;

namespace Battleships.Core.Test.GameTests
{
    public class WhenShoot
    {
        private readonly IGrid _grid;
        private readonly Game _game;

        public WhenShoot()
        {
            _grid = Substitute.For<IGrid>();
            _game = new Game(_grid);
        }
        
        [Fact]
        public void ThenShotIsSentToGrid()
        {
            _game.Shoot('A', 1);
            _grid.Received(1).ShotFired('A', 1);
        }

        [Fact]
        public void AndMisses_ThenMissIsReturned()
        {
            _grid.ShotFired('A', 1).Returns(ShotState.Miss);
            
            var actual = _game.Shoot('A', 1);
            
            Assert.Equal(ShotState.Miss, actual);
        }
        
        [Fact]
        public void AndHits_ThenHitIsReturned()
        {
            _grid.ShotFired('A', 1).Returns(ShotState.Hit);
            
            var actual = _game.Shoot('A', 1);
            
            Assert.Equal(ShotState.Hit, actual);
        }
    }
}