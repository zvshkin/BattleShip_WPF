using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF
{
    public enum Turn { Player, Computer }

    public partial class GameForm : Form
    {
        private static readonly Random aiRandom = new Random();

        private GameBoard myBoard;
        private GameBoard enemyBoard;
        private Button[,] enemyButtons;
        private Button[,] myButtons;
        private int boardSize;

        private Turn currentTurn = Turn.Player;
        private Label turnIndicator;
        private Timer computerTimer;

        private Queue<Position> potentialTargets = new Queue<Position>();

        public GameForm(GameBoard playerBoard, bool isFastMode)
        {
            InitializeComponent();

            this.myBoard = playerBoard;
            this.boardSize = isFastMode ? 8 : 10;

            InitializeEnemy(isFastMode);
            CreateBoardsUI();
            InitializeTurnIndicator();
            UpdateTurnIndicator();

            computerTimer = new Timer { Interval = 500 };
            computerTimer.Tick += ComputerTimer_Tick;
        }

        private void InitializeEnemy(bool isFastMode)
        {
            var fleet = isFastMode ? FleetGenerator.FastFleet : FleetGenerator.ClassicFleet;
            List<Ship> enemyShips = FleetGenerator.GenerateRandomFleet(boardSize, fleet);
            enemyBoard = new GameBoard(boardSize);

            foreach (var ship in enemyShips) enemyBoard.PlaceShip(ship);
        }

        private void InitializeTurnIndicator()
        {
            turnIndicator = new Label
            {
                Location = new Point(this.Width / 2 - 150, 10),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = FontLoader.GetFont("Rubik Mono One", 14),
                ForeColor = Color.Navy,
                BackColor = Color.CornflowerBlue
            };
            this.Controls.Add(turnIndicator);
        }

        private void CreateBoardsUI()
        {
            int cellSize = 30;
            int boardWidth = boardSize * cellSize;

            Panel myPanel = new Panel { Location = new Point(20, 70), Size = new Size(boardWidth, boardWidth), BackColor = Color.FromArgb(120, 160, 220) };
            this.Controls.Add(myPanel);

            myButtons = DrawBoardButtons(myPanel, myBoard, false, cellSize);

            Panel enemyPanel = new Panel { Location = new Point(20 + boardWidth + 50, 70), Size = new Size(boardWidth, boardWidth), BackColor = Color.FromArgb(120, 160, 220) };
            this.Controls.Add(enemyPanel);

            enemyButtons = DrawBoardButtons(enemyPanel, enemyBoard, true, cellSize);
        }

        private Button[,] DrawBoardButtons(Panel parentPanel, GameBoard boardData, bool isClickable, int cellSize)
        {
            Button[,] btns = new Button[boardSize, boardSize];

            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    Color baseColor;

                    if (!isClickable && boardData.Grid[r, c] == BoardCellState.Ship)
                    {
                        baseColor = Color.Navy;
                    }
                    else
                    {
                        baseColor = Color.CornflowerBlue;
                    }

                    Button btn = new Button
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point(c * cellSize, r * cellSize),
                        Tag = new Position(r, c),
                        BackColor = baseColor,
                        FlatStyle = FlatStyle.Flat,
                        Enabled = isClickable
                    };

                    if (isClickable)
                    {
                        btn.Click += EnemyCell_Click;
                    }

                    btns[r, c] = btn;
                    parentPanel.Controls.Add(btn);
                }
            }

            if (!isClickable)
            {
                myButtons = btns;
            }

            return btns;
        }

        private void UpdateTurnIndicator()
        {
            if (currentTurn == Turn.Player)
            {
                turnIndicator.Text = "ВАШ ХОД";
                SetEnemyBoardEnabled(true);
            }
            else
            {
                turnIndicator.Text = "ХОД ПРОТИВНИКА";
                SetEnemyBoardEnabled(false);
            }
        }

        private void SetEnemyBoardEnabled(bool enabled)
        {
            int size = enemyButtons.GetLength(0);
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (enemyButtons[r, c].BackColor == Color.CornflowerBlue)
                    {
                        enemyButtons[r, c].Enabled = enabled;
                    }
                }
            }
        }

        private void EnemyCell_Click(object sender, EventArgs e)
        {
            if (currentTurn != Turn.Player) return;

            Button btn = (Button)sender;
            Position pos = (Position)btn.Tag;

            BoardCellState result = enemyBoard.Shoot(pos);
            btn.Enabled = false;

            if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
            {
                btn.BackColor = (result == BoardCellState.Sunk) ? Color.Red : Color.OrangeRed;

                if (CheckWin()) return;

                MessageBox.Show(result == BoardCellState.Sunk ? "Корабль потоплен!" : "Попадание!");
                return;
            }
            else
            {
                btn.BackColor = Color.LightGray;

                currentTurn = Turn.Computer;
                UpdateTurnIndicator();
                computerTimer.Start();
            }
        }


        private void ComputerTimer_Tick(object sender, EventArgs e)
        {
            computerTimer.Stop();
            DoComputerTurn();
        }

        private void DoComputerTurn()
        {
            while (currentTurn == Turn.Computer)
            {
                Position target;

                if (potentialTargets.Count > 0)
                {
                    target = potentialTargets.Dequeue();
                }
                else
                {
                    int r, c;
                    do
                    {
                        r = aiRandom.Next(boardSize);
                        c = aiRandom.Next(boardSize);
                        target = new Position(r, c);
                    }
                    while (myBoard.Grid[r, c] != BoardCellState.Empty && myBoard.Grid[r, c] != BoardCellState.Ship);
                }

                BoardCellState result = myBoard.Shoot(target);

                Button targetButton = myButtons[target.Row, target.Column];
                targetButton.Enabled = false;

                if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
                {
                    targetButton.BackColor = (result == BoardCellState.Sunk) ? Color.Red : Color.OrangeRed;

                    if (result == BoardCellState.Hit)
                    {
                        AddNeighborsToTargetQueue(target);
                        computerTimer.Start();
                    }
                    else
                    {
                        potentialTargets.Clear();
                    }

                    if (CheckLose()) return;
                }
                else
                {
                    targetButton.BackColor = Color.LightGray;

                    currentTurn = Turn.Player;
                    UpdateTurnIndicator();
                    return;
                }
            }
        }

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
                    BoardCellState state = myBoard.Grid[r, c];

                    if (state == BoardCellState.Empty || state == BoardCellState.Ship)
                    {
                        Position newTarget = new Position(r, c);
                        if (!potentialTargets.Contains(newTarget))
                        {
                            potentialTargets.Enqueue(newTarget);
                        }
                    }
                }
            }
        }

        private bool CheckWin()
        {
            if (enemyBoard.IsGameOver)
            {
                MessageBox.Show("ПОБЕДА! Вы потопили весь флот противника.", "Игра окончена");
                SetEnemyBoardEnabled(false);
                return true;
            }
            return false;
        }

        private bool CheckLose()
        {
            if (myBoard.IsGameOver)
            {
                MessageBox.Show("ПОРАЖЕНИЕ! Весь ваш флот потоплен.", "Игра окончена");
                return true;
            }
            return false;
        }
    }
}