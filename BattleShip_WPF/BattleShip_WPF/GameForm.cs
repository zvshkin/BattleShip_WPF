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

        private Panel gameOverPanel;
        private Timer animationTimer;

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
            InitializeStatusPanel();
            CreateGameOverPanel();

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
            int boardWidth = boardSize * cellSize + cellSize;
            int boardHeight = boardSize * cellSize + cellSize;

            Panel myPanel = new Panel { Size = new Size(boardWidth, boardHeight), BackColor = Color.FromArgb(120, 160, 220), Anchor = AnchorStyles.Top };
            DrawAxisLabels(myPanel, boardSize, cellSize);
            myButtons = DrawBoardButtons(myPanel, myBoard, false, cellSize);
            tableLayoutPanel1.Controls.Add(myPanel, 0, 1);

            Panel enemyPanel = new Panel { Size = new Size(boardWidth, boardHeight), BackColor = Color.FromArgb(120, 160, 220), Anchor = AnchorStyles.Top };
            DrawAxisLabels(enemyPanel, boardSize, cellSize);
            enemyButtons = DrawBoardButtons(enemyPanel, enemyBoard, true, cellSize);
            tableLayoutPanel1.Controls.Add(enemyPanel, 2, 1);
        }

        private void DrawAxisLabels(Panel parentPanel, int size, int cellSize)
        {
            for (int r = 0; r < size; r++)
            {
                Label rowLabel = new Label
                {
                    Text = (r + 1).ToString(),
                    Font = FontLoader.GetFont("Rubik Mono One", 8),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(0, (r + 1) * cellSize),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                parentPanel.Controls.Add(rowLabel);
            }

            string colLetters = "АБВГДЕЖЗИК".Substring(0, size);
            for (int c = 0; c < size; c++)
            {
                Label colLabel = new Label
                {
                    Text = colLetters[c].ToString(),
                    Font = FontLoader.GetFont("Rubik Mono One", 8),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point((c + 1) * cellSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                parentPanel.Controls.Add(colLabel);
            }
        }

        private void InitializeStatusPanel()
        {
            Panel statusPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(162, 191, 244)
            };

            TableLayoutPanel legendLayout = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 2,
                Dock = DockStyle.Top,
                Location = new Point(0, 40),
                AutoSize = true,
                Anchor = AnchorStyles.Top
            };
            legendLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
            legendLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddLegendItem(legendLayout, Color.Navy, "Ваш корабль", 0);
            AddLegendItem(legendLayout, Color.CornflowerBlue, "Пустая вода", 1);
            AddLegendItem(legendLayout, Color.LightGray, "Промах", 2);
            AddLegendItem(legendLayout, Color.OrangeRed, "Попадание", 3);
            AddLegendItem(legendLayout, Color.Red, "Потоплен", 4);

            statusPanel.Controls.Add(legendLayout);

            tableLayoutPanel1.Controls.Add(statusPanel, 1, 1);
        }

        private void AddLegendItem(TableLayoutPanel layout, Color color, string text, int row)
        {
            Panel colorSquare = new Panel { BackColor = color, Size = new Size(20, 20), Margin = new Padding(5) };
            layout.Controls.Add(colorSquare, 0, row);

            Label textLabel = new Label
            {
                Text = text,
                Font = FontLoader.GetFont("Rubik Mono One", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Margin = new Padding(5)
            };
            layout.Controls.Add(textLabel, 1, row);
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
                        Location = new Point((c + 1) * cellSize, (r + 1) * cellSize),
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

                if (CheckGameOver()) return;

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

                if (CheckGameOver()) return;

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

        private void CreateGameOverPanel()
        {
            gameOverPanel = new Panel
            {
                Size = this.ClientSize,
                Location = new Point(0, -this.ClientSize.Height),
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Dock = DockStyle.None,
                Visible = false,
                Name = "gameOverPanel"
            };

            this.Controls.Add(gameOverPanel);
            gameOverPanel.BringToFront();
        }

        private void ShowGameOverScreen(bool playerWon)
        {
            SetEnemyBoardEnabled(false);
            computerTimer.Stop();

            gameOverPanel.Visible = true;
            gameOverPanel.Location = new Point(0, -this.ClientSize.Height); 

            gameOverPanel.Controls.Clear();

            Color baseColor = playerWon ? Color.DarkGreen : Color.DarkRed;
            gameOverPanel.BackColor = Color.FromArgb(180, baseColor);


            Label titleLabel = new Label
            {
                Text = playerWon ? "ПОБЕДА!" : "ПОРАЖЕНИЕ",
                Font = FontLoader.GetFont("Oi", 60),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 150
            };
            gameOverPanel.Controls.Add(titleLabel);

            TableLayoutPanel statsLayout = CreateStatsLayout();
            gameOverPanel.Controls.Add(statsLayout);

            statsLayout.Location = new Point(
                (gameOverPanel.Width - statsLayout.Width) / 2,
                titleLabel.Height + 50
            );

            int buttonWidth = 280;
            int buttonHeight = 60;
            int buttonY = statsLayout.Location.Y + statsLayout.Height + 40;
            int spacing = 30;
            int totalButtonWidth = (buttonWidth * 2) + spacing;
            int startX = (gameOverPanel.Width - totalButtonWidth) / 2;

            Button menuButton = new Button
            {
                Text = "В МЕНЮ",
                Font = FontLoader.GetFont("Rubik Mono One", 14),
                BackColor = Color.White,
                Size = new Size(buttonWidth, buttonHeight),
                Location = new Point(350, buttonY),
                FlatStyle = FlatStyle.Flat
            };
            menuButton.FlatAppearance.BorderSize = 0;
            menuButton.Click += MenuButton_Click;
            gameOverPanel.Controls.Add(menuButton);

            int targetY = 0;
            int step = 40;

            animationTimer?.Stop();
            animationTimer = new Timer { Interval = 10 };
            animationTimer.Tick += (s, e) =>
            {
                if (gameOverPanel.Location.Y < targetY)
                {
                    gameOverPanel.Location = new Point(0, gameOverPanel.Location.Y + step);

                    if (gameOverPanel.Location.Y >= targetY)
                    {
                        gameOverPanel.Location = new Point(0, targetY);
                        animationTimer.Stop();
                    }
                }
                else
                {
                    animationTimer.Stop();
                }
            };
            animationTimer.Start();
        }

        private TableLayoutPanel CreateStatsLayout()
        {
            int totalShots = enemyBoard.Grid.Cast<BoardCellState>().Count(s => s == BoardCellState.Hit || s == BoardCellState.Miss);
            int hits = enemyBoard.Grid.Cast<BoardCellState>().Count(s => s == BoardCellState.Hit || s == BoardCellState.Sunk);
            int misses = enemyBoard.Grid.Cast<BoardCellState>().Count(s => s == BoardCellState.Miss);

            int enemyShots = myBoard.Grid.Cast<BoardCellState>().Count(s => s == BoardCellState.Hit || s == BoardCellState.Miss);

            TableLayoutPanel layout = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 2,
                AutoSize = true,
                BackColor = Color.Transparent,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Width = 1000,
                Padding = new Padding(15)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));

            Action<string, string, int> AddStat = (label, value, row) =>
            {
                Font statFont = FontLoader.GetFont("Rubik Mono One", 22);

                Label labelCtrl = new Label { Text = label, Font = statFont, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill };
                Label valueCtrl = new Label { Text = value, Font = statFont, ForeColor = Color.White, TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill };

                layout.Controls.Add(labelCtrl, 0, row);
                layout.Controls.Add(valueCtrl, 1, row);
            };

            AddStat("Выстрелов сделано:", totalShots.ToString(), 0);
            AddStat("Точных попаданий:", hits.ToString(), 1);
            AddStat("Промахов:", misses.ToString(), 2);
            AddStat("Точность:", (totalShots > 0 ? $"{(double)hits / totalShots * 100:0.0}%" : "0%"), 3);
            AddStat("Выстрелов компьютера:", enemyShots.ToString(), 4);

            return layout;
        }

        private void MenuButton_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm();
            modeForm.FormClosed += (s, args) => Application.Exit();
            modeForm.Show();
            this.Hide();
        }

        private bool CheckGameOver()
        {
            if (enemyBoard.IsGameOver)
            {
                ShowGameOverScreen(true);
                return true;
            }

            if (myBoard.IsGameOver)
            {
                ShowGameOverScreen(false);
                return true;
            }

            return false;
        }
    }
}