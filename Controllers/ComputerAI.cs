using System;
using System.Collections.Generic;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF.Controllers
{
    /// <summary>
    /// Класс для управления искусственным интеллектом компьютера
    /// </summary>
    public class ComputerAI
    {
        private static readonly Random aiRandom = new Random();
        private readonly Queue<Position> potentialTargets = new Queue<Position>();
        private readonly int boardSize;

        /// <summary>
        /// Конструктор ИИ компьютера
        /// </summary>
        /// <param name="boardSize">Размер игровой доски</param>
        public ComputerAI(int boardSize)
        {
            this.boardSize = boardSize;
        }

        /// <summary>
        /// Получает следующую цель для выстрела
        /// </summary>
        /// <param name="board">Игровая доска</param>
        /// <returns>Позиция для выстрела</returns>
        public Position GetNextTarget(GameBoard board)
        {
            if (potentialTargets.Count > 0)
            {
                return potentialTargets.Dequeue();
            }

            // Случайный выбор цели
            int r, c;
            do
            {
                r = aiRandom.Next(boardSize);
                c = aiRandom.Next(boardSize);
            }
            while (board.Grid[r, c] != BoardCellState.Empty && board.Grid[r, c] != BoardCellState.Ship);

            return new Position(r, c);
        }

        /// <summary>
        /// Добавляет соседние ячейки в очередь целей
        /// </summary>
        /// <param name="center">Центральная позиция</param>
        private void AddNeighborsToTargetQueue(Position center)
        {
            int[] dr = { -1, 1, 0, 0 };
            int[] dc = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int r = center.Row + dr[i];
                int c = center.Column + dc[i];

                if (r >= 0 && r < boardSize && c >= 0 && c < boardSize)
                {
                    Position newTarget = new Position(r, c);
                    if (!potentialTargets.Contains(newTarget))
                    {
                        potentialTargets.Enqueue(newTarget);
                    }
                }
            }
        }

        /// <summary>
        /// Обрабатывает результат попадания
        /// </summary>
        /// <param name="hitPosition">Позиция попадания</param>
        /// <param name="result">Результат выстрела</param>
        /// <param name="board">Игровая доска</param>
        public void ProcessHitResult(Position hitPosition, BoardCellState result, GameBoard board)
        {
            if (result == BoardCellState.Hit)
            {
                AddNeighborsToTargetQueue(hitPosition);
            }
            else if (result == BoardCellState.Sunk)
            {
                potentialTargets.Clear();
            }
        }
    }
}
