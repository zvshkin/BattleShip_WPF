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
        private Timer computerTimer;

        private Queue<Position> potentialTargets = new Queue<Position>();

        public GameForm(GameBoard playerBoard, bool isFastMode)
        {
            InitializeComponent();

            this.myBoard = playerBoard;
            this.boardSize = isFastMode ? 8 : 10;

            this.BackColor = Color.FromArgb(162, 191, 244);
            this.Text = "Морской Бой - " + (isFastMode ? "Ускоренный" : "Классический");

            InitializeEnemy(isFastMode);
            CreateBoardsUI();

            InitializeExitButton();

            turnIndicator.Font = FontLoader.GetFont("Oi", 36);
            myBoardLabel.Font = FontLoader.GetFont("Rubik Mono One", 16);
            enemyBoardLabel.Font = FontLoader.GetFont("Rubik Mono One", 16);

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

        private void InitializeExitButton()
        {
            Button exitButton = new Button
            {
                Text = "Выход в меню",
                Font = FontLoader.GetFont("Rubik Mono One", 12),
                BackColor = Color.OrangeRed,
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };
            exitButton.Click += ExitButton_Click;
            tableLayoutPanel1.Controls.Add(exitButton, 1, 2);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Перейти в меню?",
                "Подтверждение перехода",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ModeForm modeForm = new ModeForm();
                modeForm.FormClosed += (s, args) => Application.Exit();
                modeForm.Show();
                this.Hide();
            }
            else
            {
            }

        }

        private void CreateBoardsUI()
        {
            int cellSize = 30;
            int boardWidth = boardSize * cellSize;
            int boardHeight = boardSize * cellSize;

            Panel myPanel = new Panel { Size = new Size(boardWidth, boardHeight), BackColor = Color.FromArgb(120, 160, 220), Anchor = AnchorStyles.Top };

            myButtons = DrawBoardButtons(myPanel, myBoard, false, cellSize);

            Panel enemyPanel = new Panel { Size = new Size(boardWidth, boardHeight), BackColor = Color.FromArgb(120, 160, 220), Anchor = AnchorStyles.Top };

            enemyButtons = DrawBoardButtons(enemyPanel, enemyBoard, true, cellSize);

            tableLayoutPanel1.Controls.Add(myPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(enemyPanel, 2, 1);
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

            if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
            {
                targetButton.BackColor = (result == BoardCellState.Sunk) ? Color.Red : Color.OrangeRed;

                if (result == BoardCellState.Hit)
                {
                    AddNeighborsToTargetQueue(target);
                }
                else
                {
                    potentialTargets.Clear();
                }

                if (CheckLose()) return;

                computerTimer.Start();
            }
            else
            {
                targetButton.BackColor = Color.LightGray;

                currentTurn = Turn.Player;
                UpdateTurnIndicator();
                return;
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
                MessageBox.Show("👑 Поздравляем! Вы потопили весь флот противника. ВЫ ПОБЕДИЛИ! 🏆", "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetEnemyBoardEnabled(false);
                computerTimer.Stop();
                return true;
            }
            return false;
        }

        private bool CheckLose()
        {
            if (myBoard.IsGameOver)
            {
                MessageBox.Show("⚓️ Сожалеем, весь ваш флот потоплен. ПОРАЖЕНИЕ. 😭", "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetEnemyBoardEnabled(false);
                computerTimer.Stop();
                return true;
            }
            return false;
        }
    }
}