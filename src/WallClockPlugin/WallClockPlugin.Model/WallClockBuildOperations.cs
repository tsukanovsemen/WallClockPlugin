using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallClockPlugin.Model
{
    /// <summary>
    /// Операции для создания настенных часов
    /// </summary>
    public enum WallClockBuildOperations
    {
        /// <summary>
        /// Создание окружности для чаосв
        /// </summary>
        CreateCircleForClock = 1,

        /// <summary>
        /// Создание окружности для циферблата 
        /// </summary>
        CreateCircleForClockFace,

        /// <summary>
        /// Выдавливание окружности для часов
        /// </summary>
        ExtrudeCircleForClock,

        /// <summary>
        /// Вырез окружности для циферблата
        /// </summary>
        CutCircleForClockFace,

        /// <summary>
        /// Создание осевой лини
        /// </summary>
        CreateAxisLine, 

        /// <summary>
        /// Создание прямоугольника для шкалы часов
        /// </summary>
        CreateRectangleForHoursScale,

        /// <summary>
        /// Создание прямоугольника для шкалы минут
        /// </summary>
        CreateRectangleForMinutesScale,

        /// <summary>
        /// Выдавливание прямоугольника шкалы минут
        /// </summary>
        ExtrudeRectangleScaleMinutes,

        /// <summary>
        /// Выдавливание прямоугольника шкалы часов
        /// </summary>
        ExtrudeRectangleScaleHours,

        /// <summary>
        /// Создание массива шкалы часов
        /// </summary>
        CreateCircleArrayHours,

        /// <summary>
        /// Создание массива шкалы минут
        /// </summary>
        CreateCircleArrayMinutes,

        /// <summary>
        /// Создание центральной окружности для стрелок часов
        /// </summary>
        CreateCentralCircleForHands,

        /// <summary>
        /// Выдавливание центральной окружности для стрелок часов
        /// </summary>
        ExtrudeCentralCircleForHands
    }
}
