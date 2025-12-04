using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    /// <summary>
    /// Перечисление состояний ячейки игровой доски
    /// </summary>
    public enum BoardCellState
    {
        /// <summary>
        /// Пустая ячейка
        /// </summary>
        Empty,

        /// <summary>
        /// Ячейка с кораблем
        /// </summary>
        Ship,

        /// <summary>
        /// Промах
        /// </summary>
        Miss,

        /// <summary>
        /// Попадание
        /// </summary>
        Hit,

        /// <summary>
        /// Потопленный корабль
        /// </summary>
        Sunk
    }
}
