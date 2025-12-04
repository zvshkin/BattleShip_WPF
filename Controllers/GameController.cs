using System;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF.Controllers
{
    /// <summary>
    /// Перечисление для определения чей ход
    /// </summary>
    public enum Turn { Player, Computer }

    /// <summary>
    /// Класс для управления игровым процессом
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// Текущий ход
        /// </summary>
        public Turn CurrentTurn { get; private set; } = Turn.Player;

        /// <summary>
        /// Доска игрока
        /// </summary>
        public GameBoard PlayerBoard { get; }

        /// <summary>
        /// Доска противника
        /// </summary>
        public GameBoard EnemyBoard { get; }

        /// <summary>
        /// Событие смены хода
        /// </summary>
        public event EventHandler<TurnChangedEventArgs> TurnChanged;

        /// <summary>
        /// Событие окончания игры
        /// </summary>
        public event EventHandler<GameOverEventArgs> GameOver;

        /// <summary>
        /// Конструктор контроллера игры
        /// </summary>
        /// <param name="playerBoard">Доска игрока</param>
        /// <param name="enemyBoard">Доска противника</param>
        public GameController(GameBoard playerBoard, GameBoard enemyBoard)
        {
            PlayerBoard = playerBoard;
            EnemyBoard = enemyBoard;
        }

        /// <summary>
        /// Обрабатывает выстрел игрока
        /// </summary>
        /// <param name="target">Позиция выстрела</param>
        /// <returns>Результат выстрела</returns>
        public BoardCellState ProcessPlayerShot(Position target)
        {
            if (CurrentTurn != Turn.Player)
            {
                return BoardCellState.Empty;
            }

            BoardCellState result = EnemyBoard.Shoot(target);

            if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
            {
                CheckGameOver();
                return result;
            }
            else
            {
                SwitchTurn();
                return result;
            }
        }

        /// <summary>
        /// Обрабатывает выстрел компьютера
        /// </summary>
        /// <param name="target">Позиция выстрела</param>
        /// <returns>Результат выстрела</returns>
        public BoardCellState ProcessComputerShot(Position target)
        {
            BoardCellState result = PlayerBoard.Shoot(target);

            if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
            {
                CheckGameOver();
            }
            else
            {
                SwitchTurn();
            }

            return result;
        }

        /// <summary>
        /// Переключает ход между игроком и компьютером
        /// </summary>
        private void SwitchTurn()
        {
            if (CurrentTurn == Turn.Player)
            {
                CurrentTurn = Turn.Computer;
            }
            else
            {
                CurrentTurn = Turn.Player;
            }

            if (TurnChanged != null)
            {
                TurnChanged(this, new TurnChangedEventArgs(CurrentTurn));
            }
        }

        /// <summary>
        /// Проверяет, закончилась ли игра
        /// </summary>
        private void CheckGameOver()
        {
            bool playerWon = EnemyBoard.IsGameOver;
            bool computerWon = PlayerBoard.IsGameOver;

            if (playerWon || computerWon)
            {
                if (GameOver != null)
                {
                    GameOver(this, new GameOverEventArgs(playerWon));
                }
            }
        }
    }

    /// <summary>
    /// Аргументы события смены хода
    /// </summary>
    public class TurnChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Новый ход
        /// </summary>
        public Turn NewTurn { get; }

        /// <summary>
        /// Конструктор аргументов смены хода
        /// </summary>
        /// <param name="newTurn">Новый ход</param>
        public TurnChangedEventArgs(Turn newTurn)
        {
            NewTurn = newTurn;
        }
    }

    /// <summary>
    /// Аргументы события окончания игры
    /// </summary>
    public class GameOverEventArgs : EventArgs
    {
        /// <summary>
        /// Победил ли игрок
        /// </summary>
        public bool PlayerWon { get; }

        /// <summary>
        /// Конструктор аргументов окончания игры
        /// </summary>
        /// <param name="playerWon">Победил ли игрок</param>
        public GameOverEventArgs(bool playerWon)
        {
            PlayerWon = playerWon;
        }
    }
}
