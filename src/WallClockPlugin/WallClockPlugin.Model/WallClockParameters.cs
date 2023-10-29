﻿namespace WallClockPlugin.Model
{
    /// <summary>
    /// Класс для хранения и валидации параметров
    /// </summary>
    public class WallClockParameters
    {
        #region [Private]
        /// <summary>
        /// Минимальное значение радиуса циферблата
        /// </summary>
        private const int MIN_RADIUS = 100;

        /// <summary>
        /// Максимальное значение радиуса циферблата
        /// </summary>
        private const int MAX_RADIUS = 200;

        /// <summary>
        /// Минимальное значение ширины бортика
        /// </summary>
        private const int MIN_SIDE_WIDTH = 30;

        /// <summary>
        /// Максимальное значение ширины бортика
        /// </summary>
        private const int MAX_SIDE_WIDTH = 60;

        /// <summary>
        /// Минимальное значение высоты бортика
        /// </summary>
        private const int MIN_SIDE_HEIGHT = 20;

        /// <summary>
        /// Максимальное значение высоты бортика
        /// </summary>
        private const int MAX_SIDE_HEIGHT = 40;

        /// <summary>
        /// Радиус циферблата
        /// </summary>
        private float _radius;

        /// <summary>
        /// Ширина бортика
        /// </summary>
        private float _sideWidth;

        /// <summary>
        /// Высота борткиа 
        /// </summary>
        private float _sideHeight;

        /// <summary>
        /// Длина минутной стрелки
        /// </summary>
        private float _minuteHandLength;

        /// <summary>
        /// Длина часовой стрелки
        /// </summary>
        private float _hourHandLength;

        /// <summary>
        /// Состояние - отображать только часы
        /// </summary>
        private bool _onlyHours = false;

        /// <summary>
        /// Форма часов
        /// </summary>
        private ClockForm _frameForm = ClockForm.CircleForm;
        #endregion

        #region [Public]

        /// <summary>
        /// Радиус циферблата
        /// </summary>
        public float Radius 
        {
            get { return _radius; }

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
        /// Ширина бортика
        /// </summary>
        public float SideWidth
        {
            get { return _sideWidth; }

            set 
            {
                if(!Validator.ValidateRange(MIN_SIDE_WIDTH, MAX_SIDE_WIDTH, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{MIN_SIDE_WIDTH};{MAX_SIDE_WIDTH}]");
                }

                _sideWidth = value;
            }
        }

        /// <summary>
        /// Высота бортика
        /// </summary>
        public float SideHeight
        {
            get { return _sideHeight; }

            set 
            {
                if(!Validator.ValidateRange(MIN_SIDE_HEIGHT, MAX_SIDE_HEIGHT, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{MIN_SIDE_HEIGHT};{MAX_SIDE_HEIGHT}]");
                }

                _sideHeight = value;
            }
        }

        /// <summary>
        /// Длина минутной стрелки
        /// </summary>
        public float MinuteHandLength
        {
            get { return _minuteHandLength; }

            set 
            {
                var minValue = MinMinuteHandLength();
                var maxValue = MaxMinuteHandLength();

                if(!Validator.ValidateRange(minValue, maxValue, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{minValue};{maxValue}]");
                }

                _minuteHandLength = value;
            }
        }

        /// <summary>
        /// Длина часовой стрелки
        /// </summary>
        public float HourHandLength
        {
            get { return _hourHandLength; }

            set 
            {
                var minValue = MinHourHandLength();
                var maxValue = MaxHourHandLength();

                if(!Validator.ValidateRange(minValue, maxValue, value))
                {
                    throw new ArgumentException($"Input value is out of range - " +
                        $"[{minValue};{maxValue}]");
                }

                _hourHandLength = value;
            }
        }

        /// <summary>
        /// Состояние - отображать только часы
        /// </summary>
        public bool OnlyHours { get; set; }

        /// <summary>
        /// Форма часов
        /// </summary>
        public ClockForm FrameForm { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        WallClockParameters() { }
        #endregion

        #region [Private methods]

        /// <summary>
        /// Метод возвращает минимальное значение длины минутной стрелки
        /// </summary>
        /// <returns>Минимальное значение длины минутной стрелки</returns>
        private float MinMinuteHandLength()
        {
            return (_radius / 2) + 4;
        }

        /// <summary>
        /// Метод возвращает минимальное значение длины часовой стрелки
        /// </summary>
        /// <returns>Минимальное значение длины часовой стрелки</returns>
        private float MinHourHandLength()
        {
            return _radius / 5;
        }

        /// <summary>
        /// Метод возвращает максимальне значение длины минутной стрелки
        /// </summary>
        /// <returns>Максимальное значение длины минутной стрелки</returns>
        private float MaxMinuteHandLength()
        {
            return _radius - 2;
        }

        /// <summary>
        /// Метод возвращает максимальное значение длины часовой стрелки
        /// </summary>
        /// <returns>Максимальное значение длины часовой стрелки</returns>
        private float MaxHourHandLength()
        {
            return _minuteHandLength / 2;
        }
        #endregion
    }
}