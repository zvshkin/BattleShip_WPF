using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    public class Ship
    {
        public int Size { get; }

        public List<Position> Positions { get; } = new List<Position>();

        public List<Position> Hits { get; } = new List<Position>();

        public bool IsSunk => Hits.Count == Size;

        public Ship(int size)
        {
            Size = size;
        }

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
