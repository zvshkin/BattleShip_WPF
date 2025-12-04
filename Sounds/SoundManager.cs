using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace BattleShip_WPF.Sounds
{
    /// <summary>
    /// Класс для управления звуковым сопровождением игры
    /// </summary>
    public class SoundManager
    {
        private SoundPlayer backgroundMusic;
        private bool isMusicEnabled = true;
        private bool isSoundEnabled = true;

        private const string SoundsFolder = "Sounds";
        private const string BackgroundMusicFile = "background_music.wav";

        /// <summary>
        /// Получает или задает состояние музыки
        /// </summary>
        public bool IsMusicEnabled => isMusicEnabled;

        /// <summary>
        /// Получает или задает состояние звуковых эффектов
        /// </summary>
        public bool IsSoundEnabled => isSoundEnabled;

        /// <summary>
        /// Конструктор менеджера звуков
        /// </summary>
        public SoundManager()
        {
            InitializeSounds();
        }

        /// <summary>
        /// Инициализирует звуковые файлы
        /// </summary>
        private void InitializeSounds()
        {
            try
            {
                string musicPath = Path.Combine(SoundsFolder, BackgroundMusicFile);
                if (File.Exists(musicPath))
                {
                    backgroundMusic = new SoundPlayer(musicPath);
                    backgroundMusic.LoadAsync();
                }
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки загрузки музыки, если файл отсутствует
                System.Diagnostics.Debug.WriteLine($"Ошибка загрузки музыки: {ex.Message}");
            }
        }

        /// <summary>
        /// Включает или отключает фоновую музыку
        /// </summary>
        /// <param name="enabled">Включена ли музыка</param>
        public void SetMusicEnabled(bool enabled)
        {
            if (enabled == isMusicEnabled)
                return;

            isMusicEnabled = enabled;

            if (enabled)
            {
                PlayBackgroundMusic();
            }
            else
            {
                StopBackgroundMusic();
            }
        }

        /// <summary>
        /// Включает или отключает звуковые эффекты
        /// </summary>
        /// <param name="enabled">Включены ли звуки</param>
        public void SetSoundEnabled(bool enabled)
        {
            isSoundEnabled = enabled;
        }

        /// <summary>
        /// Воспроизводит фоновую музыку
        /// </summary>
        public void PlayBackgroundMusic()
        {
            if (isMusicEnabled && backgroundMusic != null)
            {
                try
                {
                    backgroundMusic.PlayLooping();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка воспроизведения музыки: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Останавливает фоновую музыку
        /// </summary>
        public void StopBackgroundMusic()
        {
            if (backgroundMusic != null)
            {
                try
                {
                    backgroundMusic.Stop();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Ошибка остановки музыки: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Освобождает ресурсы
        /// </summary>
        public void Dispose()
        {
            StopBackgroundMusic();
            if (backgroundMusic != null)
            {
                backgroundMusic.Dispose();
                backgroundMusic = null;
            }
        }
    }
}

