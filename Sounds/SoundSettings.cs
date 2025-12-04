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

namespace BattleShip_WPF.Sounds
{
    public partial class SoundSettings : Form
    {
        private SoundManager soundManager;
        private bool originalMusicEnabled;
        private bool originalSoundEnabled;


        public SoundSettings(SoundManager manager)
        {
            InitializeComponent();
            soundManager = manager;

            // Сохраняем исходные значения на случай отмены
            originalMusicEnabled = soundManager.IsMusicEnabled;
            originalSoundEnabled = soundManager.IsSoundEnabled;

            // Инициализируем чекбокс
            chkMusic.Checked = originalMusicEnabled;

            label1.Font = FontLoader.GetFont("Oi", 26);
            btnOK.Font = FontLoader.GetFont("Oi", 20);
            chkMusic.Font = FontLoader.GetFont("Rubik Mono One", 18);
        }

        private void chkMusic_CheckedChanged(object sender, System.EventArgs e)
        {
            soundManager.SetMusicEnabled(chkMusic.Checked);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            // Восстанавливаем настройки, если пользователь нажал "Отмена"
            soundManager.SetMusicEnabled(originalMusicEnabled);
            soundManager.SetSoundEnabled(originalSoundEnabled);
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            // Настройки уже применены в реальном времени — просто закрываем
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}