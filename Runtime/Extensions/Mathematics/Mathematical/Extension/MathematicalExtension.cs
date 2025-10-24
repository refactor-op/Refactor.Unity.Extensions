using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematics.Mathematical
{
    /// <summary>
    /// 常用数学公式链式拓展
    /// </summary>
    public static class MathematicalExtension
    {

        #region Key Mathematical

        /// <summary>
        /// 求根
        /// </summary>
        public static float Sqrt(this float val) => Mathf.Sqrt(val);

        /// <summary>
        /// 平方根
        /// </summary>
        /// <param name="val">Vector2</param>
        /// <returns>每个分量的平方根</returns>
        public static Vector2 Sqrt(this Vector2 val) => new Vector2(Mathf.Sqrt(val.x), Mathf.Sqrt(val.y));

        /// <summary>
        /// 平方根
        /// </summary>
        /// <param name="val">Vector3</param>
        /// <returns>每个分量的平方根</returns>
        public static Vector3 Sqrt(this Vector3 val) =>
            new Vector3(Mathf.Sqrt(val.x), Mathf.Sqrt(val.y), Mathf.Sqrt(val.z));

        /// <summary>
        /// 平方根
        /// </summary>
        /// <param name="val">Vector4</param>
        /// <returns>每个分量的平方根</returns>
        public static Vector4 Sqrt(this Vector4 val) =>
            new Vector4(Mathf.Sqrt(val.x), Mathf.Sqrt(val.y), Mathf.Sqrt(val.z), Mathf.Sqrt(val.w));

        /// <summary>
        /// 立方根
        /// </summary>
        /// <param name="val">数值</param>
        /// <returns>立方根</returns>
        public static float Cbrt(this float val)
        {
            if (val >= 0)
                return Mathf.Pow(val, 1f/3f);

            return -Mathf.Pow(-val, 1f/3f);
        }

        /// <summary>
        /// 数值的幂次方
        /// </summary>
        /// <param name="val">底数</param>
        /// <param name="power">指数</param>
        /// <returns>幂次结果</returns>
        public static float Pow(this float val, float power) => Mathf.Pow(val, power);

        /// <summary>
        /// e的幂次方
        /// </summary>
        /// <param name="val">指数</param>
        /// <returns>e的val次方</returns>
        public static float Exp(this float val) => Mathf.Exp(val);

        /// <summary>
        /// 指定底数的对数
        /// </summary>
        /// <param name="val">真数</param>
        /// <param name="baseValue">底数</param>
        /// <returns>对数结果</returns>
        public static float Log(this float val, float baseValue) => Mathf.Log(val, baseValue);

        /// <summary>
        /// 自然对数
        /// </summary>
        /// <param name="val">真数</param>
        /// <returns>自然对数结果</returns>
        public static float Log(this float val) => Mathf.Log(val);

        #endregion

        #region Round

        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>向下取整后的浮点数</returns>
        public static float Floor(this float val) => Mathf.Floor(val);

        /// <summary>
        /// 向下取整并转换为整数
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>向下取整后的整数</returns>
        public static int FloorToInt(this float val) => Mathf.FloorToInt(val);

        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>向上取整后的浮点数</returns>
        public static float Ceil(this float val) => Mathf.Ceil(val);

        /// <summary>
        /// 向上取整并转换为整数
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>向上取整后的整数</returns>
        public static int CeilToInt(this float val) => Mathf.CeilToInt(val);

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>四舍五入后的浮点数</returns>
        public static float Round(this float val) => Mathf.Round(val);

        /// <summary>
        /// 四舍五入并转换为整数
        /// </summary>
        /// <param name="val">输入值</param>
        /// <returns>四舍五入后的整数</returns>
        public static int RoundToInt(this float val) => Mathf.RoundToInt(val);

        /// <summary>
        /// 取小数部分
        /// </summary>
        public static float Fraction(this float val) => val - Mathf.Floor(val);

        #endregion

        #region others

        /// <summary>
        /// 判断正负号
        /// </summary>
        public static int Sign(this int val) => (int)Mathf.Sign(val);

        /// <summary>
        /// 判断正负号
        /// </summary>
        public static float Sign(this float val) => Mathf.Sign(val);

        /// <summary>
        /// 绝对值
        /// </summary>
        public static int Abs(this int val) => Mathf.Abs(val);

        /// <summary>
        /// 绝对值
        /// </summary>
        public static float Abs(this float val) => Mathf.Abs(val);

        /// <summary>
        /// 线性插值
        /// </summary>
        /// <param name="self">比例</param>
        /// <param name="a">起始点</param>
        /// <param name="b">终点</param>
        /// <returns></returns>
        public static float Lerp(this float self, float a, float b) => Mathf.Lerp(a, b, self);

        #endregion

    }
}