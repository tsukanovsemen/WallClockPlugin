namespace WallClockPlugin.Model.UnitTests
{
    using System;
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
            var inputValue = 80;
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
            var expectedValue = radius * 0.55f;

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

        [Test(Description = "Ввод корректного значения для радиуса выреза.")]
        public void CutRadius_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters
            {
                SideWidth = 55
            };
            var cutRadius = 25;
            var expectedCutRadius = cutRadius;

            // Act
            parameters.CutRadius = cutRadius;
            var actualCutRadius = parameters.CutRadius;

            // Assert
            Assert.AreEqual(expectedCutRadius, actualCutRadius);
        }

        [Test(Description = "Ввод некорректного значения для радиуса выреза.")]
        public void CutRadius_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters
            {
                SideWidth = 55
            };
            var cutRadius = 60;
            var leftBorderOfValue = parameters.MinCutRadius();
            var rightBorderOfValue = parameters.MaxCutRadius();
            var message = $"Исключение должно быть брошено если входное " +
                $"значение больше чем {leftBorderOfValue} или меньше чем {rightBorderOfValue}.";

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.HourHandLength = cutRadius;
            },
           message);
        }

        [Test(Description = "Ввод корректного значения для количества допустимых вырезов.")]
        public void CutsCount_SetCorrectValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters
            {
                SideWidth = 55,
                Radius = 150
            };
            var inputCutsCount = 5;
            var expectedCutsCount = inputCutsCount;

            // Act
            parameters.CutsCount = inputCutsCount;
            var actualCutsCount = parameters.CutsCount;

            // Assert
            Assert.AreEqual(expectedCutsCount, actualCutsCount);
        }

        [Test(Description = "Ввод некорректного значения для количества допустимых вырезов.")]
        public void CutsCount_SetIncorrectValue_ThrowArgumentException()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters
            {
                SideWidth = 30,
                Radius = 100
            };
            var inputValue = 100;
            var leftBorderOfValue = parameters.MinCutsCount();
            var rightBorderOfValue = parameters.MaxCutsCount();
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

        [Test(Description = "Ввод значения для состояния - с вырезами/без.")]
        public void SideCuts_SetValue_ReturnsSameValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var inputStateValue = true;
            var expectedValue = inputStateValue;

            // Act
            parameters.SideCuts = inputStateValue;
            var actualValue = parameters.SideCuts;

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возврат корректного значения для максимального значения радиуса.")]
        public void MaxRadius_GetMaxRadius_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedMaxRadius = 200;

            // Act
            var actualMaxRadius = parameters.MaxRadius();

            // Assert
            Assert.AreEqual(expectedMaxRadius, actualMaxRadius);
        }

        [Test(Description = "Возврат корректного значения для максимальной ширины бортика.")]
        public void MaxSideWidth_GetMaxSideWidth_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 60;

            // Act
            var actualValue = parameters.MaxSideWidth();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возврат корректного значения для максимальной высоты бортика.")]
        public void MaxSideHeight_GetMaxSideHeight_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 40;

            // Act
            var actualValue = parameters.MaxSideHeight();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает корректное значение для максимально допустимого " +
            "количества вырезов.")]
        public void MaxCutsCount_GetMaxCutsCount_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters
            {
                SideWidth = 40,
                Radius = 140,
                CutRadius = 30
            };

            var expectedValue = 18;

            // Act
            var actualValue = parameters.MaxCutsCount();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает корректное значения для минимального количества вырезов.")]
        public void MinCutsCount_GetMinCutsCount_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 2;

            // Act
            var actualValue = parameters.MinCutsCount();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает корректное число для максимального радиуса вырезов.")]
        public void MaxCutRadius_GetMaxCutRadius_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var sideWidth = 40;
            parameters.SideWidth = sideWidth;
            var expectedValue = sideWidth - 5;

            // Act
            var actualValue = parameters.MaxCutRadius();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test(Description = "Возвращает корректное число для " +
            "минимального значения радиуса вырезов.")]
        public void MinCutRadius_GetMinCutRadius_ReturnsCorrectValue()
        {
            // Setup
            WallClockParameters parameters = new WallClockParameters();
            var expectedValue = 25;

            // Act
            var actualValue = parameters.MinCutRadius();

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
