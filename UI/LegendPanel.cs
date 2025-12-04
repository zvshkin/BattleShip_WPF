using System.Drawing;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;

namespace BattleShip_WPF.UI
{
    /// <summary>
    /// Класс для создания панели легенды с описанием состояний ячеек
    /// </summary>
    public static class LegendPanel
    {
        /// <summary>
        /// Создает панель с легендой
        /// </summary>
        /// <returns>Созданная панель с легендой</returns>
        public static Panel CreateLegendPanel()
        {
            Panel statusPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(229, 229, 229)
            };

            TableLayoutPanel legendLayout = new TableLayoutPanel
            {
                RowCount = 5,
                ColumnCount = 2,
                Dock = DockStyle.Top,
                Location = new Point(0, 40),
                AutoSize = true,
                Anchor = AnchorStyles.Top
            };
            legendLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
            legendLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            AddLegendItem(legendLayout, Color.FromArgb(82, 82, 82), "Ваш корабль", 0);
            AddLegendItem(legendLayout, Color.FromArgb(171, 169, 169), "Пустая вода", 1);
            AddLegendItem(legendLayout, Color.LightGray, "Промах", 2);
            AddLegendItem(legendLayout, Color.OrangeRed, "Попадание", 3);
            AddLegendItem(legendLayout, Color.Red, "Потоплен", 4);

            statusPanel.Controls.Add(legendLayout);
            return statusPanel;
        }

        /// <summary>
        /// Добавляет элемент в легенду
        /// </summary>
        /// <param name="layout">Таблица для размещения</param>
        /// <param name="color">Цвет элемента</param>
        /// <param name="text">Текст описания</param>
        /// <param name="row">Номер строки</param>
        private static void AddLegendItem(TableLayoutPanel layout, Color color, string text, int row)
        {
            Panel colorSquare = new Panel 
            { 
                BackColor = color, 
                Size = new Size(20, 20), 
                Margin = new Padding(5) 
            };
            layout.Controls.Add(colorSquare, 0, row);

            Label textLabel = new Label
            {
                Text = text,
                Font = FontLoader.GetFont("Rubik Mono One", 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Margin = new Padding(5)
            };
            layout.Controls.Add(textLabel, 1, row);
        }
    }
}
