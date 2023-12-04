namespace WallClockPlugin.Model.UnitTests
{
    using NUnit.Framework;
    using WallClockPlugin.Model;

    [TestFixture]
    public class ValidatorTests
    {
        [Test(Description = "Ввод некорректного значения.")]
        public void ValidateRange_InputIncorrectValue_ReturnsFalse()
        {
            // Setup
            var leftBorder = 10;
            var rightBorder = 15;
            var inputValue = 32;
            var expectedResult = false;

            // Act
            var actualResult = Validator.ValidateRange(leftBorder, rightBorder, inputValue);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test(Description = "Ввод корректного значения.")]
        public void ValidateRange_InputCorrectValue_ReturnTrue()
        {
            // Setup
            var leftBorder = 10;
            var rightBorder = 15;
            var inputValue = 12;
            var expectedResult = true;

            // Act
            var actualResult = Validator.ValidateRange(leftBorder, rightBorder, inputValue);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
