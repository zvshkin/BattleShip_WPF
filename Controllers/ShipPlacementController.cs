using System;
using System.Collections.Generic;
using System.Linq;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF.Controllers
{
    /// <summary>
    /// Класс для управления размещением кораблей на доске
    /// </summary>
    public class ShipPlacementController
    {
        private readonly GameBoard board;
        private readonly Stack<Ship> placedShips = new Stack<Ship>();
        private Dictionary<int, int> shipsToPlace;

        /// <summary>
        /// Конструктор контроллера размещения кораблей
        /// </summary>
        /// <param name="board">Игровая доска</param>
        /// <param name="initialFleet">Начальный флот для размещения</param>
        public ShipPlacementController(GameBoard board, Dictionary<int, int> initialFleet)
        {
            this.board = board;
            this.shipsToPlace = new Dictionary<int, int>(initialFleet);
        }

        /// <summary>
        /// Получает размер текущего корабля для размещения
        /// </summary>
        public int CurrentShipSize
        {
            get
            {
                int maxSize = 0;
                foreach (var pair in shipsToPlace)
                {
                    if (pair.Value > 0 && pair.Key > maxSize)
                    {
                        maxSize = pair.Key;
                    }
                }
                return maxSize;
            }
        }

        /// <summary>
        /// Проверяет, размещены ли все корабли
        /// </summary>
        public bool AllShipsPlaced
        {
            get
            {
                foreach (var count in shipsToPlace.Values)
                {
                    if (count > 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Пытается разместить корабль на доске
        /// </summary>
        /// <param name="positions">Позиции для размещения корабля</param>
        /// <param name="shipSize">Размер корабля</param>
        /// <returns>Результат попытки размещения</returns>
        public ShipPlacementResult TryPlaceShip(List<Position> positions, int shipSize)
        {
            if (!shipsToPlace.ContainsKey(shipSize) || shipsToPlace[shipSize] <= 0)
            {
                return new ShipPlacementResult 
                { 
                    Success = false, 
                    Message = "Все корабли этого размера уже размещены." 
                };
            }

            if (!board.IsPlacementValid(positions))
            {
                return new ShipPlacementResult 
                { 
                    Success = false, 
                    Message = "Нельзя разместить здесь: выходит за границы или касается других кораблей." 
                };
            }

            try
            {
                Ship newShip = new Ship(shipSize);
                newShip.Positions.AddRange(positions);
                board.PlaceShip(newShip);

                shipsToPlace[shipSize]--;
                placedShips.Push(newShip);

                return new ShipPlacementResult { Success = true, Ship = newShip };
            }
            catch (InvalidOperationException ex)
            {
                return new ShipPlacementResult { Success = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Удаляет последний размещенный корабль
        /// </summary>
        /// <returns>Результат удаления</returns>
        public ShipRemovalResult RemoveLastShip()
        {
            if (placedShips.Count == 0)
            {
                return new ShipRemovalResult 
                { 
                    Success = false, 
                    Message = "Нет кораблей для удаления." 
                };
            }

            Ship lastShip = placedShips.Pop();
            int shipSize = lastShip.Size;

            board.RemoveShip(lastShip);
            shipsToPlace[shipSize]++;

            return new ShipRemovalResult { Success = true, Ship = lastShip };
        }

        /// <summary>
        /// Получает словарь с оставшимися кораблями для размещения
        /// </summary>
        /// <returns>Словарь с оставшимися кораблями</returns>
        public Dictionary<int, int> GetRemainingShips()
        {
            return new Dictionary<int, int>(shipsToPlace);
        }
    }

    /// <summary>
    /// Результат попытки размещения корабля
    /// </summary>
    public class ShipPlacementResult
    {
        /// <summary>
        /// Успешно ли размещение
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение о результате
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Размещенный корабль
        /// </summary>
        public Ship Ship { get; set; }
    }

    /// <summary>
    /// Результат удаления корабля
    /// </summary>
    public class ShipRemovalResult
    {
        /// <summary>
        /// Успешно ли удаление
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение о результате
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Удаленный корабль
        /// </summary>
        public Ship Ship { get; set; }
    }
}
