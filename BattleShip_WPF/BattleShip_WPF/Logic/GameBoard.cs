using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_WPF.Logic
{
    public class GameBoard
    {
        public int BoardSize { get; }
        public BoardCellState[,] Grid { get; private set; }
        public List<Ship> Ships { get; } = new List<Ship>();

        public bool IsGameOver => Ships.All(s => s.IsSunk);

        public GameBoard(int size)
        {
            BoardSize = size;
            Grid = new BoardCellState[size, size];
        }

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
    }
}
