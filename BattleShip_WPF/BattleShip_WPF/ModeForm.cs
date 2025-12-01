using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;
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

        private void ClassicButton_Click(object sender, EventArgs e)
        {
            var ships = FleetGenerator.GenerateRandomFleet(10, FleetGenerator.ClassicFleet);
            GameBoard autoBoard = new GameBoard(10);
            foreach (var s in ships) autoBoard.PlaceShip(s);

            GameForm game = new GameForm(autoBoard, false);
            game.Show();
            this.Hide();
        }

        private void FastButton_Click(object sender, EventArgs e)
        {
            var ships = FleetGenerator.GenerateRandomFleet(8, FleetGenerator.FastFleet);
            GameBoard autoBoard = new GameBoard(8);
            foreach (var s in ships) autoBoard.PlaceShip(s);

            GameForm game = new GameForm(autoBoard, true);
            game.Show();
            this.Hide();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            SetupForm setup = new SetupForm();
            setup.Show();
            this.Hide();
        }
    }
}
