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
    public class SolidWorksWrapper : ICADWrapper
    {
        /// <summary>
        /// Объект SW api
        /// </summary>
        public SldWorks SolidWorks { get; private set; } = new SldWorks();

        /// <summary>
        /// Объект для работы с созданными документами
        /// </summary>
        public ModelDoc2 ModelDocument { get; private set; }

        public void RunCAD()
        {
            SolidWorks.Visible = true;
        }

        public void CreateNewDocument()
        {
            SolidWorks.NewPart();
            ModelDocument = SolidWorks.IActiveDoc2;
        }

        public void CreateCircleSketch(float radius, float xc, float yc, float zc, string operationName)
        {
            ModelDocument.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            var radiusInMeters = radius / 1000.0f;

            ModelDocument.SketchManager.CreateCircleByRadius(xc, yc, zc, radiusInMeters);

            var currentFeature = (Feature)ModelDocument.SketchManager.ActiveSketch;
            currentFeature.Name = operationName;

            ModelDocument.ClearSelection2(true);
        }

        public void ExtrudePart(float extrusionDepth, string operationName, bool oneSide = true)
        {
            //Доступ к элементу feature manager к активному эскизу
            Feature currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            var sketchName = currentFeature.Name;

            // Если выдавливание в две стороны, то тогда делим пополам
            extrusionDepth = oneSide ? extrusionDepth : extrusionDepth / 2;

            var extrusionDepthInMeters = extrusionDepth / 1000.0f;

            ModelDocument.Extension.SelectByID2(sketchName, "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);

            var feature = ModelDocument.FeatureManager.FeatureExtrusion2(oneSide, false, false, 0, 0, extrusionDepthInMeters,
                oneSide ? 0 : extrusionDepthInMeters, false, false, false, false, 0, 0, false, false, false, false,
                true, true, true, 0, 0, false);

            feature.Name = operationName;
        }

        public void CutPart(float cutoutDepth, string operationName, bool oneSide = true)
        {
            //Доступ к элементу feature manager к активному эскизу
            Feature currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            var sketchName = currentFeature.Name;

            var cutoutDepthInMeters = cutoutDepth / 1000.0f;

            ModelDocument.Extension.SelectByID2(sketchName, "SKETCHSEGMENT", 0, 0, 0, false, 0, null, 0);
            var feature = ModelDocument.FeatureManager.FeatureCut4(true, false, true, 0, 0, cutoutDepthInMeters, 0,
                false, false, false, false, 0, 0,
                false, false, false, false, false, true, true, true, true, false, 0, 0, false, false);

            feature.Name = operationName;
        }

        public void CreateRectangleSketch(float width, float height, string operationName,
            float xc = 0, float yc = 0, float zc = 0)
        {
            ModelDocument.Extension.SelectByID2("Спереди", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.InsertSketch(true);
            ModelDocument.ClearSelection2(true);

            width /= 2;
            height /= 2;

            var widthInMeters = width / 1000;
            var heightInMeters = height / 1000;

            yc /= 1000;

            var x2 = xc + widthInMeters;
            var y2 = yc + heightInMeters;

            ModelDocument.SketchManager.CreateCenterRectangle(xc, yc, zc, x2, y2, zc);

            var feature = ModelDocument.SketchManager.ActiveSketch as Feature;
            feature.Name = operationName;

            ModelDocument.ClearSelection2(true);
        }

        /// <summary>
        /// Создание вспомогательной линии
        /// </summary>
        public void CreateAxisLine(string operationName) 
        {
            ModelDocument.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0, false, 0, null, 0);
            ModelDocument.SketchManager.CreateCenterLine(0, 0.02, 0, 0, -0.02, 0);

            var currentFeature = ModelDocument.SketchManager.ActiveSketch as Feature;
            currentFeature.Name = operationName;

            ModelDocument.SketchManager.InsertSketch(true);
        }

        public void CreateCircularArray(int count, float angle, string operationName, 
            string repetitiveOperationName, string axisName)
        {
            ModelDocument.Extension.SelectByID2(repetitiveOperationName, "BODYFEATURE", 0, 0, 0, false, 4, null, 0);
            ModelDocument.Extension.SelectByID2("Line1@" + axisName, "EXTSKETCHSEGMENT", 0, -0.02f, 0, true, 1, null, 0);

            var angleInRadian = (angle * Math.PI) / 180;

            var feature = ModelDocument.FeatureManager.FeatureCircularPattern4(count, angleInRadian, true,
               "NULL", true, false, false );

            //feature.Name = operationName;

            ModelDocument.FeatureManager.CreateFeature(feature);
            ModelDocument.ClearSelection2(true);
        }
    }

}
    