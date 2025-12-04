using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;
using BattleShip_WPF.UI;
using BattleShip_WPF.Controllers;
using BattleShip_WPF.Sounds;

namespace BattleShip_WPF
{
    /// <summary>
    /// Форма игрового процесса
    /// </summary>
    public partial class GameForm : Form
    {
        private GameBoard myBoard;
        private GameBoard enemyBoard;
        private Button[,] enemyButtons;
        private Button[,] myButtons;
        private int boardSize;

        private GameController gameController;
        private ComputerAI computerAI;
        private Timer computerTimer;
        private GameOverPanel gameOverPanel;
        private SoundManager soundManager;

        /// <summary>
        /// Конструктор формы игры
        /// </summary>
        /// <param name="playerBoard">Доска игрока</param>
        /// <param name="isFastMode">Режим быстрой игры</param>
        public GameForm(GameBoard playerBoard, bool isFastMode)
        {
            InitializeComponent();

            this.myBoard = playerBoard;
            this.boardSize = isFastMode ? 8 : 10;

            this.BackColor = Color.FromArgb(229, 229, 229);
            this.Text = "Морской Бой - " + (isFastMode ? "Ускоренный" : "Классический");

            enemyBoard = GameFactory.CreateEnemyBoard(isFastMode);
            gameController = new GameController(myBoard, enemyBoard);
            computerAI = new ComputerAI(boardSize);

            gameController.TurnChanged += GameController_TurnChanged;
            gameController.GameOver += GameController_GameOver;

            CreateBoardsUI();
            InitializeExitButton();

            turnIndicator.Font = FontLoader.GetFont("Oi", 36);
            myBoardLabel.Font = FontLoader.GetFont("Rubik Mono One", 16);
            enemyBoardLabel.Font = FontLoader.GetFont("Rubik Mono One", 16);

            UpdateTurnIndicator();
            InitializeStatusPanel();
            gameOverPanel = new GameOverPanel(this);

            computerTimer = new Timer { Interval = 500 };
            computerTimer.Tick += ComputerTimer_Tick;

            soundManager = new SoundManager();
            LoadSoundSettings();
            InitializeSoundButton();
        }

        /// <summary>
        /// Загружает настройки звука
        /// </summary>
        private void LoadSoundSettings()
        {
            soundManager.SetMusicEnabled(Properties.Settings.Default.MusicEnabled);
            soundManager.SetSoundEnabled(Properties.Settings.Default.SoundEnabled);
            if (Properties.Settings.Default.MusicEnabled)
            {
                soundManager.PlayBackgroundMusic();
            }
        }

        /// <summary>
        /// Инициализирует кнопку настроек звука
        /// </summary>
        private void InitializeSoundButton()
        {
            Button soundButton = new Button
            {
                Text = "🔊",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = FontLoader.GetFont("Rubik Mono One", 55),
                ForeColor = Color.FromArgb(59, 50, 35),
                Size = new Size(94, 94),
                Location = new Point(this.ClientSize.Width - 120, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat
            };
            soundButton.FlatAppearance.BorderSize = 0;
            soundButton.Click += SoundButton_Click;
            this.Controls.Add(soundButton);
            soundButton.BringToFront();

            // Обновляем позицию после загрузки формы
            tableLayoutPanel1.Controls.Add(soundButton, 2, 2);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку настроек звука
        /// </summary>
        private void SoundButton_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new SoundSettings(soundManager))
            {
                if (settingsForm.ShowDialog(this) == DialogResult.OK)
                {
                    // Сохраняем настройки в Properties.Settings
                    Properties.Settings.Default.MusicEnabled = soundManager.IsMusicEnabled;
                    Properties.Settings.Default.SoundEnabled = soundManager.IsSoundEnabled;
                    Properties.Settings.Default.Save();
                }
            }
        }

        /// <summary>
        /// Инициализирует кнопку выхода в меню
        /// </summary>
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

