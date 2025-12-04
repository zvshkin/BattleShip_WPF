using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    /// <summary>
    /// Класс для генерации случайного флота кораблей
    /// </summary>
    public static class FleetGenerator
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Классический флот: 1 корабль на 4 палубы, 2 на 3, 3 на 2, 4 на 1
        /// </summary>
        public static readonly Dictionary<int, int> ClassicFleet = new Dictionary<int, int>
        {
            { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 }
        };

        /// <summary>
        /// Быстрый флот: 1 корабль на 3 палубы, 2 на 2, 3 на 1
        /// </summary>
        public static readonly Dictionary<int, int> FastFleet = new Dictionary<int, int>
        {
            { 3, 1 }, { 2, 2 }, { 1, 3 }
        };

        /// <summary>
        /// Генерирует случайный флот кораблей
        /// </summary>
        /// <param name="boardSize">Размер игровой доски</param>
        /// <param name="fleetDefinition">Определение флота (размер корабля -> количество)</param>
        /// <returns>Список сгенерированных кораблей</returns>
        /// <exception cref="Exception">Выбрасывается, если не удалось разместить все корабли</exception>
        public static List<Ship> GenerateRandomFleet(int boardSize, Dictionary<int, int> fleetDefinition)
        {
            var board = new GameBoard(boardSize);
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
