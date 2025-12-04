using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    /// <summary>
    /// Класс игровой доски
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// Размер доски
        /// </summary>
        public int BoardSize { get; }

        /// <summary>
        /// Сетка состояний ячеек доски
        /// </summary>
        public BoardCellState[,] Grid { get; private set; }

        /// <summary>
        /// Список кораблей на доске
        /// </summary>
        public List<Ship> Ships { get; } = new List<Ship>();

        /// <summary>
        /// Проверяет, закончилась ли игра (все корабли потоплены)
        /// </summary>
        public bool IsGameOver => Ships.All(s => s.IsSunk);

        /// <summary>
        /// Конструктор игровой доски
        /// </summary>
        /// <param name="size">Размер доски</param>
        public GameBoard(int size)
        {
            BoardSize = size;
            Grid = new BoardCellState[size, size];
        }

        /// <summary>
        /// Проверяет, можно ли разместить корабль в указанных позициях
        /// </summary>
        /// <param name="shipPositions">Список позиций для размещения корабля</param>
        /// <returns>true, если размещение допустимо, иначе false</returns>
        public bool IsPlacementValid(List<Position> shipPositions)
        {
            foreach (var pos in shipPositions)
            {
                if (pos.Row < 0 || pos.Row >= BoardSize || pos.Column < 0 || pos.Column >= BoardSize)
                    return false;

                for (int r = pos.Row - 1; r <= pos.Row + 1; r++)
                {
                    for (int c = pos.Column - 1; c <= pos.Column + 1; c++)
                    {
                        if (r >= 0 && r < BoardSize && c >= 0 && c < BoardSize)
                        {
                            if (Grid[r, c] == BoardCellState.Ship)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Размещает корабль на доске
        /// </summary>
        /// <param name="ship">Корабль для размещения</param>
        /// <exception cref="InvalidOperationException">Выбрасывается, если размещение невозможно</exception>
        public void PlaceShip(Ship ship)
        {
            if (IsPlacementValid(ship.Positions))
            {
                Ships.Add(ship);
                foreach (var pos in ship.Positions)
                {
                    Grid[pos.Row, pos.Column] = BoardCellState.Ship;
                }
            }
            else
            {
                throw new InvalidOperationException("Невозможно разместить корабль в указанных позициях.");
            }
        }

        /// <summary>
        /// Выполняет выстрел по указанной позиции
        /// </summary>
        /// <param name="target">Позиция выстрела</param>
        /// <returns>Результат выстрела (состояние ячейки после выстрела)</returns>
        public BoardCellState Shoot(Position target)
        {
            if (Grid[target.Row, target.Column] == BoardCellState.Miss ||
                Grid[target.Row, target.Column] == BoardCellState.Hit ||
                Grid[target.Row, target.Column] == BoardCellState.Sunk)
            {
                return Grid[target.Row, target.Column];
            }

            if (Grid[target.Row, target.Column] == BoardCellState.Ship)
            {
                Grid[target.Row, target.Column] = BoardCellState.Hit;

                Ship hitShip = Ships.FirstOrDefault(s => s.Positions.Contains(target));
                hitShip?.RecordHit(target);

                if (hitShip.IsSunk)
                {
                    foreach (var pos in hitShip.Positions)
                    {
                        Grid[pos.Row, pos.Column] = BoardCellState.Sunk;
                    }
                    MarkNeighboringCellsAsMiss(hitShip);
                    return BoardCellState.Sunk;
                }
                return BoardCellState.Hit;
            }
            else
            {
                Grid[target.Row, target.Column] = BoardCellState.Miss;
                return BoardCellState.Miss;
            }
        }

        /// <summary>
        /// Удаляет корабль с доски
        /// </summary>
        /// <param name="ship">Корабль для удаления</param>
        public void RemoveShip(Ship ship)
        {
            if (ship == null || !Ships.Contains(ship))
                return;

            foreach (var pos in ship.Positions)
            {
                Grid[pos.Row, pos.Column] = BoardCellState.Empty;
            }

            Ships.Remove(ship);
        }

        /// <summary>
        /// Помечает соседние ячейки потопленного корабля как промахи
        /// </summary>
        /// <param name="sunkShip">Потопленный корабль</param>
        private void MarkNeighboringCellsAsMiss(Ship sunkShip)
        {
            foreach (var pos in sunkShip.Positions)
            {
                for (int r = pos.Row - 1; r <= pos.Row + 1; r++)
                {
                    for (int c = pos.Column - 1; c <= pos.Column + 1; c++)
                    {
                        if (r >= 0 && r < BoardSize && c >= 0 && c < BoardSize)
                        {
                            if (Grid[r, c] == BoardCellState.Empty)
                            {
                                Grid[r, c] = BoardCellState.Miss;
                            }
                        }
                    }
                }
            }
        }
    }
}
