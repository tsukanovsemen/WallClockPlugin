namespace WallClockPlugin.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Класс для построения модели часов.
    /// </summary>
    public class WallClockBuilder
    {
        /// <summary>
        /// Создание объекта и инициализация всех операций построения.
        /// </summary>
        public WallClockBuilder()
        {
            BuildOperations = GetAllBuildOperations();
        }

        /// <summary>
        /// Обертка над api SolidWorks.
        /// </summary>
        public ICADWrapper Wrapper { get; private set; } = new SolidWorksWrapper();

        /// <summary>
        /// Коллекция операций построения.
        /// </summary>
        public Dictionary<WallClockBuildOperations, string> BuildOperations { get; private set; }

        /// <summary>
        /// X координата центра, относительно которого строится объект.
        /// </summary>
        public float XCenter { get; set; } = 0.0f;

        /// <summary>
        /// Y координата центра, относительно которого строится объект.
        /// </summary>
        public float YCenter { get; set; } = 0.0f;

        /// <summary>
        /// Z координата центра, относительно которого строится объект.
        /// </summary>
        public float ZCenter { get; set; } = 0.0f;

        /// <summary>
        /// Построение детали по входным параметрам.
        /// </summary>
        /// <param name="parameters">Параметры детали.</param>
        public void Build(WallClockParameters parameters)
        {
            Wrapper.RunCAD();
            Wrapper.CreateNewDocument();

            BuildClockFrame(parameters.Radius, parameters.SideWidth, parameters.SideHeight);

            Wrapper.CreateAxisLine(BuildOperations[WallClockBuildOperations.CreateAxisLine]);

            BuildHoursMarks(
                parameters.Radius,
                parameters.ClocksMarkWidth(),
                parameters.ClocksHoursMarkLength(),
                parameters.ClocksMarkHeight());

            if (!parameters.OnlyHours)
            {
                BuildMinutesMarks(
                    parameters.Radius,
                    parameters.ClocksMarkWidth(),
                    parameters.ClocksMinutesMarkLength(),
                    parameters.ClocksMarkHeight());
            }

            var widthHand = 15;

            BuildHourAndMinuteHands(
                parameters.HourHandLength,
                parameters.MinuteHandLength,
                widthHand);

            if (parameters.SideCuts)
            {
                BuildSideCuts(
                    parameters.CutRadius,
                    parameters.CutsCount,
                    parameters.Radius + parameters.SideWidth,
                    parameters.SideHeight);
            }

            Wrapper.ShowTrimetry();
        }

        /// <summary>
        /// Построение формы часов.
        /// </summary>
        /// <param name="radius">Радиус часов.</param>
        /// <param name="sideWidth">Ширина бортика.</param>
        /// <param name="sideHeight">Высота бортика.</param>
        private void BuildClockFrame(float radius, float sideWidth, float sideHeight)
        {
            Wrapper.CreateCircleSketch(
                radius + sideWidth,
                XCenter,
                YCenter,
                ZCenter,
                BuildOperations[WallClockBuildOperations.CreateCircleForClock]);

            Wrapper.ExtrudePart(
                sideHeight,
                BuildOperations[WallClockBuildOperations.ExtrudeCircleForClock],
                false);

            Wrapper.CreateCircleSketch(
                radius,
                XCenter,
                YCenter,
                ZCenter,
                BuildOperations[WallClockBuildOperations.CreateCircleForClockFace]);

            Wrapper.CutPart(
                sideHeight / 2,
                BuildOperations[WallClockBuildOperations.CutCircleForClockFace]);
        }

        /// <summary>
        /// Построение рисок часов.
        /// </summary>
        /// <param name="radius">Радиус циферблата.</param>
        /// <param name="clocksMarkWidth">Ширина рисок.</param>
        /// <param name="clocksHoursMarkLength">Длина рисок.</param>
        /// <param name="clocksMarkHeight">Высота рисок.</param>
        private void BuildHoursMarks(
            float radius,
            float clocksMarkWidth,
            float clocksHoursMarkLength,
            float clocksMarkHeight)
        {
            var yMarkCenter = radius * 0.7f;

            Wrapper.CreateRectangleSketch(
                clocksMarkWidth,
                clocksHoursMarkLength,
                BuildOperations[WallClockBuildOperations.CreateRectangleForHoursScale],
                0,
                yMarkCenter,
                0);

            Wrapper.ExtrudePart(
                clocksMarkHeight,
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleHours]);

            var countMarks = 12;
            var angleBeetweenMarks = 360 / countMarks;

            Wrapper.CreateCircularArray(
                countMarks,
                angleBeetweenMarks,
                BuildOperations[WallClockBuildOperations.CreateCircleArrayHours],
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleHours],
                BuildOperations[WallClockBuildOperations.CreateAxisLine]);
        }

        /// <summary>
        /// Построение рисок минут.
        /// </summary>
        /// <param name="radius">Радиус циферблата.</param>
        /// <param name="clocksMarkWidth">Ширина риски.</param>
        /// <param name="clocksMinutesMarkLength">Длина рисок.</param>
        /// <param name="clocksMarkHeight">Высота рисок.</param>
        private void BuildMinutesMarks(
            float radius,
            float clocksMarkWidth,
            float clocksMinutesMarkLength,
            float clocksMarkHeight)
        {
            var yMarkCenter = radius * 0.9f;

            Wrapper.CreateRectangleSketch(
                clocksMarkWidth,
                clocksMinutesMarkLength,
                BuildOperations[WallClockBuildOperations.CreateRectangleForMinutesScale],
                0,
                yMarkCenter,
                0);

            Wrapper.ExtrudePart(
                clocksMarkHeight,
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleMinutes]);

            var countMarks = 60;
            var angleBeetweenMarks = 360 / countMarks;

            Wrapper.CreateCircularArray(
                countMarks,
                angleBeetweenMarks,
                BuildOperations[WallClockBuildOperations.CreateCircleArrayMinutes],
                BuildOperations[WallClockBuildOperations.ExtrudeRectangleScaleMinutes],
                BuildOperations[WallClockBuildOperations.CreateAxisLine]);
        }

        /// <summary>
        /// Построение стрелок часов и минут.
        /// </summary>
        /// <param name="hourHandLength">Длина часовой стрелки.</param>
        /// <param name="minuteHandLength">Длина минутной стрелки.</param>
        /// <param name="widthHand">Ширина стрелки.</param>
        private void BuildHourAndMinuteHands(
            float hourHandLength,
            float minuteHandLength,
            float widthHand)
        {
            Wrapper.CreateCircleSketch(
                10,
                XCenter,
                YCenter,
                ZCenter,
                BuildOperations[WallClockBuildOperations.CreateCentralCircleForHands]);

            Wrapper.ExtrudePart(
                10,
                BuildOperations[WallClockBuildOperations.ExtrudeCentralCircleForHands]);

            var agnleInOneMinute = 360 / 60;
            var angleInOneHour = 360 / 12;

            var angleMinute = DateTime.Now.Minute * agnleInOneMinute;
            var angleHour = DateTime.Now.Hour * angleInOneHour;

            BuildHourHand(hourHandLength, widthHand, angleHour);
            BuildMinuteHand(minuteHandLength, widthHand, angleMinute);
        }

        /// <summary>
        /// Построение стрелки минут.
        /// </summary>
        /// <param name="length">Длина стрелки минут.</param>
        /// <param name="width">Ширина стрелки минут.</param>
        /// <param name="angle">Угол поворота стрелки минут.</param>
        private void BuildMinuteHand(float length, float width, float angle)
        {
            Wrapper.CreateRhombusSketch(length, width, XCenter, YCenter, ZCenter, angle);
            var extrusionDepth = 3;
            Wrapper.ExtrudePart(extrusionDepth, "Выдавливание стрелки минуты");
        }

        /// <summary>
        /// Построение стрелки часов.
        /// </summary>
        /// <param name="length">Длина стрелки часов.</param>
        /// <param name="width">Ширина стрелки часов.</param>
        /// <param name="angle">Угол поворота стрелки часов.</param>
        private void BuildHourHand(float length, float width, float angle)
        {
            Wrapper.CreateRhombusSketch(length, width, XCenter, YCenter, ZCenter, angle);
            var extrusionDepth = 10;
            Wrapper.ExtrudePart(extrusionDepth, "Выдавливание стрелки часов");
            Wrapper.CreateRhombusSketch(length, width, XCenter, YCenter, ZCenter, angle);
            var cutDepth = 5;
            Wrapper.CutPart(cutDepth, "Вырез стрелки часов");
        }

        /// <summary>
        /// Построение вырезов бортика.
        /// </summary>
        /// <param name="cutRadius">Радиус выреза.</param>
        /// <param name="cutsCount">Количество вырезов.</param>
        /// <param name="clockRadius">Радиус часов.</param>
        /// <param name="cutDepth">Глубина выреза.</param>
        private void BuildSideCuts(
            float cutRadius,
            int cutsCount,
            float clockRadius,
            float cutDepth)
        {
            var x = 0;
            var y = clockRadius;
            var z = 0;

            Wrapper.CreateCircleSketch(
                cutRadius,
                x,
                y,
                z,
                BuildOperations[WallClockBuildOperations.CreateSideCutSketch]);

            Wrapper.CutPart(
                cutDepth,
                BuildOperations[WallClockBuildOperations.CutSideCutCircle]);

            float angleBeetweenCuts = 360.0f / (float)cutsCount;

            Wrapper.CreateCircularArray(
                cutsCount,
                angleBeetweenCuts,
                BuildOperations[WallClockBuildOperations.CreateCirckeArraySideCuts],
                BuildOperations[WallClockBuildOperations.CutSideCutCircle],
                BuildOperations[WallClockBuildOperations.CreateAxisLine]);
        }

        /// <summary>
        /// Инициализирует и возвращает все необходимые операции построения.
        /// </summary>
        /// <returns>Коллекцию операций построения.</returns>
        private Dictionary<WallClockBuildOperations, string> GetAllBuildOperations()
        {
            var operations = new Dictionary<WallClockBuildOperations, string>();

            operations.Add(
                WallClockBuildOperations.CreateCircleForClock,
                "Создание окружности для часов");

            operations.Add(
                WallClockBuildOperations.CreateCircleForClockFace,
                "Создание окружности для циферблата");

            operations.Add(
                WallClockBuildOperations.ExtrudeCircleForClock,
                "Выдавливание окружности для часов");

            operations.Add(
                WallClockBuildOperations.CutCircleForClockFace,
                "Вырез окружности для циферблата");

            operations.Add(
                WallClockBuildOperations.CreateAxisLine,
                "Создание осевой лини");

            operations.Add(
                WallClockBuildOperations.CreateRectangleForHoursScale,
                "Создание прямоугольника для шкалы часов");

            operations.Add(
                WallClockBuildOperations.CreateRectangleForMinutesScale,
                "Создание прямоугольника для шкалы минут");

            operations.Add(
                WallClockBuildOperations.ExtrudeRectangleScaleMinutes,
                "Выдавливание прямоугольника шкалы минут");

            operations.Add(
                WallClockBuildOperations.ExtrudeRectangleScaleHours,
                "Выдавливание прямоугольника шкалы часов");

            operations.Add(
                WallClockBuildOperations.CreateCircleArrayHours,
                "Создание массива шкалы часов");

            operations.Add(
                WallClockBuildOperations.CreateCircleArrayMinutes,
                "Создание массива шкалы минут");

            operations.Add(
                WallClockBuildOperations.CreateCentralCircleForHands,
                "Создание центральной окружности для стрелок часов");

            operations.Add(
                WallClockBuildOperations.ExtrudeCentralCircleForHands,
                "Выдавливание центральной окружности для стрелок часов");

            operations.Add(
                WallClockBuildOperations.CreateSideCutSketch,
                "Создание эскиза выреза бортика");

            operations.Add(
                WallClockBuildOperations.CutSideCutCircle,
                "Вырез узора бортика");

            operations.Add(
                WallClockBuildOperations.CreateCirckeArraySideCuts,
                "Создание кругового массивы вырезов бортика");

            return operations;
        }
    }
}