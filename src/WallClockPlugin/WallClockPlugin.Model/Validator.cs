namespace WallClockPlugin.Model
{
    /// <summary>
    /// Класс для проверки данных.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Проверка входа значения в определенный диапазон [left;right].
        /// </summary>
        /// <param name="leftBorder"> Левая граница диапазона.</param>
        /// <param name="rightBorder"> Правая граница диапазона.</param>
        /// <param name="value"> Значение.</param>
        /// <returns> True - если значение входит в диапазон, false - если нет.</returns>
        public static bool ValidateRange(float leftBorder, float rightBorder, float value)
        {
            return value >= leftBorder && value <= rightBorder;
        }
    }
}
