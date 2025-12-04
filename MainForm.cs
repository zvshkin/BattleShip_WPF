using BattleShip_WPF.Fonts;
using BattleShip_WPF.UI;
using System;
using System.Windows.Forms;

namespace BattleShip_WPF
{
    /// <summary>
    /// Главная форма приложения
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            FontLoader.LoadFont();

            string oiFontName = "Oi";
            string rubikFontName = "Rubik Mono One";

            label1.Font = FontLoader.GetFont(oiFontName, 42);
            ModeButton.Font = FontLoader.GetFont(rubikFontName, 20);
            ExitButton.Font = FontLoader.GetFont(rubikFontName, 20);
        }

        /// <summary>
        /// Обработчик нажатия кнопки выбора режима
        /// </summary>
        private void ModeButton_Click(object sender, EventArgs e)
        {
            FormNavigator.NavigateToForm(this, new ModeForm());
        }

        /// <summary>
        /// Обработчик нажатия кнопки выхода
        /// </summary>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
