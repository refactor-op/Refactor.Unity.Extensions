using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematics.Vector
{
    /// <summary>
    /// 向量计算链式拓展
    /// </summary>
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

        #region 基本向量运算
        
        /// <summary>
        /// 向量模长
        /// </summary>
        public static float Magnitude(this Vector3 vector) => vector.magnitude;
        
        /// <summary>
        /// 向量模长
        /// </summary>
        public static float Magnitude(this Vector2 vector) => vector.magnitude;
        
        /// <summary>
        /// 向量平方模长
        /// </summary>
        public static float SqrMagnitude(this Vector3 vector) => vector.sqrMagnitude;
        
        /// <summary>
        /// 向量平方模长
        /// </summary>
        public static float SqrMagnitude(this Vector2 vector) => vector.sqrMagnitude;
        
        /// <summary>
        /// 向量归一化
        /// </summary>
        public static Vector3 Normalize(this Vector3 self) => self.normalized;
        
        /// <summary>
        /// 向量归一化
        /// </summary>
        public static Vector2 Normalize(this Vector2 self) => self.normalized;
        
        #endregion

        #region 向量间关系运算
        
        /// <summary>
        /// 计算从当前点到目标点的方向向量
        /// </summary>
        /// <param name="self">当前点</param>
        /// <param name="to">目标点</param>
        /// <returns>方向向量 (to - self)</returns>
        public static Vector3 DirectionTo(this Vector3 self, Vector3 to) => to - self;
        
        /// <summary>
        /// 计算从当前点到目标点的方向向量
        /// </summary>
        public static Vector2 DirectionTo(this Vector2 self, Vector2 to) => to - self;
        
        /// <summary>
        /// 计算从起点到当前点的方向向量
        /// </summary>
        /// <param name="self">当前点</param>
        /// <param name="from">起点</param>
        /// <returns>方向向量 (self - from)</returns>
        public static Vector3 DirectionFrom(this Vector3 self, Vector3 from) => self - from;
        
        /// <summary>
        /// 计算从起点到当前点的方向向量
        /// </summary>
        public static Vector2 DirectionFrom(this Vector2 self, Vector2 from) => self - from;
        
        /// <summary>
        /// 计算两个点之间的距离
        /// </summary>
        /// <param name="self">当前点</param>
        /// <param name="other">另一个点</param>
        /// <returns>两点之间的距离</returns>
        public static float Distance(this Vector3 self, Vector3 other) => Vector3.Distance(self, other);
        
        /// <summary>
        /// 计算两个点之间的距离
        /// </summary>
        public static float Distance(this Vector2 self, Vector2 other) => Vector2.Distance(self, other);
        
        /// <summary>
        /// 点乘
        /// </summary>
        /// <param name="self">当前点</param>
        /// <param name="other">另一个点</param>
        public static float Dot(this Vector3 self, Vector3 other) => Vector3.Dot(self, other);
        
        /// <summary>
        /// 点乘
        /// </summary>
        public static float Dot(this Vector2 self, Vector2 other) => Vector2.Dot(self, other);
        
        /// <summary>
        /// 叉乘
        /// </summary>
        /// <param name="self">当前点</param>
        /// <param name="other">另一个点</param>
        public static Vector3 Cross(this Vector3 self, Vector3 other) => Vector3.Cross(self, other);
        
        #endregion

        #region 角度与方向转换
        
        /// <summary>
        /// 向量转角度
        /// </summary>
        public static float ToAngle(this Vector2 self)
        {
            if (self.magnitude < Mathf.Epsilon)
                return 0f;

            float angle = Mathf.Atan2(self.y, self.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;
            return angle;
        }
        
        /// <summary>
        /// 角度转方向向量
        /// </summary>
        public static Vector2 ToVector(this float self) 
            => new Vector2(Mathf.Cos(self * Mathf.Deg2Rad), Mathf.Sin(self * Mathf.Deg2Rad));
        
        #endregion
        
    }
}