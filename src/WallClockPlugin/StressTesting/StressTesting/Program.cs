namespace StressTesting
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Входные данные.</param>
        static void Main(string[] args)
        {
            StressTestingWallClockBuilding stressTesting = new StressTestingWallClockBuilding();
            stressTesting.StartStressTesting();
        }
    }
}
