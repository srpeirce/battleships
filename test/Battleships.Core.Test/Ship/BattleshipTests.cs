using Battleships.Core.Ship;
using Xunit;

namespace Battleships.Core.Test.Ship
{
    public class BattleshipTests
    {
        [Fact]
        public void WhenCreated_ThenHasSize5()
        {
            var battleship = new Battleship();
            Assert.Equal(5, battleship.Size);
        }
    }
}