        /// <summary>
        /// Обработчик нажатия кнопки выхода
        /// </summary>
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
                soundManager.StopBackgroundMusic();
                FormNavigator.NavigateToForm(this, new ModeForm());
            }
        }

        /// <summary>
        /// Создает интерфейс игровых досок
        /// </summary>
        private void CreateBoardsUI()
        {
            int cellSize = 30;

            Panel myPanel = BoardRenderer.CreateBoardPanel(boardSize, cellSize);
            myButtons = BoardRenderer.CreateBoardButtons(myPanel, myBoard, boardSize, false, cellSize);
            tableLayoutPanel1.Controls.Add(myPanel, 0, 1);

            Panel enemyPanel = BoardRenderer.CreateBoardPanel(boardSize, cellSize);
            enemyButtons = BoardRenderer.CreateBoardButtons(enemyPanel, enemyBoard, boardSize, true, cellSize, EnemyCell_Click);
            tableLayoutPanel1.Controls.Add(enemyPanel, 2, 1);
        }

        /// <summary>
        /// Инициализирует панель статуса с легендой
        /// </summary>
        private void InitializeStatusPanel()
        {
            Panel statusPanel = LegendPanel.CreateLegendPanel();
            tableLayoutPanel1.Controls.Add(statusPanel, 1, 1);
        }

        /// <summary>
        /// Обновляет индикатор текущего хода
        /// </summary>
        private void UpdateTurnIndicator()
        {
            if (gameController.CurrentTurn == Turn.Player)
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

        /// <summary>
        /// Обработчик события смены хода
        /// </summary>
        private void GameController_TurnChanged(object sender, TurnChangedEventArgs e)
        {
            UpdateTurnIndicator();
        }

        /// <summary>
        /// Обработчик события окончания игры
        /// </summary>
        private void GameController_GameOver(object sender, GameOverEventArgs e)
        {
            soundManager.StopBackgroundMusic();
            gameOverPanel.ShowGameOverScreen(e.PlayerWon, myBoard, enemyBoard, MenuButton_Click);
        }

        /// <summary>
        /// Включает или отключает доску противника
        /// </summary>
        /// <param name="enabled">Включена ли доска</param>
        private void SetEnemyBoardEnabled(bool enabled)
        {
            int size = enemyButtons.GetLength(0);
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    Color btnColor = enemyButtons[r, c].BackColor;
                    if (btnColor != Color.LightGray && btnColor != Color.OrangeRed && btnColor != Color.Red)
                    {
                        enemyButtons[r, c].Enabled = enabled;
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик клика по ячейке доски противника
        /// </summary>
        private void EnemyCell_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Position pos = (Position)btn.Tag;

            BoardCellState result = gameController.ProcessPlayerShot(pos);
            btn.Enabled = false;

            if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
            {
                if (result == BoardCellState.Sunk)
                {
                    btn.BackColor = Color.Red;
                    UpdateNeighboringCells(enemyBoard, enemyButtons, pos);
                }
                else
                {
                    btn.BackColor = Color.OrangeRed;
                }
            }
            else
            {
                btn.BackColor = Color.LightGray;
                computerTimer.Start();
            }
        }

        /// <summary>
        /// Обновляет соседние ячейки потопленного корабля
        /// </summary>
        /// <param name="board">Игровая доска</param>
        /// <param name="buttons">Массив кнопок</param>
        /// <param name="hitPosition">Позиция попадания</param>
        private void UpdateNeighboringCells(GameBoard board, Button[,] buttons, Position hitPosition)
        {
            Ship sunkShip = FindSunkShip(board, hitPosition);
            if (sunkShip == null) return;

            foreach (var shipPos in sunkShip.Positions)
            {
                for (int r = shipPos.Row - 1; r <= shipPos.Row + 1; r++)
                {
                    for (int c = shipPos.Column - 1; c <= shipPos.Column + 1; c++)
                    {
                        if (r >= 0 && r < boardSize && c >= 0 && c < boardSize)
                        {
                            if (board.Grid[r, c] == BoardCellState.Miss)
                            {
                                Button neighborBtn = buttons[r, c];
                                neighborBtn.BackColor = Color.LightGray;
                                neighborBtn.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Находит потопленный корабль по позиции попадания
        /// </summary>
        /// <param name="board">Игровая доска</param>
        /// <param name="hitPosition">Позиция попадания</param>
        /// <returns>Потопленный корабль или null</returns>
        private Ship FindSunkShip(GameBoard board, Position hitPosition)
        {
            foreach (var ship in board.Ships)
            {
                if (ship.IsSunk && ship.Positions.Contains(hitPosition))
                {
                    return ship;
                }
            }
            return null;
        }

        /// <summary>
        /// Обработчик события таймера хода компьютера
        /// </summary>
        private void ComputerTimer_Tick(object sender, EventArgs e)
        {
            computerTimer.Stop();
            DoComputerTurn();
        }

        /// <summary>
        /// Выполняет ход компьютера
        /// </summary>
        private void DoComputerTurn()
        {
            Position target = computerAI.GetNextTarget(myBoard);
            BoardCellState result = gameController.ProcessComputerShot(target);

            Button targetButton = myButtons[target.Row, target.Column];

            if (result == BoardCellState.Hit || result == BoardCellState.Sunk)
            {
                if (result == BoardCellState.Sunk)
                {
                    targetButton.BackColor = Color.Red;
                    UpdateNeighboringCells(myBoard, myButtons, target);
                }
                else
                {
                    targetButton.BackColor = Color.OrangeRed;
                }

                computerAI.ProcessHitResult(target, result, myBoard);
                computerTimer.Start();
            }
            else
            {
                targetButton.BackColor = Color.LightGray;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки меню
        /// </summary>
        private void MenuButton_Click(object sender, EventArgs e)
        {
            computerTimer.Stop();
            SetEnemyBoardEnabled(false);
            soundManager.StopBackgroundMusic();
            FormNavigator.NavigateToForm(this, new ModeForm());
        }

        /// <summary>
        /// Обработчик закрытия формы
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (soundManager != null)
            {
                soundManager.Dispose();
            }
        }
    }
}
