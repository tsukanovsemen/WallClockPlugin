using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swcommands;

namespace WallClockPlugin.Model
{
    /// <summary>
    /// Класс-обертка для api SolidWorks
    /// </summary>
    public class SolidWorksWrapper
    {
        /// <summary>
        /// Объект SW api
        /// </summary>
        public SldWorks SolidWorks { get; private set; } = new SldWorks();

        /// <summary>
        /// Объект для работы с созданными документами
        /// </summary>
        public ModelDoc2 ModelDocument { get; private set; }

        /// <summary>
        /// X координата для рисования эскиза
        /// </summary>
        public float XCenter { get; private set; } = 0;

        /// <summary>
        /// Y координата для рисования эскиза
        /// </summary>
        public float YCenter { get; private set; } = 0;

        /// <summary>
        /// Z координата для рисования эскиза
        /// </summary>
        public float ZCenter { get; private set; } = 0;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SolidWorksWrapper()
        {}

        /// <summary>
        /// Запуск SolidWorks
        /// </summary>
        public void RunSolidWorks()
        { 
            SolidWorks.Visible = true;
        }

        /// <summary>
        /// Создание нового 3d документа
        /// </summary>
        public void CreateNewDocument()
        {
            SolidWorks.NewPart();
            ModelDocument = SolidWorks.IActiveDoc2;
        }

        /// <summary>
        /// Построение эскиза круга по радиусу
        /// </summary>
        /// <param name="radius">Радиус круга в мм</param>
        /// <exception cref="ArgumentException">Исключение если документ был null</exception>
        public void CreateCircleSketch(ModelDoc2 document, float radius)
        {
            if(document == null)
                throw new ArgumentException("Document was null.");

            document.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            document.SketchManager.InsertSketch(true);
            document.ClearSelection2(true);

            var radiusInMeters = radius / 1000.0f;

            document.SketchManager.CreateCircleByRadius(XCenter, YCenter, ZCenter, radiusInMeters);
            document.ClearSelection2(true);
        }

        /// <summary>
        /// Выдавливание детали
        /// </summary>
        /// <param name="document">Активный документ</param>
        /// <param name="sketch">Активный эскиз детали</param>
        /// <param name="extrusionDepth">Глубина выдаливания в мм</param>
        /// <exception cref="ArgumentException">Исключение если документ был null</exception>
        public void ExtrudePart(ModelDoc2 document, Sketch sketch, float extrusionDepth)
        {
            if(document == null)
                throw new ArgumentException("Document was null.");

            //Доступ к элементу feature manager к активному эскизу
            Feature feature = (Feature)sketch;
            var sketchName = feature.Name;
            var extrusionDepthInMeters = extrusionDepth / 1000.0f;

            document.Extension.SelectByID2(sketchName, "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            document.FeatureManager.FeatureExtrusion2(true, false, true, 0, 0, extrusionDepthInMeters, 0,
                false, false, false, false, 0, 0, false, false, false, false,
                true, true, true, 0, 0, false);
        }

        /// <summary>
        /// Вырез детали
        /// </summary>
        /// <param name="document">Активный документ</param>
        /// <param name="sketch">Активный эскиз</param>
        /// <param name="cutoutDepth">Глубина выреза</param>
        /// <exception cref="ArgumentException">Исключение если документ был null</exception>
        public void CutPart(ModelDoc2 document, Sketch sketch, float cutoutDepth)
        {
            if (document == null)
                throw new ArgumentException("Document was null.");

            //Доступ к элементу feature manager к активному эскизу
            Feature feature = (Feature)sketch;
            var sketchName = feature.Name;
            var cutoutDepthInMeters = cutoutDepth / 1000.0f;

            document.Extension.SelectByID2(sketchName, "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            document.FeatureManager.FeatureCut4(true, false, false, 0, 0, cutoutDepthInMeters, 0, 
                false, false, false, false, 0, 0, 
                false, false, false, false, false, true, true, true, true, false, 0, 0, false, false); 
        }
    }
}
    