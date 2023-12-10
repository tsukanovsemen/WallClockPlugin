namespace StressTesting
{
    using System.Diagnostics;
    using WallClockPlugin.Model;

    /// <summary>
    /// Класс нагрузочного тестирования построения настенных часов.
    /// </summary>
    public class StressTestingWallClockBuilding
    {
        private readonly WallClockBuilder _builder;

        private readonly Stopwatch _stopwatch;

        private readonly WallClockParameters _parameters;

        private readonly StreamWriter _streamWriter;

        private readonly string _outputFileName = "log.txt";

        private const double GIGABYTE_IN_BYTE = 0.000000000931322574615478515625;

        public StressTestingWallClockBuilding()
        {
            _builder = new WallClockBuilder();
            _stopwatch = new Stopwatch();
            _parameters = new WallClockParameters();
            _streamWriter = new StreamWriter(_outputFileName, true);
        }

        public void StartStressTesting()
        {
            _stopwatch.Start();
            Process currentProcess = Process.GetCurrentProcess();
            var countIteration = 0;

            while (true)
            {
                _builder.Build(_parameters);
                var computerInfo = new NickStrupat.ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory
                    - computerInfo.AvailablePhysicalMemory) * GIGABYTE_IN_BYTE;

                _streamWriter.WriteLine($"{++countIteration}" +
                    $"\t{_stopwatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");

                _streamWriter.Flush();
            }
        }
    }
}
