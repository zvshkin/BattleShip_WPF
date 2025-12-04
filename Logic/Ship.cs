using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    /// <summary>
    /// Класс корабля
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// Размер корабля (количество палуб)
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Список позиций, занимаемых кораблем
        /// </summary>
        public List<Position> Positions { get; } = new List<Position>();

        /// <summary>
        /// Список позиций, по которым был нанесен удар
        /// </summary>
        public List<Position> Hits { get; } = new List<Position>();

        /// <summary>
        /// Проверяет, потоплен ли корабль
        /// </summary>
        public bool IsSunk => Hits.Count == Size;

        /// <summary>
        /// Конструктор корабля
        /// </summary>
        /// <param name="size">Размер корабля</param>
        public Ship(int size)
        {
            Size = size;
        }

        /// <summary>
        /// Записывает попадание по кораблю
        /// </summary>
        /// <param name="hitPosition">Позиция попадания</param>
        /// <returns>true, если попадание зарегистрировано, иначе false</returns>
        public bool RecordHit(Position hitPosition)
        {
            if (Positions.Contains(hitPosition) && !Hits.Contains(hitPosition))
            {
                Hits.Add(hitPosition);
                return true;
            }
            return false;
        }
    }
}
