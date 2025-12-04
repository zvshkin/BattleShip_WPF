using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    /// <summary>
    /// Класс для представления позиции на игровой доске
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Номер строки
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Номер столбца
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// Конструктор позиции
        /// </summary>
        /// <param name="row">Номер строки</param>
        /// <param name="column">Номер столбца</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Сравнивает две позиции на равенство
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>true, если позиции равны, иначе false</returns>
        public override bool Equals(object obj)
        {
            if (obj is Position other)
            {
                return Row == other.Row && Column == other.Column;
            }
            return false;
        }

        /// <summary>
        /// Возвращает хеш-код позиции
        /// </summary>
        /// <returns>Хеш-код</returns>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Row.GetHashCode();
            hash = hash * 23 + Column.GetHashCode();
            return hash;
        }
    }
}
