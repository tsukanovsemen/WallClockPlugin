namespace WallClockPlugin.Model
{
    /// <summary>
    /// Класс для валидации данных
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Валидация входа значения в определенный диапазон [left;right]
        /// </summary>
        /// <param name="leftBorder"> Левая граница диапазона</param>
        /// <param name="rightBorder"> Правая граница диапазона</param>
        /// <param name="value"> Значение</param>
        /// <returns> True - если значение входит в диапазон, false - если нет</returns>
        public static bool ValidateRange(float leftBorder, float rightBorder, float value)
        {
            return value >= leftBorder && value <= rightBorder;
        }

        /// <summary>
        /// Проверка на положительность значения
        /// </summary>
        /// <param name="value"> Значение</param>
        /// <returns> True - если значение положительное или равное 0,
        /// false - если значение отрицательное</returns>
        public static bool CheckPositiveValue(float value)
        {
            return value >= 0;
        }
    }
}
