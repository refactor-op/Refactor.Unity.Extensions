using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematic.Vector
{
    /// <summary>
    /// 向量线性变换操作
    /// </summary>
    public static class LinearTransformation
    {
        /// <summary>
        /// 2D缩放
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="scale">缩放比例</param>
        /// <param name="pivot">缩放中心点</param>
        public static Vector2 Scale2D(this Vector2 origin, Vector2 scale, Vector2 pivot = default)
        {
            var matrix = LinearTransformationMatrix.ScaleMatrix(scale);
            var translated = origin - pivot;
            var scaled = matrix.MultiplyPoint(new Vector3(translated.x, translated.y, 0));
            return new Vector2(scaled.x, scaled.y) + pivot;
        }

        /// <summary>
        /// 3D缩放
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="scale">缩放比例</param>
        /// <param name="pivot">缩放中心点</param>
        public static Vector3 Scale3D(this Vector3 origin, Vector3 scale, Vector3 pivot = default)
        {
            var matrix = LinearTransformationMatrix.ScaleMatrix(scale);
            var translated = origin - pivot;
            return matrix.MultiplyPoint(translated) + pivot;
        }

        /// <summary>
        /// 2D旋转
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="angle">角度</param>
        /// <param name="pivot">旋转中心点</param>
        public static Vector2 Rotate2D(this Vector2 origin, float angle, Vector2 pivot = default)
        {
            var matrix = LinearTransformationMatrix.RotationMatrix(angle);
            var translated = origin - pivot;
            var rotated = matrix.MultiplyPoint(new Vector3(translated.x, translated.y, 0));
            return new Vector2(rotated.x, rotated.y) + pivot;
        }

        /// <summary>
        /// 3D旋转
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="angle">旋转角度(度)</param>
        /// <param name="axis">旋转轴</param>
        /// <param name="pivot">旋转中心点</param>
        public static Vector3 Rotate3D(this Vector3 origin, float angle, Vector3 axis, Vector3 pivot = default)
        {
            var matrix = LinearTransformationMatrix.RotationMatrix(angle, axis);
            var translated = origin - pivot;
            return matrix.MultiplyPoint(translated) + pivot;
        }

        /// <summary>
        /// 2D反射(关于直线)
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="lineNormal">直线向量</param>
        /// <param name="linePoint">直线上的点</param>
        public static Vector2 Reflect2D(this Vector2 origin, Vector2 lineNormal, Vector2 linePoint = default)
        {
            // 将2D反射转换为在XOY平面的3D反射
            var origin3D = new Vector3(origin.x, origin.y, 0);
            var normal3D = new Vector3(lineNormal.x, lineNormal.y, 0).normalized;
            var point3D = new Vector3(linePoint.x, linePoint.y, 0);

            var matrix = LinearTransformationMatrix.Reflection(normal3D, point3D);
            var reflected = matrix.MultiplyPoint(origin3D);
            return new Vector2(reflected.x, reflected.y);
        }

        /// <summary>
        /// 3D反射
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="planeNormal">平面法向量</param>
        /// <param name="planePoint">平面上的点</param>
        public static Vector3 Reflect3D(this Vector3 origin, Vector3 planeNormal, Vector3 planePoint = default)
        {
            var matrix = LinearTransformationMatrix.Reflection(planeNormal, planePoint);
            return matrix.MultiplyPoint(origin);
        }

        /// <summary>
        /// 2D错切变换
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="shear">错切因子 (x: X轴错切, y: Y轴错切)</param>
        /// <param name="pivot">错切中心点</param>
        public static Vector2 Shear2D(this Vector2 origin, Vector2 shear, Vector2 pivot = default)
        {
            var matrix = LinearTransformationMatrix.ShearMatrix(shear);
            var translated = origin - pivot;
            var sheared = matrix.MultiplyPoint(new Vector3(translated.x, translated.y, 0));
            return new Vector2(sheared.x, sheared.y) + pivot;
        }

        /// <summary>
        /// 3D错切变换
        /// </summary>
        /// <param name="origin">原始向量</param>
        /// <param name="shear">错切因子 (x: YZ面对X轴的错切, y: XZ面对Y轴的错切, z: XY面对Z轴的错切)</param>
        /// <param name="pivot">错切中心点</param>
        public static Vector3 Shear3D(this Vector3 origin, Vector3 shear, Vector3 pivot = default)
        {
            var shearMatrix = LinearTransformationMatrix.ShearMatrix(shear);
            var translated = origin - pivot;
            return shearMatrix.MultiplyPoint(translated) + pivot;
        }
    }
}