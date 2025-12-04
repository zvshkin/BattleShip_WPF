using System;
using System.Drawing;
using System.Windows.Forms;
using BattleShip_WPF.Fonts;
using BattleShip_WPF.Logic;

namespace BattleShip_WPF.UI
{
    /// <summary>
    /// Класс для создания и отрисовки игровых досок
    /// </summary>
    public static class BoardRenderer
    {
        /// <summary>
        /// Создает массив кнопок для игровой доски
        /// </summary>
        /// <param name="parentPanel">Панель, на которой размещаются кнопки</param>
        /// <param name="boardData">Данные игровой доски</param>
        /// <param name="boardSize">Размер доски</param>
        /// <param name="isClickable">Можно ли кликать по кнопкам</param>
        /// <param name="cellSize">Размер одной ячейки</param>
        /// <param name="clickHandler">Обработчик клика по кнопке</param>
        /// <returns>Массив кнопок</returns>
        public static Button[,] CreateBoardButtons(Panel parentPanel, GameBoard boardData, int boardSize, bool isClickable, int cellSize, EventHandler clickHandler = null)
        {
            Button[,] btns = new Button[boardSize, boardSize];

            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    Color baseColor = Color.FromArgb(171, 169, 169);
                    if (!isClickable && boardData.Grid[r, c] == BoardCellState.Ship)
                    {
                        baseColor = Color.FromArgb(82, 82, 82);
                    }

                    Button btn = new Button
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point((c + 1) * cellSize, (r + 1) * cellSize),
                        Tag = new Position(r, c),
                        BackColor = baseColor,
                        FlatStyle = FlatStyle.Flat,
                        Enabled = isClickable
                    };

                    StyleButton(btn);

                    if (isClickable && clickHandler != null)
                    {
                        btn.Click += clickHandler;
                    }

                    btns[r, c] = btn;
                    parentPanel.Controls.Add(btn);
                }
            }

            return btns;
        }

        /// <summary>
        /// Применяет стиль к кнопке (границы, эффекты наведения)
        /// </summary>
        /// <param name="btn">Кнопка для стилизации</param>
        public static void StyleButton(Button btn)
        {
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(210, 210, 210);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(170, 170, 170);
        }

        /// <summary>
        /// Создает стилизованную кнопку для доски
        /// </summary>
        /// <param name="cellSize">Размер ячейки</param>
        /// <param name="pos">Позиция на доске</param>
        /// <param name="backColor">Цвет фона</param>
        /// <param name="enabled">Включена ли кнопка</param>
        /// <returns>Созданная кнопка</returns>
        public static Button CreateStyledButton(int cellSize, Position pos, Color backColor, bool enabled = true)
        {
            Button btn = new Button
            {
                Size = new Size(cellSize, cellSize),
                Location = new Point(pos.Column * cellSize, pos.Row * cellSize),
                Tag = pos,
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                Enabled = enabled
            };

            StyleButton(btn);
            return btn;
        }

        /// <summary>
        /// Рисует метки осей (номера строк и буквы столбцов) на доске
        /// </summary>
        /// <param name="parentPanel">Родительская панель</param>
        /// <param name="size">Размер доски</param>
        /// <param name="cellSize">Размер ячейки</param>
        public static void DrawAxisLabels(Panel parentPanel, int size, int cellSize)
        {
            // Рисуем номера строк
            for (int r = 0; r < size; r++)
            {
                Label rowLabel = new Label
                {
                    Text = (r + 1).ToString(),
                    Font = FontLoader.GetFont("Rubik Mono One", 8),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(0, (r + 1) * cellSize),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                parentPanel.Controls.Add(rowLabel);
            }

            // Рисуем буквы столбцов
            string colLetters = "АБВГДЕЖЗИК".Substring(0, size);
            for (int c = 0; c < size; c++)
            {
                Label colLabel = new Label
                {
                    Text = colLetters[c].ToString(),
                    Font = FontLoader.GetFont("Rubik Mono One", 8),
                    Size = new Size(cellSize, cellSize),
                    Location = new Point((c + 1) * cellSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                parentPanel.Controls.Add(colLabel);
            }
        }

        /// <summary>
        /// Создает панель для игровой доски
        /// </summary>
        /// <param name="boardSize">Размер доски</param>
        /// <param name="cellSize">Размер ячейки</param>
        /// <returns>Созданная панель</returns>
        public static Panel CreateBoardPanel(int boardSize, int cellSize)
        {
            int boardWidth = boardSize * cellSize + cellSize;
            int boardHeight = boardSize * cellSize + cellSize;

            Panel boardPanel = new Panel
            {
                Size = new Size(boardWidth, boardHeight),
                BackColor = Color.FromArgb(171, 169, 169),
                Anchor = AnchorStyles.Top
            };

            DrawAxisLabels(boardPanel, boardSize, cellSize);
            return boardPanel;
        }
    }
}
