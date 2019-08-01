using Battleships.Core.Ship;
using Xunit;

namespace Battleships.Core.Test.Ship
{
    public class DestroyerTests
    {
        [Fact]
        public void WhenCreated_ThenHasSize4()
        {
            var battleship = new Destroyer();
            Assert.Equal(4, battleship.Size);
        }
    }
}