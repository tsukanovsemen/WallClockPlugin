namespace WallClockPlugin.Model
{
    /// <summary>
    /// Класс для построения модели часов
    /// </summary>
    public class WallClockBuilder
    {
        /// <summary>
        /// Обертка над api SolidWorks
        /// </summary>
        public SolidWorksWrapper Wrapper { get; private set; } = new SolidWorksWrapper();

        /// <summary>
        /// Построение детали по входным параметрам
        /// </summary>
        /// <param name="parameters">Параметры детали</param>
        public void Build(WallClockParameters parameters)
        {
            Wrapper.RunSolidWorks();
            Wrapper.CreateNewDocument();

            BuildClockFrame(parameters.Radius, parameters.SideWidth, 
                parameters.SideHeight, parameters.FrameForm);

            BuildHoursAndMinutes(parameters.OnlyHours, parameters.Radius);

            BuildHourAndMinuteHands(parameters.HourHandLength, parameters.MinuteHandLength);
        }

        /// <summary>
        /// Построение формы часов
        /// </summary>
        /// <param name="radius">Радиус часов</param>
        /// <param name="sideWidth">Ширина бортика</param>
        /// <param name="sideHeight">Высота бортика</param>
        /// <param name="clockForm">Форма часов</param>
        private void BuildClockFrame(float radius, float sideWidth, 
            float sideHeight, ClockForm clockForm)
        {
            Wrapper.CreateCircleSketch(Wrapper.ModelDocument, radius + sideWidth);

            var activeSketch = Wrapper.ModelDocument.SketchManager.ActiveSketch;

            Wrapper.ExtrudePart(Wrapper.ModelDocument, activeSketch, sideHeight);

            Wrapper.CreateCircleSketch(Wrapper.ModelDocument, radius);

            activeSketch = Wrapper.ModelDocument.SketchManager.ActiveSketch;

            Wrapper.CutPart(Wrapper.ModelDocument, activeSketch, sideHeight / 2);
        }

        /// <summary>
        /// Построение рисок часов и минут
        /// </summary>
        /// <param name="onlyHours">Построение только часов</param>
        /// <param name="radius">Радиус циферблата</param>
        private void BuildHoursAndMinutes (bool onlyHours, float radius)
        {

        }

        private void BuildHourAndMinuteHands(float hourHandLength, float minuteHandeLength)
        {

        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public WallClockBuilder() 
        {}

    }
}
