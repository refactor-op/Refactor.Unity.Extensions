using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematic.Vector
{
    /// <summary>
    /// 线性变换矩阵
    /// </summary>
    public static class LinearTransformationMatrix
    {
        /// <summary>
        /// 3D平移矩阵
        /// </summary>
        public static Matrix4x4 TranslationMatrix(Vector3 translation)
        {
            return Matrix4x4.Translate(translation);
        }

        /// <summary>
        /// 2D平移矩阵
        /// </summary>
        public static Matrix4x4 TranslationMatrix(Vector2 translation)
        {
            return Matrix4x4.Translate(new Vector3(translation.x, translation.y, 0));
        }

        /// <summary>
        /// 3D缩放矩阵
        /// </summary>
        public static Matrix4x4 ScaleMatrix(Vector3 scale)
        {
            return Matrix4x4.Scale(scale);
        }

        /// <summary>
        /// 2D缩放矩阵
        /// </summary>
        public static Matrix4x4 ScaleMatrix(Vector2 scale)
        {
            return Matrix4x4.Scale(new Vector3(scale.x, scale.y, 1));
        }

        /// <summary>
        /// 3D旋转矩阵
        /// </summary>
        /// <param name="angle">旋转角度</param>
        /// <param name="axis">旋转轴</param>
        public static Matrix4x4 RotationMatrix(float angle, Vector3 axis)
        {
            return Matrix4x4.Rotate(Quaternion.AngleAxis(angle, axis));
        }

        /// <summary>
        /// 2D旋转矩阵
        /// </summary>
        public static Matrix4x4 RotationMatrix(float angle)
        {
            return Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));
        }
        
        /// <summary>
        /// 平面反射矩阵
        /// </summary>
        /// <param name="planeNormal">平面法向量</param>
        /// <param name="planePoint">平面上的一点</param>
        public static Matrix4x4 Reflection(Vector3 planeNormal, Vector3 planePoint = default)
        {
            var n = planeNormal.normalized;
            float d = -Vector3.Dot(n, planePoint);
            
            var reflectionMatrix = new Matrix4x4
            {
                m00 = 1 - 2 * n.x * n.x, m01 = -2 * n.x * n.y, m02 = -2 * n.x * n.z, m03 = -2 * n.x * d,
                m10 = -2 * n.x * n.y, m11 = 1 - 2 * n.y * n.y, m12 = -2 * n.y * n.z, m13 = -2 * n.y * d,
                m20 = -2 * n.x * n.z, m21 = -2 * n.y * n.z, m22 = 1 - 2 * n.z * n.z, m23 = -2 * n.z * d,
                m30 = 0, m31 = 0, m32 = 0, m33 = 1
            };

            return reflectionMatrix;
        }

        /// <summary>
        /// 3D错切变换
        /// </summary>
        /// <param name="shear">错切向量</param>
        /// <returns>4x4 错切矩阵</returns>
        public static Matrix4x4 ShearMatrix(Vector3 shear)
        {
            Matrix4x4 shearMatrix = new Matrix4x4
            {
                m00 = 1, m01 = shear.y, m02 = shear.z, m03 = 0,
                m10 = shear.x, m11 = 1, m12 = shear.z, m13 = 0,
                m20 = shear.x, m21 = shear.y, m22 = 1, m23 = 0,
                m30 = 0, m31 = 0, m32 = 0, m33 = 1
            };

            return shearMatrix;
        }

        /// <summary>
        /// 创建 2D 错切变换矩阵
        /// </summary>
        /// <param name="shear">错切向量</param>
        /// <returns>3x3 错切矩阵</returns>
        public static Matrix4x4 ShearMatrix(Vector2 shear)
        {
            var shearMatrix = Matrix4x4.identity;
            
            shearMatrix.m01 = shear.y;
            shearMatrix.m10 = shear.x;

            return shearMatrix;
        }
    }
}