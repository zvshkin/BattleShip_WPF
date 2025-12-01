using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF
{
    public partial class SetupForm : Form
    {
        private GameBoard board = new GameBoard(10);
        private Button[,] buttons = new Button[10, 10];

        private Dictionary<int, int> shipsToPlace = new Dictionary<int, int>
    {
        { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 }
    };

        private bool isHorizontal = true;
        private int currentShipSize = 4;
        private Button startButton;
        private Label statusLabel;

        public SetupForm()
        {
            InitializeComponent();

            this.Size = new Size(800, 550);
            this.BackColor = Color.FromArgb(162, 191, 244);
            this.Text = "Своя игра: Расстановка флота";

            CreateBoardUI();
            CreateControls();
            UpdateControls();
        }

        private void CreateBoardUI()
        {
            Panel gamePanel = new Panel { Location = new Point(20, 20), Size = new Size(400, 400), BackColor = Color.FromArgb(120, 160, 220) };
            this.Controls.Add(gamePanel);

            int cellSize = 40;
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point(c * cellSize, r * cellSize),
                        BackColor = Color.CornflowerBlue,
                        Tag = new Position(r, c)
                    };
                    btn.Click += Cell_Click;
                    buttons[r, c] = btn;
                    gamePanel.Controls.Add(btn);
                }
            }
        }

        private void CreateControls()
        {
            statusLabel = new Label { Location = new Point(450, 20), AutoSize = true, ForeColor = Color.Navy };
            statusLabel.Font = FontLoader.GetFont("Rubik Mono One", 10);
            this.Controls.Add(statusLabel);

            Button rotateBtn = new Button { Text = "Повернуть (Гориз)", Location = new Point(450, 180), Size = new Size(180, 50), BackColor = Color.CornflowerBlue, ForeColor = Color.Navy };
            rotateBtn.Font = FontLoader.GetFont("Rubik Mono One", 12);
            rotateBtn.Click += (s, e) => {
                isHorizontal = !isHorizontal;
                rotateBtn.Text = isHorizontal ? "Повернуть (Гориз)" : "Повернуть (Верт)";
            };
            this.Controls.Add(rotateBtn);

            startButton = new Button { Text = "В БОЙ!", Location = new Point(450, 350), Size = new Size(180, 60), BackColor = Color.OrangeRed, ForeColor = Color.White, Enabled = false };
            startButton.Font = FontLoader.GetFont("Oi", 20);
            startButton.Click += StartGame_Click;
            this.Controls.Add(startButton);
        }

        private void UpdateControls()
        {
            currentShipSize = shipsToPlace.Keys.Where(size => shipsToPlace[size] > 0).OrderByDescending(size => size).FirstOrDefault();

            string status = $"Ставим: {currentShipSize}-палубный\n\nОсталось:\n";
            foreach (var pair in shipsToPlace.OrderByDescending(p => p.Key))
            {
                status += $"{pair.Key}x: {pair.Value}\n";
            }
            statusLabel.Text = status;
        }

        private void Cell_Click(object sender, EventArgs e)
        {
            if (currentShipSize == 0) return;

            Position startPos = (Position)((Button)sender).Tag;
            List<Position> proposedPositions = new List<Position>();

            for (int i = 0; i < currentShipSize; i++)
            {
                int r = isHorizontal ? startPos.Row : startPos.Row + i;
                int c = isHorizontal ? startPos.Column + i : startPos.Column;
                proposedPositions.Add(new Position(r, c));
            }

            try
            {
                if (board.IsPlacementValid(proposedPositions))
                {
                    Ship newShip = new Ship(currentShipSize);
                    newShip.Positions.AddRange(proposedPositions);
                    board.PlaceShip(newShip);

                    foreach (var p in proposedPositions)
                    {
                        buttons[p.Row, p.Column].BackColor = Color.Navy;
                        buttons[p.Row, p.Column].Enabled = false;
                    }

                    shipsToPlace[currentShipSize]--;
                    UpdateControls();

                    if (currentShipSize == 0)
                    {
                        startButton.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя разместить здесь: выходит за границы или касается других кораблей.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка размещения");
            }
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            GameForm game = new GameForm(this.board, false);
            game.Show();
            this.Hide();
        }
    }
}
