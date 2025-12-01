using BattleShip_WPF.Fonts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleShip_WPF
{
    public partial class ModeForm : Form
    {
        public ModeForm()
        {
            InitializeComponent();
            FontLoader.LoadFont();

            string oiFontName = "Oi";
            string rubikFontName = "Rubik Mono One";

            label1.Font = FontLoader.GetFont(oiFontName, 36);
            ClassicButton.Font = FontLoader.GetFont(rubikFontName, 20);
            FastButton.Font = FontLoader.GetFont(rubikFontName, 20);
            CreateButton.Font = FontLoader.GetFont(rubikFontName, 20);
            label2.Font = FontLoader.GetFont(rubikFontName, 20);
            label3.Font = FontLoader.GetFont(rubikFontName, 20);
            label4.Font = FontLoader.GetFont(rubikFontName, 20);
            BackButton.Font = FontLoader.GetFont(rubikFontName, 20);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.FormClosed += (s, args) => Application.Exit();
            mainForm.Show();
            this.Hide();
        }
    }
}
