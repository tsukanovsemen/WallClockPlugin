namespace WallClockPlugin.Model
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Класс для хранения и проверки параметров.
    /// </summary>
    public class WallClockParameters
    {
        /// <summary>
        /// Ширина рисок часов.
        /// </summary>
        private const float CLOCKS_MARK_WIDTH = 15.0f;

        /// <summary>
        /// Минимальное значение радиуса циферблата.
        /// </summary>
        private const int MIN_RADIUS = 100;

        /// <summary>
        /// Максимальное значение радиуса циферблата.
        /// </summary>
        private const int MAX_RADIUS = 200;

        /// <summary>
        /// Минимальное значение ширины бортика.
        /// </summary>
        private const int MIN_SIDE_WIDTH = 30;

        /// <summary>
        /// Максимальное значение ширины бортика.
        /// </summary>
        private const int MAX_SIDE_WIDTH = 60;

        /// <summary>
        /// Минимальное значение высоты бортика.
        /// </summary>
        private const int MIN_SIDE_HEIGHT = 20;

        /// <summary>
        /// Максимальное значение высоты бортика.
        /// </summary>
        private const int MAX_SIDE_HEIGHT = 40;

        /// <summary>
        /// Минимальный размер радиуса выреза (узора).
        /// </summary>
        private const int MIN_CUT_RADIUS = 25;

        /// <summary>
        /// Минимальное количество вырезов.
        /// </summary>
        private const int MIN_CUTS_COUNT = 2;

        /// <summary>
        /// Радиус циферблата.
        /// </summary>
        private float _radius = MIN_RADIUS;

        /// <summary>
        /// Ширина бортика.
        /// </summary>
        private float _sideWidth = MIN_SIDE_WIDTH;

        /// <summary>
        /// Высота бортика.
        /// </summary>
        private float _sideHeight = MIN_SIDE_HEIGHT;

        /// <summary>
        /// Длина минутной стрелки.
        /// </summary>
        private float _minuteHandLength;

        /// <summary>
        /// Длина часовой стрелки.
        /// </summary>
        private float _hourHandLength;

        /// <summary>
        /// Радиус выреза.
        /// </summary>
        private float _cutRadius = MIN_CUT_RADIUS;

        /// <summary>
        /// Количество вырезов.
        /// </summary>
        private int _cutsCount = MIN_CUTS_COUNT;

        /// <summary>
        /// Создает объект класса.
        /// </summary>
        public WallClockParameters()
        {
            MinuteHandLength = MaxMinuteHandLength();
            HourHandLength = MaxHourHandLength();
        }

        /// <summary>
        /// Радиус циферблата.
        /// </summary>
        public float Radius
        {
            get => _radius;
            set
            {
                if (!Validator.ValidateRange(MIN_RADIUS, MAX_RADIUS, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{MIN_RADIUS};{MAX_RADIUS}]");
                }

                _radius = value;
            }
        }

        /// <summary>
        /// Ширина бортика.
        /// </summary>
        public float SideWidth
        {
            get => _sideWidth;
            set
            {
                if (!Validator.ValidateRange(MIN_SIDE_WIDTH, MAX_SIDE_WIDTH, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{MIN_SIDE_WIDTH};{MAX_SIDE_WIDTH}].");
                }

                _sideWidth = value;
            }
        }

        /// <summary>
        /// Высота бортика.
        /// </summary>
        public float SideHeight
        {
            get => _sideHeight;
            set
            {
                if (!Validator.ValidateRange(MIN_SIDE_HEIGHT, MAX_SIDE_HEIGHT, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{MIN_SIDE_HEIGHT};{MAX_SIDE_HEIGHT}].");
                }

                _sideHeight = value;
            }
        }

        /// <summary>
        /// Длина минутной стрелки.
        /// </summary>
        public float MinuteHandLength
        {
            get => _minuteHandLength;
            set
            {
                var minValue = MinMinuteHandLength();
                var maxValue = MaxMinuteHandLength();

                if (!Validator.ValidateRange(minValue, maxValue, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{minValue};{maxValue}].");
                }

                _minuteHandLength = value;
            }
        }

        /// <summary>
        /// Длина часовой стрелки.
        /// </summary>
        public float HourHandLength
        {
            get => _hourHandLength;
            set
            {
                var minValue = MinHourHandLength();
                var maxValue = MaxHourHandLength();

                if (!Validator.ValidateRange(minValue, maxValue, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{minValue};{maxValue}].");
                }

                _hourHandLength = value;
            }
        }

        /// <summary>
        /// Состояние - отображать только часы.
        /// </summary>
        public bool OnlyHours { get; set; } = false;

        /// <summary>
        /// Радиус выреза.
        /// </summary>
        public float CutRadius
        {
            get => _cutRadius;
            set
            {
                var minValue = MIN_CUT_RADIUS;
                var maxValue = SideWidth - 5;

                if (!Validator.ValidateRange(minValue, maxValue, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{minValue};{maxValue}].");
                }

                _cutRadius = value;
            }
        }

        /// <summary>
        /// Количество вырезов.
        /// </summary>
        public int CutsCount
        {
            get => _cutsCount;
            set
            {
                var minValue = MIN_CUTS_COUNT;
                var maxValue = MaxCutsCount();

                if (!Validator.ValidateRange(minValue, maxValue, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{minValue};{maxValue}].");
                }

                _cutsCount = value;
            }
        }

        /// <summary>
        /// Состояние - отображать вырезы бортика.
        /// </summary>
        public bool SideCuts { get; set; } = false;

        /// <summary>
        /// Возвращает минимальный радиус циферблата.
        /// </summary>
        /// <returns>Минимальный радиус циферблата.</returns>
        public int MinRadius()
        {
            return MIN_RADIUS;
        }

        /// <summary>
        /// Возвращает максимальный радиус циферблата.
        /// </summary>
        /// <returns>Максимальный радиус циферблата.</returns>
        public int MaxRadius()
        {
            return MAX_RADIUS;
        }

        /// <summary>
        /// Возвращает минимальную ширину бортика.
        /// </summary>
        /// <returns>Минимальная ширина бортика.</returns>
        public int MinSideWidth()
        {
            return MIN_SIDE_WIDTH;
        }

        /// <summary>
        /// Возвращает максимальную ширину бортика.
        /// </summary>
        /// <returns>Максимальная ширина бортика.</returns>
        public int MaxSideWidth()
        {
            return MAX_SIDE_WIDTH;
        }

        /// <summary>
        /// Возвращает минимальную высотку бортика.
        /// </summary>
        /// <returns>Минимальная высота бортика.</returns>
        public int MinSideHeight()
        {
            return MIN_SIDE_HEIGHT;
        }

        /// <summary>
        /// Возвращает максимальную высоту бортика.
        /// </summary>
        /// <returns>Максимальная высота бортика.</returns>
        public int MaxSideHeight()
        {
            return MAX_SIDE_HEIGHT;
        }

        /// <summary>
        /// Ширина рисок часов.
        /// </summary>
        /// <returns>Возвращает ширину рисок часов.</returns>
        public float ClocksMarkWidth()
        {
            return CLOCKS_MARK_WIDTH;
        }

        /// <summary>
        /// Длина риски часов, обозначающих часы.
        /// </summary>
        /// <returns>Возвращает длину риски часов, обозначающих часы.</returns>
        public float ClocksHoursMarkLength()
        {
            return Radius * 0.2f;
        }

        /// <summary>
        /// Длина риски часов, обозначающих минуты.
        /// </summary>
        /// <returns>Возвращает длину риски часов, обозначающих минуты.</returns>
        public float ClocksMinutesMarkLength()
        {
            return Radius * 0.1f;
        }

        /// <summary>
        /// Высота риски часов.
        /// </summary>
        /// <returns>Возвращает высоту риски часов.</returns>
        public float ClocksMarkHeight()
        {
            return SideHeight / 6.0f;
        }

        /// <summary>
        /// Метод возвращает минимальное значение длины минутной стрелки.
        /// </summary>
        /// <returns>Минимальное значение длины минутной стрелки.</returns>
        public float MinMinuteHandLength()
        {
            return (Radius / 2) + 4;
        }

        /// <summary>
        /// Метод возвращает минимальное значение длины часовой стрелки.
        /// </summary>
        /// <returns>Минимальное значение длины часовой стрелки.</returns>
        public float MinHourHandLength()
        {
            return Radius / 5;
        }

        /// <summary>
        /// Метод возвращает максимальное значение длины минутной стрелки.
        /// </summary>
        /// <returns>Максимальное значение длины минутной стрелки.</returns>
        public float MaxMinuteHandLength()
        {
            return _radius * 0.55f;
        }

        /// <summary>
        /// Метод возвращает максимальное значение длины часовой стрелки.
        /// </summary>
        /// <returns>Максимальное значение длины часовой стрелки.</returns>
        public float MaxHourHandLength()
        {
            return _radius * 0.3f;
        }

        /// <summary>
        /// Возвращает максимальное количество вырезов.
        /// </summary>
        /// <returns>Максимальное количество вырезов.</returns>
        public int MaxCutsCount()
        {
            return (int)(Math.PI / Math.Asin(CutRadius / (Radius + SideWidth)));
        }

        /// <summary>
        /// Возвращает минимальное количество вырезов.
        /// </summary>
        /// <returns>Минимальное количество вырезов.</returns>
        public int MinCutsCount()
        {
            return MIN_CUTS_COUNT;
        }

        /// <summary>
        /// Возвращает максимальный радиус выреза.
        /// </summary>
        /// <returns>Максимальный радиус выреза.</returns>
        public float MaxCutRadius()
        {
            return SideWidth - 5;
        }

        /// <summary>
        /// Возвращает минимальный радиус выреза.
        /// </summary>
        /// <returns>Минимальный радиус выреза.</returns>
        public float MinCutRadius()
        {
            return MIN_CUT_RADIUS;
        }

        /// <summary>
        /// Устанавливает значения по умолчанию.
        /// </summary>
        public void SetDefaultParameters()
        {
            Radius = MIN_RADIUS;
            SideWidth = MIN_SIDE_WIDTH;
            SideHeight = MIN_SIDE_HEIGHT;
            MinuteHandLength = MinMinuteHandLength();
            HourHandLength = MinHourHandLength();
            SideCuts = false;
            CutRadius = MIN_CUT_RADIUS;
            CutsCount = MIN_CUTS_COUNT;
        }
    }
}