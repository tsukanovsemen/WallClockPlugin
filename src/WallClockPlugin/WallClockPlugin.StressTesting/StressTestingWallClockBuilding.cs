namespace WallClockPlugin.StressTesting
{
    using System.Diagnostics;
    using WallClockPlugin.Model;
    using System.IO;
    using Microsoft.VisualBasic.Devices;

    /// <summary>
    /// Класс нагрузочного тестирования построения настенных часов.
    /// </summary>
    public class StressTestingWallClockBuilding
    {

        /// <summary>
        /// Число, показывающее количество байт в гигабайте.
        /// </summary>
        private const double GIGABYTE_IN_BYTE = 0.000000000931322574615478515625;

        /// <summary>
        /// Объект, которые запускает построение детали.
        /// </summary>
        private readonly WallClockBuilder _builder;

        /// <summary>
        /// Объект определения времени выполнения приложения.
        /// </summary>
        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// Параметры объекта.
        /// </summary>
        private readonly WallClockParameters _parameters;

        /// <summary>
        /// Объект для записи данных в файл.
        /// </summary>
        private readonly StreamWriter _streamWriter;

        /// <summary>
        /// Имя выходного файла.
        /// </summary>
        private readonly string _outputFileName = "log.txt";

        /// <summary>
        /// Создает объект класса.
        /// </summary>
        public StressTestingWallClockBuilding()
        {
            _builder = new WallClockBuilder();
            _stopwatch = new Stopwatch();
            _parameters = new WallClockParameters();
            _streamWriter = new StreamWriter(_outputFileName, true);
        }

        /// <summary>
        /// Запуск стресс теста.
        /// </summary>
        public void StartStressTesting()
        {
            _stopwatch.Start();
            Process currentProcess = Process.GetCurrentProcess();
            var countIteration = 0;
            
            while (true)
            {
                _builder.Build(_parameters);
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory
                    - computerInfo.AvailablePhysicalMemory) * GIGABYTE_IN_BYTE;

                _streamWriter.WriteLine($"{++countIteration}" +
                    $"\t{_stopwatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");

                _streamWriter.Flush();
            }
        }
    }
}
