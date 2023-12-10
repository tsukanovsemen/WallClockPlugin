namespace WallClockPlugin.StressTesting
{
    /// <summary>
    /// Класс точки входа.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            StressTestingWallClockBuilding stressTesting = new StressTestingWallClockBuilding();
            stressTesting.StartStressTesting();
        }
    }
}
