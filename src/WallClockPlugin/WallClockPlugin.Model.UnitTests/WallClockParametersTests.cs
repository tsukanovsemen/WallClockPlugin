using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallClockPlugin.Model.UnitTests
{
    using NUnit.Framework;
    using WallClockPlugin.Model;

    [TestFixture]
    public class WallClockParametersTests
    {
        [Test(Description = "Ввод корректного значения для радиуса.")]
        public void Radius_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = 150;
            var expectedValue = inputValue;

            // Act
            parameters.Radius = inputValue;
            var actualValue = parameters.Radius;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Ввод некорректного значения для радиуса.")]
        public void Radius_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = 210;
            var message = "Исключение должно быть выброшено если входное " +
                "значение радиуса больше 200 или меньше 100.";

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.Radius = inputValue;
            },
            message);
        }

        [Test(Description = "Ввод корректного значения для ширины борта.")]
        public void SideWidth_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = 45;
            var expectedValue = inputValue;

            // Act
            parameters.SideWidth = inputValue;
            var actualValue = parameters.SideWidth;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Ввод некорректного значения для ширины борта.")]
        public void SideWidth_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = 10;
            var message = "Исключение должно быть выброшено если входное " +
                "значение ширины борта больше 60 или меньше 30.";

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.SideWidth = inputValue;
            },
            message);
        }

        [Test(Description = "Ввод корректного значения для высоты борта.")]
        public void SideHeight_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = 30;
            var expectedValue = inputValue;

            // Act
            parameters.SideHeight = inputValue;
            var actualValue = parameters.SideHeight;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Ввод некорректного значения для высоты борта.")]
        public void SideHeight_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = 100;
            var message = "Исключение должно быть выброшено если входное " +
                "значение больше 40 или меньше 20.";

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.SideHeight = inputValue;
            },
            message);
        }

        [Test(Description = "Ввод корректного значения для длины минутной стрелки.")]
        public void MinuteHandLength_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var inputValue = 87;
            var expectedValue = inputValue;

            // Act
            parameters.MinuteHandLength = inputValue;
            var actualValue = parameters.MinuteHandLength;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Ввод некорректного значения для длины минутной стрелки.")]
        public void MinuteHandLength_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var inputValue = 20;
            var leftBorderOfValue = (radius / 2) + 4;
            var rightBorderOfValue = radius * 0.6;
            var message = $"Исключение должно быть выброшено если входное " +
                $"значение больше чем {rightBorderOfValue} и меньше чем {leftBorderOfValue}.";

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.MinuteHandLength = inputValue;
            },
           message);
        }

        [Test(Description = "Ввод корректного значения для длины часовой стрелки.")]
        public void HourHandLength_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var inputValue = 37;
            var expectedValue = inputValue;

            // Act
            parameters.HourHandLength = inputValue;
            var actualValue = parameters.HourHandLength;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Ввод некорректного значения для длины часовой стрелки.")]
        public void HourHandLength_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var inputValue = 10;
            var leftBorderOfValue = radius / 5;
            var rightBorderOfValue = radius * 0.3;
            var message = $"Исключение должно быть брошено если входное " +
                $"значение больше чем {leftBorderOfValue} или меньше чем {rightBorderOfValue}.";

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.HourHandLength = inputValue;
            },
           message);
        }

        [Test(Description = "Ввод true или false для OnlyHours.")]
        public void OnlyHours_SetValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputValue = true;
            var expectedValue = inputValue;

            // Act
            parameters.OnlyHours = inputValue;
            var actualValue = parameters.OnlyHours;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращение значения минимального для радиуса.")]
        public void MinRadius_GetMinValue_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 100;

            // Act
            var actualValue = parameters.MinRadius();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращение значения минимального для ширины бортика.")]
        public void MinSideWidth_GetMinSideWidth_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 30;

            // Act
            var actualValue = parameters.MinSideWidth();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращение значения минимального для высоты бортика.")]
        public void MinSideHeight_GetMinSideHeight_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 20;

            // Act
            var actualValue = parameters.MinSideHeight();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращение значения для ширины рисок.")]
        public void ClocksMarkWidth_GetClocksMarkWidth_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 15.0f;

            // Act
            var actualValue = parameters.ClocksMarkWidth();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращение длины часовой риски.")]
        public void ClocksHoursMarkLength_GetClocksHoursMarkLength_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var expectedValue = radius * 0.2;

            // Act
            var actualValue = parameters.ClocksHoursMarkLength();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращение длины минутной риски.")]
        public void ClocksMinutesMarkLength_GetClocksMinutesMarkLength_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var expectedValue = radius * 0.1;

            // Act
            var actualValue = parameters.ClocksMinutesMarkLength();
        }

        [Test(Description = "Возвращение высоты рисок.")]
        public void ClocksMarkHeight_GetClocksMarkHeight_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var sideHeight = 30;
            parameters.SideHeight = 30;
            var expectedValue = sideHeight / 6;

            // Act
            var actualValue = parameters.ClocksMarkHeight();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает минимальное значение длины минутной стрелки.")]
        public void MinMinuteHandLength_GetMinMinuteHandLength_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var expectedValue = (radius / 2) + 4;

            // Act
            var actualValue = parameters.MinMinuteHandLength();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает минимальное значение длины часовой стрелки.")]
        public void MinHourHandLength_GetMinHourHandLength_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var expectedValue = radius / 5;

            // Act
            var actualValue = parameters.MinHourHandLength();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает максимальное значение длины минутной стрелки.")]
        public void MaxMinuteHandLength_GetMaxMinuteHandLength_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var expectedValue = radius * 0.6;

            // Act
            var actualValue = parameters.MaxMinuteHandLength();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает максимальное значение длины часовой стрелки.")]
        public void MaxHourHandLength_GetMaxHourHandLength_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var radius = 150;
            parameters.Radius = radius;
            var expectedValue = radius * 0.3;

            // Act
            var actualValue = parameters.MaxHourHandLength();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
