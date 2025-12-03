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
        private Stack<Ship> placedShips = new Stack<Ship>();
        private Button[,] buttons = new Button[10, 10];

        private Dictionary<int, int> shipsToPlace = new Dictionary<int, int>
    {
        { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 }
    };

        private bool isHorizontal = true;
        private int currentShipSize = 4;

        public SetupForm()
        {
            InitializeComponent();

            CreateBoardUI();
            CreateControls();
            UpdateControls();

            label1.Font = FontLoader.GetFont("Oi", 36);
            BackButton.Font = FontLoader.GetFont("Rubik Mono One", 12);
        }

        private void CreateBoardUI()
        {
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
            statusLabel.Font = FontLoader.GetFont("Rubik Mono One", 15);
            this.Controls.Add(statusLabel);

            rotateBtn.Font = FontLoader.GetFont("Rubik Mono One", 12);
            rotateBtn.Click += (s, e) => {
                isHorizontal = !isHorizontal;
                rotateBtn.Text = isHorizontal ? "Повернуть (Гориз)" : "Повернуть (Верт)";
            };
            this.Controls.Add(rotateBtn);

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
            if (AllShipsPlaced())
            {
                startButton.Enabled = true;
            }

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
                    placedShips.Push(newShip);
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

        private bool AllShipsPlaced()
        {
            return shipsToPlace.Values.All(count => count == 0);
        }

        private void RemoveLastShip()
        {
            if (placedShips.Count == 0)
            {
                MessageBox.Show("Нет кораблей для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Ship lastShip = placedShips.Pop();
            int shipSize = lastShip.Size;

            board.RemoveShip(lastShip);

            foreach (var pos in lastShip.Positions)
            {
                buttons[pos.Row, pos.Column].BackColor = Color.CornflowerBlue;
                buttons[pos.Row, pos.Column].Enabled = true;
            }

            shipsToPlace[shipSize]++;

            UpdateControls();
            startButton.Enabled = false;
        }

        private void StartGame_Click(object sender, EventArgs e)
        {
            GameForm game = new GameForm(this.board, false);
            game.FormClosed += (s, args) => Application.Exit();
            game.Show();
            this.Hide();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            ModeForm modeForm = new ModeForm();
            modeForm.FormClosed += (s, args) => Application.Exit();
            modeForm.Show();
            this.Hide();
        }

        private void dellBtn_Click(object sender, EventArgs e)
        {
            RemoveLastShip();
        }
    }
}
