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
    public static class FontLoader
    {
        private static PrivateFontCollection privateFonts = new PrivateFontCollection();

        private const string FontFileName1 = "Fonts/Oi-Regular.ttf";
        private const string FontFileName2 = "Fonts/RubikMonoOne-Regular.ttf";


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
