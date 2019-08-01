namespace Battleships.Core
{
    public class SquarePlacement
    {
        public Square[] Squares { get; }

        public SquarePlacement(Square[] squares)
        {
            Squares = squares;
        }

        public SquarePlacement()
        {
            Squares = new Square[0];
        }
    }
}