namespace WallClockPlugin.View
{
    using System.Drawing;

    /// <summary>
    /// Класс с цветами приложения.
    /// </summary>
    public class ColorsWallClockPlugin
    {
        /// <summary>
        /// Цвет ошибки работы приложения.
        /// </summary>
        public static Color COLOR_ERROR { get; private set; } = Color.LightPink;

        /// <summary>
        /// Цвет правильной работы приложения.
        /// </summary>
        public static Color COLOR_CORRECTLY { get; private set; } = Color.White;
    }
}