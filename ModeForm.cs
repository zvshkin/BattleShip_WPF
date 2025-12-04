using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;
using BattleShip_WPF.Controllers;
using BattleShip_WPF.UI;
using System;
using System.Windows.Forms;

namespace BattleShip_WPF
{
    /// <summary>
    /// Форма выбора режима игры
    /// </summary>
    public partial class ModeForm : Form
    {
        /// <summary>
        /// Конструктор формы выбора режима
        /// </summary>
        public ModeForm()
        {
            InitializeComponent();
            FontLoader.LoadFont();

            string oiFontName = "Oi";
            string rubikFontName = "Rubik Mono One";

            label1.Font = FontLoader.GetFont(oiFontName, 42);
            ClassicButton.Font = FontLoader.GetFont(rubikFontName, 20);
            FastButton.Font = FontLoader.GetFont(rubikFontName, 20);
            CreateButton.Font = FontLoader.GetFont(rubikFontName, 20);
            label2.Font = FontLoader.GetFont(rubikFontName, 18);
            label3.Font = FontLoader.GetFont(rubikFontName, 18);
            label4.Font = FontLoader.GetFont(rubikFontName, 18);
            BackButton.Font = FontLoader.GetFont(rubikFontName, 20);
        }

        /// <summary>
        /// Обработчик нажатия кнопки назад
        /// </summary>
        private void BackButton_Click(object sender, EventArgs e)
        {
            FormNavigator.NavigateToForm(this, new MainForm());
        }

        /// <summary>
        /// Обработчик нажатия кнопки классического режима
        /// </summary>
        private void ClassicButton_Click(object sender, EventArgs e)
        {
            GameBoard autoBoard = GameFactory.CreatePlayerBoard(false);
            FormNavigator.NavigateToForm(this, new GameForm(autoBoard, false));
        }

        /// <summary>
        /// Обработчик нажатия кнопки быстрого режима
        /// </summary>
        private void FastButton_Click(object sender, EventArgs e)
        {
            GameBoard autoBoard = GameFactory.CreatePlayerBoard(true);
            FormNavigator.NavigateToForm(this, new GameForm(autoBoard, true));
        }

        /// <summary>
        /// Обработчик нажатия кнопки создания своей расстановки
        /// </summary>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            FormNavigator.NavigateToForm(this, new SetupForm());
        }
    }
}
