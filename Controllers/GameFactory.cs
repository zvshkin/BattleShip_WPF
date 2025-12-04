using System.Collections.Generic;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF.Controllers
{
    /// <summary>
    /// Класс-фабрика для создания игровых досок
    /// </summary>
    public static class GameFactory
    {
        /// <summary>
        /// Создает доску игрока
        /// </summary>
        /// <param name="isFastMode">Режим быстрой игры</param>
        /// <param name="customBoard">Пользовательская доска (если есть)</param>
        /// <returns>Созданная доска игрока</returns>
        public static GameBoard CreatePlayerBoard(bool isFastMode, GameBoard customBoard = null)
        {
            if (customBoard != null)
            {
                return customBoard;
            }

            int boardSize = isFastMode ? 8 : 10;
            Dictionary<int, int> fleet = isFastMode ? FleetGenerator.FastFleet : FleetGenerator.ClassicFleet;
            List<Ship> ships = FleetGenerator.GenerateRandomFleet(boardSize, fleet);
            GameBoard board = new GameBoard(boardSize);

            foreach (var ship in ships)
            {
                board.PlaceShip(ship);
            }

            return board;
        }

        /// <summary>
        /// Создает доску противника
        /// </summary>
        /// <param name="isFastMode">Режим быстрой игры</param>
        /// <returns>Созданная доска противника</returns>
        public static GameBoard CreateEnemyBoard(bool isFastMode)
        {
            int boardSize = isFastMode ? 8 : 10;
            Dictionary<int, int> fleet = isFastMode ? FleetGenerator.FastFleet : FleetGenerator.ClassicFleet;
            List<Ship> enemyShips = FleetGenerator.GenerateRandomFleet(boardSize, fleet);
            GameBoard enemyBoard = new GameBoard(boardSize);

            foreach (var ship in enemyShips)
            {
                enemyBoard.PlaceShip(ship);
            }

            return enemyBoard;
        }
    }
}
