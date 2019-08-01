using System.Collections.Generic;
using System.Linq;
using Battleships.Core.Ship;

namespace Battleships.Core
{
    public class Grid : IGrid
    {
        private readonly IRandomPlacementSelector _randomPlacementSelector;
        public Square[] Squares { get; }
        public List<ShipBase> Ships { get; } = new List<ShipBase>();

        public Grid(IRandomPlacementSelector randomPlacementSelector)
        {
            _randomPlacementSelector = randomPlacementSelector;
            Squares = InitialiseSquares();
        }

        private Square[] InitialiseSquares()
        {
            var rows = Enumerable.Range(1, 10);
            var columns = "ABCDEFGHIJ".ToArray();

            return (
                from rowNumber in rows
                from column in columns
                select new Square(rowNumber, column)
            ).ToArray();
        }
        
        public void PlaceShipAtRandom(ShipBase ship)
        {
            var placement = _randomPlacementSelector.Select(Squares, ship.Size);
            ship.Occupy(placement);
            
            Ships.Add(ship);
        }

        public ShotState ShotFired(char column, int row)
        {
            var square = Squares.SingleOrDefault(s => s.ColumnId == column 
                                                      && s.RowNumber == row);
            return square?.Shoot() ?? ShotState.Miss;
        }
    }
}