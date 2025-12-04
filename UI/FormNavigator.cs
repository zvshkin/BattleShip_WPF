using System;
using System.Windows.Forms;

namespace BattleShip_WPF.UI
{
    /// <summary>
    /// Класс для навигации между формами приложения
    /// </summary>
    public static class FormNavigator
    {
        /// <summary>
        /// Переходит с текущей формы на целевую форму
        /// </summary>
        /// <param name="currentForm">Текущая форма</param>
        /// <param name="targetForm">Целевая форма для перехода</param>
        public static void NavigateToForm(Form currentForm, Form targetForm)
        {
            targetForm.FormClosed += (s, args) => Application.Exit();
            targetForm.Show();
            currentForm.Hide();
        }
    }
}
