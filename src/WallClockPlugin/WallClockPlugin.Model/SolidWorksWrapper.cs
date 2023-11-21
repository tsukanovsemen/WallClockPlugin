namespace WallClockPlugin.Model
{
    using System;
    using SolidWorks.Interop.sldworks;
    using SolidWorks.Interop.swconst;

    /// <summary>
    /// Класс-обертка для api SolidWorks.
    /// </summary>
    public class SolidWorksWrapper : ICADWrapper
    {
        /// <summary>
        /// Объект SW api.
        /// </summary>
        public SldWorks SolidWorks { get; private set; } = new SldWorks();

        /// <summary>
        /// Объект для работы с созданными документами.
        /// </summary>
        public ModelDoc2 ModelDocument { get; private set; }

        /// <summary>
        /// Запуск САПР.
        /// </summary>
        public void RunCAD()
        {
            SolidWorks.Visible = true;
            SolidWorks.FrameState = (int)swWindowState_e.swWindowMaximized;
        }

        /// <summary>
        /// Создание нового документа.
        /// </summary>
        public void CreateNewDocument()
        {
            SolidWorks.NewPart();
            ModelDocument = SolidWorks.IActiveDoc2;
        }

        /// <summary>
        /// Создание эскиза круга.
        /// </summary>
        /// <param name="radius">Радиус круга в мм.</param>
        /// <param name="xCenter">Координата x центра.</param>
        /// <param name="yCenter">Координата y центра.</param>
        /// <param name="zCenter">Координата z центра.</param>
        /// <param name="operationName">Название операции.</param>
        public void CreateCircleSketch(
            float radius,
            float xCenter,
            float yCenter,
            float zCenter,
            string operationName)
        {
            ModelDocument.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            var radiusInMeters = ToMeters(radius);

            ModelDocument.SketchManager.CreateCircleByRadius(
                xCenter, yCenter, zCenter, radiusInMeters);

            var currentFeature = (Feature)ModelDocument.SketchManager.ActiveSketch;
            currentFeature.Name = operationName;

            ModelDocument.ClearSelection2(true);
        }

        /// <summary>
        /// Выдавливание детали.
        /// </summary>
        /// <param name="extrusionDepth">Глубина выдавливания в мм.</param>
        /// <param name="operationName">Название операции.</param>
        /// <param name="oneSide">Выдавливание в двух или одном направлении.</param>
        public void ExtrudePart(float extrusionDepth, string operationName, bool oneSide = true)
        {
            // Доступ к элементу feature manager к активному эскизу.
            Feature currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            var sketchName = currentFeature.Name;

            // Если выдавливание в две стороны, то тогда делим пополам
            extrusionDepth = oneSide ? extrusionDepth : extrusionDepth / 2;

            var extrusionDepthInMeters = ToMeters(extrusionDepth);

            ModelDocument.Extension.SelectByID2(
                sketchName,
                "SKETCHSEGMENT",
                0,
                0,
                0,
                false,
                0,
                null,
                0);

            var feature = ModelDocument.FeatureManager.FeatureExtrusion2(
                oneSide,
                false,
                false,
                0,
                0,
                extrusionDepthInMeters,
                oneSide ? 0 : extrusionDepthInMeters,
                false,
                false,
                false,
                false,
                0,
                0,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                0,
                0,
                false);

            feature.Name = operationName;
        }

        /// <summary>
        /// Вырезание детали.
        /// </summary>
        /// <param name="cutoutDepth">Глубина выреза в мм.</param>
        /// <param name="operationName">Название операции.</param>
        /// <param name="oneSide">Вырезание в двух или одном направлении.</param>
        public void CutPart(float cutoutDepth, string operationName, bool oneSide = true)
        {
            // Доступ к элементу feature manager к активному эскизу.
            Feature currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            var sketchName = currentFeature.Name;

            var cutoutDepthInMeters = ToMeters(cutoutDepth);

            ModelDocument.Extension.SelectByID2(
                sketchName, "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);

            var feature = ModelDocument.FeatureManager.FeatureCut4(
                true,
                false,
                true,
                0,
                0,
                cutoutDepthInMeters,
                0,
                false,
                false,
                false,
                false,
                0,
                0,
                false,
                false,
                false,
                false,
                false,
                true,
                true,
                true,
                true,
                false,
                0,
                0,
                false,
                false);

            feature.Name = operationName;
        }

        /// <summary>
        /// Создание эскиза прямоугольника.
        /// </summary>
        /// <param name="width">Ширина прямоугольника в мм.</param>
        /// <param name="height">Высота прямоугольника в мм.</param>
        /// <param name="operationName">Название операции.</param>
        /// <param name="xCenter">Координата x центра.</param>
        /// <param name="yCenter">Координата y центра.</param>
        /// <param name="zCenter">Координата z центра.</param>
        public void CreateRectangleSketch(
            float width,
            float height,
            string operationName,
            float xCenter = 0,
            float yCenter = 0,
            float zCenter = 0)
        {
            ModelDocument.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            // Делим пополам, так как метод САПР строит по половине параметров
            width /= 2;
            height /= 2;

            var widthInMeters = ToMeters(width);
            var heightInMeters = ToMeters(height);

            xCenter = ToMeters(xCenter);
            yCenter = ToMeters(yCenter);
            zCenter = ToMeters(zCenter);

            var x2 = xCenter + widthInMeters;
            var y2 = yCenter + heightInMeters;

            ModelDocument.SketchManager.CreateCenterRectangle(
                xCenter,
                yCenter,
                zCenter,
                x2,
                y2,
                zCenter);

            var feature = ModelDocument.SketchManager.ActiveSketch as Feature;
            feature.Name = operationName;

            ModelDocument.ClearSelection2(true);
        }

        /// <summary>
        /// Создание оси.
        /// </summary>
        /// <param name="operationName">Название операции.</param>
        public void CreateAxisLine(string operationName)
        {
            ModelDocument.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.CreateCenterLine(0, 0.02, 0, 0, -0.02, 0);

            var currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            currentFeature.Name = operationName;

            ModelDocument.SketchManager.InsertSketch(true);
        }

        /// <summary>
        /// Создание кругового массива из операций.
        /// </summary>
        /// <param name="count">Количество повторяющихся элементов.</param>
        /// <param name="angle">Угол между элементами.</param>
        /// <param name="operationName">Название операции.</param>
        /// <param name="repetitiveOperationName">Название повторяющейся операции.</param>
        /// <param name="axisName">Название оси.</param>
        public void CreateCircularArray(
            int count,
            float angle,
            string operationName,
            string repetitiveOperationName,
            string axisName)
        {
            ModelDocument.Extension.SelectByID2(
                repetitiveOperationName, "BODYFEATURE", 0, 0, 0, false, 4, null, 0);

            ModelDocument.Extension.SelectByID2(
                "Line1@" + axisName, "EXTSKETCHSEGMENT", 0, -0.02f, 0, true, 1, null, 0);

            var angleInRadian = ToRadians(angle);

            var feature = ModelDocument.FeatureManager.FeatureCircularPattern4(
                count, angleInRadian, true, "NULL", true, false, false);

            feature.Name = operationName;

            ModelDocument.FeatureManager.CreateFeature(feature);
            ModelDocument.ClearSelection2(true);
        }

        /// <summary>
        /// Создание эскиза Ромба.
        /// </summary>
        /// <param name="verticalDiagonalLength">Длина в мм.</param>
        /// <param name="horizontalDiagonalLength">Длина горизонтальной диагонали.</param>
        /// <param name="xCenter">Координата x центра.</param>
        /// <param name="yCenter">Координата y центра.</param>
        /// <param name="zCenter">Координата z центра.</param>
        /// <param name="angle">Угол наклона ромба в градусах.</param>
        public void CreateRhombusSketch(
            float verticalDiagonalLength,
            float horizontalDiagonalLength,
            float xCenter = 0,
            float yCenter = 0,
            float zCenter = 0,
            float angle = 0)
        {
            ModelDocument.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            verticalDiagonalLength = ToMeters(verticalDiagonalLength);
            horizontalDiagonalLength = ToMeters(horizontalDiagonalLength);

            var angleInRadian = ToRadians(angle);

            var x3 = verticalDiagonalLength * Math.Sin(angleInRadian);
            var y3 = Math.Sqrt(Math.Pow(verticalDiagonalLength, 2) - Math.Pow(x3, 2));

            var hypotenuse = Math.Sqrt(Math.Pow((verticalDiagonalLength / 2), 2)
                + Math.Pow((horizontalDiagonalLength / 2), 2));

            var innerAngleInRadian = Math.Asin(horizontalDiagonalLength / 2 / hypotenuse);

            var generalAngleInRadian = innerAngleInRadian + angleInRadian;

            var x2 = hypotenuse * Math.Sin(generalAngleInRadian);
            var y2 = Math.Sqrt(Math.Pow(hypotenuse, 2) - Math.Pow(x2, 2));

            // Если угол находится в 3 четверти.
            if (angle > 90 && angle <= 180)
            {
                y2 = -y2;
                y3 = -y3;
            }

            // Если угол находится во 2 четверти.
            else if (angle > 180 && angle <= 270)
            {
                y2 = -y2;
                y3 = -y3;
            }

            ModelDocument.SketchManager.CreateParallelogram(
                xCenter,
                yCenter,
                zCenter,
                x2,
                y2,
                zCenter,
                x3,
                y3,
                zCenter);
        }

        /// <summary>
        /// Показать деталь в Триметрии.
        /// </summary>
        public void ShowTrimetry()
        {
            ModelDocument.ShowNamedView2("*Триметрия", 8);
        }

        /// <summary>
        /// Перевод из миллиметров в метры.
        /// </summary>
        /// <param name="valueInMM">Значение в миллиметрах.</param>
        /// <returns>Значение в метрах.</returns>
        private float ToMeters(float valueInMM)
        {
            return valueInMM / 1000;
        }

        /// <summary>
        /// Перевод из градусов в радианы.
        /// </summary>
        /// <param name="degrees">Значение в градусах.</param>
        /// <returns>Значение в радианах.</returns>
        private float ToRadians(float degrees)
        {
            return (float)(degrees * Math.PI) / 180;
        }
    }
}