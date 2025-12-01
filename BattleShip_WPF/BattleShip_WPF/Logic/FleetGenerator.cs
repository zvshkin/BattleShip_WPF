using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    public static class FleetGenerator
    {
        public static readonly Dictionary<int, int> ClassicFleet = new Dictionary<int, int>
    {
        { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 }
    };

        public static readonly Dictionary<int, int> FastFleet = new Dictionary<int, int>
    {
        { 3, 1 }, { 2, 2 }, { 1, 3 }
    };

        public static List<Ship> GenerateRandomFleet(int boardSize, Dictionary<int, int> fleetDefinition)
        {
            var board = new GameBoard(boardSize);
            var random = new Random();
            var ships = new List<Ship>();

            foreach (var pair in fleetDefinition.OrderByDescending(p => p.Key))
            {
                int shipSize = pair.Key;
                int count = pair.Value;

                for (int i = 0; i < count; i++)
                {
                    bool placed = false;
                    int attempts = 0;
                    const int maxAttempts = 1000;

                    while (!placed && attempts < maxAttempts)
                    {
                        attempts++;

                        int row = random.Next(boardSize);
                        int col = random.Next(boardSize);
                        bool isHorizontal = random.Next(2) == 0;

                        var newShip = new Ship(shipSize);
                        var positions = new List<Position>();

                        for (int j = 0; j < shipSize; j++)
                        {
                            int r = isHorizontal ? row : row + j;
                            int c = isHorizontal ? col + j : col;
                            positions.Add(new Position(r, c));
                        }

                        if (board.IsPlacementValid(positions))
                        {
                            newShip.Positions.AddRange(positions);
                            board.PlaceShip(newShip);
                            ships.Add(newShip);
                            placed = true;
                        }
                    }

                    if (!placed)
                    {
                        throw new Exception("Не удалось разместить корабли. Слишком маленький размер поля или слишком много кораблей.");
                    }
                }
            }
            return ships;
        }
    }
}
