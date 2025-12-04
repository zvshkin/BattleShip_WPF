using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF.UI
{
    /// <summary>
    /// Класс для отображения панели окончания игры
    /// </summary>
    public class GameOverPanel
    {
        private Panel gameOverPanel;
        private Timer animationTimer;
        private Form parentForm;

        /// <summary>
        /// Конструктор панели окончания игры
        /// </summary>
        /// <param name="parent">Родительская форма</param>
        public GameOverPanel(Form parent)
        {
            parentForm = parent;
            CreateGameOverPanel();
        }

        /// <summary>
        /// Создает панель окончания игры
        /// </summary>
        private void CreateGameOverPanel()
        {
            gameOverPanel = new Panel
            {
                Size = parentForm.ClientSize,
                Location = new Point(0, -parentForm.ClientSize.Height),
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Dock = DockStyle.None,
                Visible = false,
                Name = "gameOverPanel"
            };

            parentForm.Controls.Add(gameOverPanel);
            gameOverPanel.BringToFront();
        }

        /// <summary>
        /// Показывает экран окончания игры
        /// </summary>
        /// <param name="playerWon">Победил ли игрок</param>
        /// <param name="playerBoard">Доска игрока</param>
        /// <param name="enemyBoard">Доска противника</param>
        /// <param name="menuButtonClick">Обработчик нажатия кнопки меню</param>
        public void ShowGameOverScreen(bool playerWon, GameBoard playerBoard, GameBoard enemyBoard, EventHandler menuButtonClick)
        {
            gameOverPanel.Visible = true;
            gameOverPanel.Location = new Point(0, -parentForm.ClientSize.Height);
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

            TableLayoutPanel statsLayout = CreateStatsLayout(playerBoard, enemyBoard);
            gameOverPanel.Controls.Add(statsLayout);

            statsLayout.Location = new Point(
                (gameOverPanel.Width - statsLayout.Width) / 2,
                titleLabel.Height + 50
            );

            int buttonWidth = 280;
            int buttonHeight = 60;
            int buttonY = statsLayout.Location.Y + statsLayout.Height + 40;

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
            menuButton.Click += menuButtonClick;
            gameOverPanel.Controls.Add(menuButton);

            AnimatePanel();
        }

        /// <summary>
        /// Создает таблицу со статистикой игры
        /// </summary>
        /// <param name="playerBoard">Доска игрока</param>
        /// <param name="enemyBoard">Доска противника</param>
        /// <returns>Таблица со статистикой</returns>
        private TableLayoutPanel CreateStatsLayout(GameBoard playerBoard, GameBoard enemyBoard)
        {
            int totalShots = CountShots(enemyBoard);
            int hits = CountHits(enemyBoard);
            int misses = CountMisses(enemyBoard);
            int enemyShots = CountShots(playerBoard);

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

            AddStatRow(layout, "Выстрелов сделано:", totalShots.ToString(), 0);
            AddStatRow(layout, "Точных попаданий:", hits.ToString(), 1);
            AddStatRow(layout, "Промахов:", misses.ToString(), 2);
            
            string accuracy = "0%";
            if (totalShots > 0)
            {
                double accuracyValue = (double)hits / totalShots * 100;
                accuracy = $"{accuracyValue:0.0}%";
            }
            AddStatRow(layout, "Точность:", accuracy, 3);
            AddStatRow(layout, "Выстрелов компьютера:", enemyShots.ToString(), 4);

            return layout;
        }

        /// <summary>
        /// Подсчитывает количество выстрелов
        /// </summary>
        /// <param name="board">Доска для подсчета</param>
        /// <returns>Количество выстрелов</returns>
        private int CountShots(GameBoard board)
        {
            int count = 0;
            for (int r = 0; r < board.BoardSize; r++)
            {
                for (int c = 0; c < board.BoardSize; c++)
                {
                    if (board.Grid[r, c] == BoardCellState.Hit || board.Grid[r, c] == BoardCellState.Miss)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Подсчитывает количество попаданий
        /// </summary>
        /// <param name="board">Доска для подсчета</param>
        /// <returns>Количество попаданий</returns>
        private int CountHits(GameBoard board)
        {
            int count = 0;
            for (int r = 0; r < board.BoardSize; r++)
            {
                for (int c = 0; c < board.BoardSize; c++)
                {
                    if (board.Grid[r, c] == BoardCellState.Hit || board.Grid[r, c] == BoardCellState.Sunk)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Подсчитывает количество промахов
        /// </summary>
        /// <param name="board">Доска для подсчета</param>
        /// <returns>Количество промахов</returns>
        private int CountMisses(GameBoard board)
        {
            int count = 0;
            for (int r = 0; r < board.BoardSize; r++)
            {
                for (int c = 0; c < board.BoardSize; c++)
                {
                    if (board.Grid[r, c] == BoardCellState.Miss)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Добавляет строку статистики в таблицу
        /// </summary>
        /// <param name="layout">Таблица</param>
        /// <param name="label">Название</param>
        /// <param name="value">Значение</param>
        /// <param name="row">Номер строки</param>
        private void AddStatRow(TableLayoutPanel layout, string label, string value, int row)
        {
            Font statFont = FontLoader.GetFont("Rubik Mono One", 22);

            Label labelCtrl = new Label 
            { 
                Text = label, 
                Font = statFont, 
                ForeColor = Color.White, 
                TextAlign = ContentAlignment.MiddleLeft, 
                Dock = DockStyle.Fill 
            };
            Label valueCtrl = new Label 
            { 
                Text = value, 
                Font = statFont, 
                ForeColor = Color.White, 
                TextAlign = ContentAlignment.MiddleRight, 
                Dock = DockStyle.Fill 
            };

            layout.Controls.Add(labelCtrl, 0, row);
            layout.Controls.Add(valueCtrl, 1, row);
        }

        /// <summary>
        /// Запускает анимацию появления панели
        /// </summary>
        private void AnimatePanel()
        {
            int targetY = 0;
            int step = 40;

            if (animationTimer != null)
            {
                animationTimer.Stop();
            }

            animationTimer = new Timer { Interval = 10 };
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        /// <summary>
        /// Обработчик события таймера анимации
        /// </summary>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int targetY = 0;
            int step = 40;

            if (gameOverPanel.Location.Y < targetY)
            {
                int newY = gameOverPanel.Location.Y + step;
                if (newY >= targetY)
                {
                    newY = targetY;
                    animationTimer.Stop();
                }
                gameOverPanel.Location = new Point(0, newY);
            }
            else
            {
                animationTimer.Stop();
            }
        }

        /// <summary>
        /// Скрывает панель окончания игры
        /// </summary>
        public void Hide()
        {
            gameOverPanel.Visible = false;
        }
    }
}
