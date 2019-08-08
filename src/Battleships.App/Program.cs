using System;
using System.Linq;
using System.Text;
using Battleships.Core;
using Battleships.Core.ShipPlacement;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.App
{
    class Program
    {
        private const string Exit = "exit";
        private const string New = "new";
        
        static void Main(string[] args)
        {
            // I rushed this part of the code due to time constraints.
            Console.Title = "Battleships - By Steven Peirce";
            var sp = ConfigureDependencies();
            var game = sp.GetService<Game>();

            var gameNumber = 1;
            var shotsFired = 0;

            while (true)
            {
                Console.Clear();
                
                DrawHeading(gameNumber, shotsFired);
                game.DrawGrid();
                DrawControls();

                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input == Exit)
                {
                    break;
                }

                if (input == New)
                {
                    game = sp.GetService<Game>();
                    gameNumber++;
                    shotsFired = 0;
                    continue;
                }
                
                var column = input.Substring(0, 1).ToArray().First();
                if (!int.TryParse(input.Substring(1), out var row))
                {
                    continue;
                }

                game.Shoot(column, row);
                shotsFired++;
            }
            
        }

        private static void DrawControls()
        {
            Console.WriteLine($"===========================");
            Console.WriteLine("Type Coordinates to fire and press Enter.");
            Console.WriteLine("\t- If you miss a ship then the square turns to O.");
            Console.WriteLine("\t- If you hit a ship then the square turns to X.");
            Console.WriteLine("\t- If you sink a ship, nothing happens :-)");
            Console.WriteLine("\t- If you sink all the ships, nothing happens :-)");
            Console.WriteLine($"To exit - Type {Exit} and press Enter.");
            Console.WriteLine($"To start new game - Type {New} and press Enter.");
        }

        private static void DrawHeading(int gameNumber, int shotsFired)
        {
            Console.WriteLine($"Battleships - Game #{gameNumber} - Shots Fired: {shotsFired}");
            Console.WriteLine($"===========================");
        }

        static ServiceProvider ConfigureDependencies()
        {
            var sp = new ServiceCollection()
                .AddTransient<Game>()
                .AddTransient<IGrid, Grid>()
                .AddSingleton<IRandomPlacementSelector, RandomPlacementSelector>()
                .AddSingleton<IFindAvailableSquarePlacements, FindAvailableSquarePlacements>()
                .AddSingleton<Random>();

            return sp.BuildServiceProvider();
        }
    }

    public static class GameExtensionMethods
    {
        public static void DrawGrid(this Game game)
        {
            var sb = new StringBuilder();
            
            var rows = game.Grid.Squares.GroupBy(s => s.Coordinates.RowNumber).ToList();
            rows.ForEach(row =>
            {
                row.ToList().ForEach(square =>
                {
                    switch (square.State)
                    {
                        case ShotState.Hit: 
                            sb.Append("X");
                            break;
                        case ShotState.Miss: 
                            sb.Append("O");
                            break;
                        default:
                            sb.Append($"{square.Coordinates.ColumnId}{square.Coordinates.RowNumber}");
                            break;
                    };
                    sb.Append("\t");

                    // When C#8 comes out I think I can tidy the above into something much nicer to read :-)
//                    square.State switch
//                    {
//                        ShotState.Hit => sb.Append("X");
//                        ShotState.Miss => sb.Append("O");
//                        _ => sb.Append($"{square.ColumnId}{square.RowNumber}");
//                    };
                });
                sb.AppendLine();
            });
            
            Console.Write(sb.ToString());
        }
    }
}
