using UnityEngine;

namespace Refactor.Unity.Extensions
{
    public static partial class VectorExtensions
    {
        #region Int <-> Float

        public static Vector2 AsFloat(this Vector2Int self) => new(self.x, self.y);

        public static Vector3 AsFloat(this Vector3Int self) => new(self.x, self.y, self.z);

        public static Vector2Int ToInt(this Vector2 self) => new(Mathf.RoundToInt(self.x), Mathf.RoundToInt(self.y));

        public static Vector3Int ToInt(this Vector3 self) => new(Mathf.RoundToInt(self.x),
            Mathf.RoundToInt(self.y),
            Mathf.RoundToInt(self.z));

        #endregion

        #region 2d <-> 3d <-> 4d

        public static Vector3 AsVector3(this Vector2 self, float z = 0) => new(self.x, self.y, z);

        public static Vector4 AsVector4(this Vector3 self, float w = 0) => new(self.x, self.y, self.z, w);

        public static Vector4 AsVector4(this Vector2 self, float z = 0, float w = 0) => new(self.x, self.y, z, w);

        public static Vector2 ToVector2(this Vector3 self) => new(self.x, self.y);

        public static Vector2 ToVector2(this Vector4 self) => new(self.x, self.y);

        public static Vector3 ToVector3(this Vector4 self) => new(self.x, self.y, self.z);

        public static Vector3Int AsVector3Int(this Vector2Int self, int z = 0) => new(self.x, self.y, z);

        public static Vector2Int ToVector2Int(this Vector3Int self) => new(self.x, self.y);
        
        #endregion

        #region Wither

        public static Vector2 WithX(this Vector2 self, float x) => new(x, self.y);

        public static Vector2 WithY(this Vector2 self, float y) => new(self.x, y);

        public static Vector3 WithX(this Vector3 self, float x) => new(x, self.y, self.z);

        public static Vector3 WithY(this Vector3 self, float y) => new(self.x, y, self.z);

        public static Vector3 WithZ(this Vector3 self, float z) => new(self.x, self.y, z);

        public static Vector4 WithX(this Vector4 self, float x) => new(x, self.y, self.z, self.w);

        public static Vector4 WithY(this Vector4 self, float y) => new(self.x, y, self.z, self.w);

        public static Vector4 WithZ(this Vector4 self, float z) => new(self.x, self.y, z, self.w);

        public static Vector4 WithW(this Vector4 self, float w) => new(self.x, self.y, self.z, w);

        #endregion

        #region 变换

        /// <summary>
        ///     反射一个二维向量.
        /// </summary>
        /// <param name="self">要反射的向量.</param>
        /// <param name="normal">反射的法线向量. 必须是单位向量.</param>
        public static Vector2 Reflect(this Vector2 self, Vector2 normal) => Vector2.Reflect(self, normal);

        /// <summary>
        ///     反射一个三维向量.
        /// </summary>
        /// <param name="self">要反射的向量.</param>
        /// <param name="normal">反射的法线向量. 必须是单位向量.</param>
        public static Vector3 Reflect(this Vector3 self, Vector3 normal) => Vector3.Reflect(self, normal);


        /// <summary>
        ///     沿水平轴 (X 轴) 反射一个二维向量.
        /// </summary>
        public static Vector2 ReflectHorizontal(this Vector2 self) => self.Reflect(Vector2.right);

        /// <summary>
        ///     沿垂直轴 (Y 轴) 反射一个二维向量.
        /// </summary>
        public static Vector2 ReflectVertical(this Vector2 self) => self.Reflect(Vector2.up);

        /// <summary>
        ///     沿 XZ 平面反射一个三维向量.
        /// </summary>
        public static Vector3 ReflectXz(this Vector3 self) => self.Reflect(Vector3.up);

        /// <summary>
        ///     沿 XY 平面反射一个三维向量.
        /// </summary>
        public static Vector3 ReflectXy(this Vector3 self) => self.Reflect(Vector3.forward);

        /// <summary>
        ///     沿 YZ 平面反射一个三维向量.
        /// </summary>
        public static Vector3 ReflectYz(this Vector3 self) => self.Reflect(Vector3.right);

        #endregion
    }
}