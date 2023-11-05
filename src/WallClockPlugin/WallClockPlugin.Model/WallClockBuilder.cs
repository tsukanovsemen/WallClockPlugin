using System;
using System.Collections.Generic;

namespace WallClockPlugin.Model
{
    /// <summary>
    /// Класс для построения модели часов
    /// </summary>
    public class WallClockBuilder
    {
        /// <summary>
        /// Обертка над api SolidWorks
        /// </summary>
        public ICADWrapper Wrapper { get; private set; } = new SolidWorksWrapper();

        /// <summary>
        /// Коллекция операций построения
        /// </summary>
        public Dictionary<WallClockBuildOperations, string> BuildOperations { get; private set; } 

        /// <summary>
        /// X координата центра, относительного которого строится объект
        /// </summary>
        public float XCenter { get; set; } = 0.0f;

        /// <summary>
        /// Y координата центра, относительного которого строится объект
        /// </summary>
        public float YCenter { get; set; } = 0.0f;

        /// <summary>
        /// Z координата центра, относительного которого строится объект
        /// </summary>
        public float ZCenter { get; set; } = 0.0f;

        /// <summary>
        /// Создание объекта и инициализация всех операций построения
        /// </summary>
        public WallClockBuilder()
        {
            BuildOperations = GetAllBuildOperations();
        }

        /// <summary>
        /// Построение детали по входным параметрам
        /// </summary>
        /// <param name="parameters">Параметры детали</param>
        public void Build(WallClockParameters parameters)
        {
            Wrapper.RunCAD();
            Wrapper.CreateNewDocument();

            BuildClockFrame(parameters.Radius, parameters.SideWidth, 
                parameters.SideHeight, parameters.FrameForm);

            BuildHoursAndMinutes(false, parameters.Radius, 
                parameters.ClocksMarkWidth(), parameters.ClocksHoursMarkLength(),
                parameters.ClocksMinutesMarkLength(), parameters.ClocksMarkHeight());

            BuildHourAndMinuteHands(parameters.HourHandLength, parameters.MinuteHandLength);
        }

        /// <summary>
        /// Построение формы часов
        /// </summary>
        /// <param name="radius">Радиус часов</param>
        /// <param name="sideWidth">Ширина бортика</param>
        /// <param name="sideHeight">Высота бортика</param>
        /// <param name="clockForm">Форма часов</param>
        private void BuildClockFrame(float radius, float sideWidth, 
            float sideHeight, ClockForm clockForm)
        {
            Wrapper.CreateCircleSketch(radius + sideWidth, 
                XCenter, YCenter, ZCenter, 
                BuildOperations[WallClockBuildOperations.CreateCircleForClock]);

            Wrapper.ExtrudePart(sideHeight, 
                BuildOperations[WallClockBuildOperations.ExtrudeCircleForClock], false);

            Wrapper.CreateCircleSketch( radius, XCenter, YCenter, ZCenter,
                BuildOperations[WallClockBuildOperations.CreateCircleForClockFace]);

            Wrapper.CutPart(sideHeight / 2, 
                BuildOperations[WallClockBuildOperations.CutCircleForClockFace]);
        }

        /// <summary>
        /// Построение рисок часов и минут
        /// </summary>
        /// <param name="onlyHours">Построение только часов</param>
        /// <param name="radius">Радиус циферблата</param>
        private void BuildHoursAndMinutes(bool onlyHours, float radius, float clocksMarkWidth,
            float clocksHoursMarkLength, float clocksMinutesMarkLength, float clocksMarkHeight)
        {
            var yMarkCenter = radius * 0.7f;
            Wrapper.CreateRectangleSketch(clocksMarkWidth, clocksHoursMarkLength,
                BuildOperations[WallClockBuildOperations.CreateRectangleForHoursScale],
                0, yMarkCenter, 0);

            Wrapper.ExtrudePart(clocksMarkHeight,
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleHours]);

            Wrapper.CreateAxisLine(BuildOperations[WallClockBuildOperations.CreateAxisLine]);

            Wrapper.CreateCircularArray(12, 30,
                BuildOperations[WallClockBuildOperations.CreateCircleArrayHours],
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleHours],
                BuildOperations[WallClockBuildOperations.CreateAxisLine]);

            if (!onlyHours)
            {
                yMarkCenter = radius * 0.9f;

                Wrapper.CreateRectangleSketch(clocksMarkWidth, clocksMinutesMarkLength,
                    BuildOperations[WallClockBuildOperations.CreateRectangleForMinutesScale],
                    0, yMarkCenter, 0);

                Wrapper.ExtrudePart(clocksMarkHeight,
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleMinutes]);

                Wrapper.CreateCircularArray(60, 6,
                BuildOperations[WallClockBuildOperations.CreateCircleArrayMinutes],
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleMinutes],
                BuildOperations[WallClockBuildOperations.CreateAxisLine]);
            }
        }

        private void BuildHourAndMinuteHands(float hourHandLength, float minuteHandeLength)
        {

        }

        /// <summary>
        /// Инициализирует и возвращает все необходимые операции построения
        /// </summary>
        /// <returns>Коллекцию операций построения</returns>
        private Dictionary<WallClockBuildOperations, string> GetAllBuildOperations()
        {
            var operations = new Dictionary<WallClockBuildOperations, string>();

            operations.Add(WallClockBuildOperations.CreateCircleForClock, 
                "Создание окружности для чаосв");

            operations.Add(WallClockBuildOperations.CreateCircleForClockFace, 
                "Создание окржности для циферблата");

            operations.Add(WallClockBuildOperations.ExtrudeCircleForClock, 
                "Выдавливание окружности для часов");

            operations.Add(WallClockBuildOperations.CutCircleForClockFace,
                "Вырез окружности для циферблата");

            operations.Add(WallClockBuildOperations.CreateAxisLine,
                "Создание осевой лини");

            operations.Add(WallClockBuildOperations.CreateRectangleForHoursScale,
                "Создание прямоугольника для шкалы часов");

            operations.Add(WallClockBuildOperations.CreateRectangleForMinutesScale,
                "Создание прямоугольника для шкалы минут");

            operations.Add(WallClockBuildOperations.ExtrudeRectangleScaleMinutes,
                "Выдавливание прямоугольника шкалы минут");

            operations.Add(WallClockBuildOperations.ExtrudeRectangleScaleHours,
                "Выдавливание прямоугольника шкалы часов");

            operations.Add(WallClockBuildOperations.CreateCircleArrayHours,
                "Создание массива шкалы часов");

            operations.Add(WallClockBuildOperations.CreateCircleArrayMinutes,
                "Создание массива шкалы минут");

            return operations;
        }
    }
}
