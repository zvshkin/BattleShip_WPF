using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;
using BattleShip_WPF.Controllers;
using BattleShip_WPF.UI;

namespace BattleShip_WPF
{
    /// <summary>
    /// Форма для расстановки кораблей
    /// </summary>
    public partial class SetupForm : Form
    {
        private GameBoard board = new GameBoard(10);
        private Button[,] buttons = new Button[10, 10];
        private ShipPlacementController placementController;
        private bool isHorizontal = true;

        /// <summary>
        /// Конструктор формы расстановки
        /// </summary>
        public SetupForm()
        {
            InitializeComponent();

            Dictionary<int, int> initialFleet = new Dictionary<int, int>
            {
                { 4, 1 }, { 3, 2 }, { 2, 3 }, { 1, 4 }
            };

            placementController = new ShipPlacementController(board, initialFleet);

            CreateBoardUI();
            CreateControls();
            UpdateControls();

            label1.Font = FontLoader.GetFont("Oi", 36);
            BackButton.Font = FontLoader.GetFont("Rubik Mono One", 12);
            this.BackColor = Color.FromArgb(229, 229, 229);
        }

        /// <summary>
        /// Создает интерфейс доски для расстановки
        /// </summary>
        private void CreateBoardUI()
        {
            this.Controls.Add(gamePanel);

            int cellSize = 40;
            for (int r = 0; r < 10; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Position pos = new Position(r, c);
                    Button btn = BoardRenderer.CreateStyledButton(cellSize, pos, Color.FromArgb(171, 169, 169), true);
                    btn.Click += Cell_Click;
                    buttons[r, c] = btn;
                    gamePanel.Controls.Add(btn);
                }
            }
        }

        /// <summary>
        /// Создает элементы управления
        /// </summary>
        private void CreateControls()
        {
            statusLabel.Font = FontLoader.GetFont("Rubik Mono One", 15);
            statusLabel.ForeColor = Color.FromArgb(59, 50, 35);
            this.Controls.Add(statusLabel);

            rotateBtn.Font = FontLoader.GetFont("Rubik Mono One", 12);
            rotateBtn.Click += RotateButton_Click;
            this.Controls.Add(rotateBtn);

            startButton.Font = FontLoader.GetFont("Oi", 20);
            startButton.Click += StartGame_Click;
            this.Controls.Add(startButton);
        }

        /// <summary>
        /// Обработчик нажатия кнопки поворота
        /// </summary>
        private void RotateButton_Click(object sender, EventArgs e)
        {
            isHorizontal = !isHorizontal;
            rotateBtn.Text = isHorizontal ? "Повернуть (Гориз)" : "Повернуть (Верт)";
        }

        /// <summary>
        /// Обновляет информацию о текущем состоянии расстановки
        /// </summary>
        private void UpdateControls()
        {
            int currentShipSize = placementController.CurrentShipSize;

            string status = $"Ставим: {currentShipSize}-палубный\n\nОсталось:\n";
            var remainingShips = placementController.GetRemainingShips();
            
            // Сортируем по убыванию размера
            var sortedShips = new List<KeyValuePair<int, int>>();
            foreach (var pair in remainingShips)
            {
                sortedShips.Add(pair);
            }
            sortedShips.Sort((a, b) => b.Key.CompareTo(a.Key));

            foreach (var pair in sortedShips)
            {
                status += $"{pair.Key}x: {pair.Value}\n";
            }
            
            statusLabel.Text = status;
        }

        /// <summary>
        /// Обработчик клика по ячейке доски
        /// </summary>
        private void Cell_Click(object sender, EventArgs e)
        {
            int currentShipSize = placementController.CurrentShipSize;
            if (currentShipSize == 0)
            {
                startButton.Enabled = true;
                return;
            }

            Position startPos = (Position)((Button)sender).Tag;
            List<Position> proposedPositions = new List<Position>();

            for (int i = 0; i < currentShipSize; i++)
            {
                int r = isHorizontal ? startPos.Row : startPos.Row + i;
                int c = isHorizontal ? startPos.Column + i : startPos.Column;
                proposedPositions.Add(new Position(r, c));
            }

            ShipPlacementResult result = placementController.TryPlaceShip(proposedPositions, currentShipSize);

            if (result.Success)
            {
                foreach (var p in proposedPositions)
                {
                    buttons[p.Row, p.Column].BackColor = Color.FromArgb(82, 82, 82);
                    buttons[p.Row, p.Column].Enabled = false;
                }

                UpdateControls();

                if (placementController.AllShipsPlaced)
                {
                    startButton.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show(result.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Удаляет последний размещенный корабль
        /// </summary>
        private void RemoveLastShip()
        {
            ShipRemovalResult result = placementController.RemoveLastShip();

            if (result.Success)
            {
                foreach (var pos in result.Ship.Positions)
                {
                    buttons[pos.Row, pos.Column].BackColor = Color.FromArgb(171, 169, 169);
                    buttons[pos.Row, pos.Column].Enabled = true;
                }

                UpdateControls();
                startButton.Enabled = false;
            }
            else
            {
                MessageBox.Show(result.Message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки начала игры
        /// </summary>
        private void StartGame_Click(object sender, EventArgs e)
        {
            FormNavigator.NavigateToForm(this, new GameForm(this.board, false));
        }

        /// <summary>
        /// Обработчик нажатия кнопки назад
        /// </summary>
        private void BackButton_Click(object sender, EventArgs e)
        {
            FormNavigator.NavigateToForm(this, new ModeForm());
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления корабля
        /// </summary>
        private void dellBtn_Click(object sender, EventArgs e)
        {
            RemoveLastShip();
        }
    }
}
