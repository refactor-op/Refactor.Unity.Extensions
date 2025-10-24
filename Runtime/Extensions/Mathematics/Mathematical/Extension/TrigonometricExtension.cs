using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematics.Mathematical
{
    /// <summary>
    /// 三角函数拓展
    /// </summary>
    public static class TrigonometricExtension
    {
        /// <summary>
        /// sin
        /// </summary>
        /// <param name="x">角度</param>
        public static float Sin(this float x) => Mathf.Sin(x.DegreesToRadians());
        
        /// <summary>
        /// cos
        /// </summary>
        /// <param name="x">角度</param>
        public static float Cos(this float x) => Mathf.Cos(x.DegreesToRadians());
        
        /// <summary>
        /// tan
        /// </summary>
        /// <param name="x">角度</param>
        public static float Tan(this float x) => Mathf.Tan(x.DegreesToRadians());
        
        /// <summary>
        /// asin
        /// </summary>
        /// <returns>弧度</returns>
        public static float Asin(this float x) => Mathf.Asin(x);
        
        /// <summary>
        /// acos
        /// </summary>
        /// <returns>弧度</returns>
        public static float Acos(this float x) => Mathf.Acos(x);
        
        /// <summary>
        /// atan
        /// </summary>
        /// <returns>弧度</returns>
        public static float Atan(this float x) => Mathf.Atan(x);
        
        /// <summary>
        /// atan2
        /// </summary>
        /// <returns>弧度</returns>
        public static float Atan2(this float y, float x) => Mathf.Atan2(y, x);
        
        /// <summary>
        /// atan2
        /// </summary>
        /// <returns>弧度</returns>
        public static float Atan2(this Vector2 self) => Mathf.Atan2(self.y, self.x);
        
        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static float DegreesToRadians(this float degrees) => degrees * Mathf.Deg2Rad;
        
        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static float RadiansToDegrees(this float radians) => radians * Mathf.Rad2Deg;
    }
}