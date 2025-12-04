using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace BattleShip_WPF.Fonts
{
    /// <summary>
    /// Класс для загрузки и управления пользовательскими шрифтами
    /// </summary>
    public static class FontLoader
    {
        private static PrivateFontCollection privateFonts = new PrivateFontCollection();

        private const string FontFileName1 = "Fonts/Oi-Regular.ttf";
        private const string FontFileName2 = "Fonts/RubikMonoOne-Regular.ttf";

        /// <summary>
        /// Загружает пользовательские шрифты из файлов
        /// </summary>
        public static void LoadFont()
        {
            try
            {
                privateFonts.AddFontFile(FontFileName1);
                privateFonts.AddFontFile(FontFileName2);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке шрифта из файла: {ex.Message}", "Ошибка шрифта", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Получает шрифт по имени и размеру
        /// </summary>
        /// <param name="fontName">Название шрифта</param>
        /// <param name="size">Размер шрифта</param>
        /// <param name="style">Стиль шрифта</param>
        /// <returns>Объект шрифта</returns>
        public static Font GetFont(string fontName, float size, FontStyle style = FontStyle.Regular)
        {
            FontFamily fontFamily = privateFonts.Families.FirstOrDefault(f => f.Name == fontName);

            if (fontFamily != null)
            {
                return new Font(fontFamily, size, style);
            }

            return new Font("Arial", size, style);
        }
    }
}
