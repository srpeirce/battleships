using System.Collections.Generic;
using System.Linq;
using Battleships.Core.ShipPlacement;
using NSubstitute;
using Xunit;

namespace Battleships.Core.Test.GridTests
{
    public class WhenNewGrid
    {
        private readonly Grid _grid;
        private readonly IEnumerable<IGrouping<int, Square>> _rows;
        private readonly IEnumerable<IGrouping<char, Square>> _columns;

        public WhenNewGrid()
        {
            var randomPlacementSelector = Substitute.For<IRandomPlacementSelector>();
            _grid = new Grid(randomPlacementSelector);
            
            var squaresToPlaceBattleship = _grid.Squares.Take(5);
            randomPlacementSelector.Select(_grid.Squares, 5)
                .Returns(new SquarePlacement(squaresToPlaceBattleship.ToArray()));
            
            _rows = _grid.Squares.GroupBy(s => s.Coordinates.RowNumber);
            _columns = _grid.Squares.GroupBy(s => s.Coordinates.ColumnId);
        }
        
        [Fact]
        public void ThenHas100Squares()
        {
            Assert.Equal(100, _grid.Squares.Length);
        }
        
        [Fact]
        public void ThenHas10Rows()
        {
            Assert.Equal(10, _rows.Count());
        }
        
        [Fact]
        public void ThenHasRowsAreLabelled1To10()
        {
            var rowNumbers = _rows.Select(r => r.Key);
            var expectedRowNumbers = Enumerable.Range(1, 10);
            Assert.True(expectedRowNumbers.All(r => rowNumbers.Contains(r)));
        }
        
        [Fact]
        public void WhenNewGrid_ThenEachRowHasSquaresInColumnsAtoJ()
        {
            var expectedColumnIds = "ABCDEFGHIJ".ToArray();
            Assert.All(_rows, r => 
                Assert.True(expectedColumnIds.All(columnId => r.Select(s => s.Coordinates.ColumnId).Contains(columnId))));
        }
        
        [Fact]
        public void ThenEachRowHas10Squares()
        {
            Assert.All(_rows, r => Assert.Equal(10, r.Count()));
        }
        
        [Fact]
        public void ThenHas10Columns()
        {
            Assert.Equal(10, _columns.Count());
        }
        
        [Fact]
        public void ThenHasColumnsAreLabelledAToJ()
        {
            var columnIds = _columns.Select(r => r.Key);
            var expectedColumnIds = "ABCDEFGHIJ".ToArray();
            Assert.True(expectedColumnIds.All(r => columnIds.Contains(r)));
        }
        
        [Fact]
        public void ThenEachColumnHas10Squares()
        {
            Assert.All(_columns, r => Assert.Equal(10, r.Count()));
        }
        
        [Fact]
        public void ThenEachColumnHasSquaresInRows1To10()
        {
            var expectedRowIds = Enumerable.Range(1, 10);
            Assert.All(_columns, c => 
                Assert.True(expectedRowIds.All(rowId => c.Select(s => s.Coordinates.RowNumber).Contains(rowId))));
        }

        [Fact]
        public void ThenAllSquaresAreUnoccupied()
        {
            Assert.All(_grid.Squares, s => Assert.False(s.IsOccupied));
        }
        
        [Fact]
        public void ThenAllSquaresAreInNotShotState()
        {
            Assert.All(_grid.Squares, s => Assert.Equal(ShotState.NotShot, s.State));
        }
    }
}