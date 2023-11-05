﻿namespace WallClockPlugin.Model
{
    /// <summary>
    /// Интерфейс для обертки над САПР
    /// </summary>
    public interface ICADWrapper
    {
        /// <summary>
        /// Запуск САПР
        /// </summary>
        void RunCAD();

        /// <summary>
        /// Создание нового документа
        /// </summary>
        void CreateNewDocument();

        /// <summary>
        /// Создание эскиза круга
        /// </summary>
        /// <param name="radius">Радиус круга</param>
        /// <param name="xc">Координата x центра</param>
        /// <param name="yc">Координата y центра</param>
        /// <param name="zc">Координата z центра</param>
        /// <param name="operationName">Название операции</param>
        void CreateCircleSketch(float radius, float xc, float yc, float zc, string operationName);

        /// <summary>
        /// Выдавливание детали
        /// </summary>
        /// <param name="extrusionDepth">Глубина выдавливания</param>
        /// <param name="operationName">Название операции</param>
        /// <param name="oneSide">Выдавливание в двух или одном направлении</param>
        void ExtrudePart(float extrusionDepth, string operationName, bool oneSide = true);

        /// <summary>
        /// Вырезание детали
        /// </summary>
        /// <param name="cutoutDepth">Глубина выреза</param>
        /// <param name="operationName">Название операции</param>
        /// <param name="oneSide">Вырезание в двух или одном направлении</param>
        void CutPart(float cutoutDepth, string operationName, bool oneSide = true);

        /// <summary>
        /// Создание эскиза прямоугольника
        /// </summary>
        /// <param name="width">Ширина прямоугольника</param>
        /// <param name="height">Высота прямоугольника</param>
        /// <param name="operationName">Название операции</param>
        /// <param name="xc">Координата x центра</param>
        /// <param name="yc">Координата y центра</param>
        /// <param name="zc">Координата z центра</param>
        void CreateRectangleSketch(float width,
            float height, string operationName, float xc = 0, float yc = 0, float zc = 0);

        /// <summary>
        /// Создание кругового массива из операция
        /// </summary>
        /// <param name="count">Количество повторяющихся элементов</param>
        /// <param name="angle">Угол между элементами</param>
        /// <param name="operationName">Название операции</param>
        /// <param name="repetitiveOperationName">Название повторяющейся операции</param>
        /// <param name="axisName">Название оси</param>
        void CreateCircularArray(int count, float angle, string operationName, 
            string repetitiveOperationName, string axisName);

        /// <summary>
        /// Создание оси
        /// </summary>
        /// <param name="operationName">Название операции</param>
        void CreateAxisLine(string operationName);

    }
}