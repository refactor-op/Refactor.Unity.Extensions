using UnityEngine;

namespace Refactor.Unity.Extensions
{
    public enum CoordinateSystemOrigin
    {
        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft,
        Center
    }

    public static partial class CoordinateSystem2dExtensions
    {
        // 1. From...
        public static PointInCoordinateSystem FromTopLeft(this Vector2 self, Rect referenceRect) =>
            new(self, referenceRect, CoordinateSystemOrigin.TopLeft);

        public static PointInCoordinateSystem FromTopRight(this Vector2 self, Rect referenceRect) =>
            new(self, referenceRect, CoordinateSystemOrigin.TopRight);

        public static PointInCoordinateSystem FromBottomLeft(this Vector2 self, Rect referenceRect) =>
            new(self, referenceRect, CoordinateSystemOrigin.BottomLeft);

        public static PointInCoordinateSystem FromBottomRight(this Vector2 self, Rect referenceRect) =>
            new(self, referenceRect, CoordinateSystemOrigin.BottomRight);

        public static PointInCoordinateSystem FromCenter(this Vector2 self, Rect referenceRect) =>
            new(self, referenceRect, CoordinateSystemOrigin.Center);

        private static Vector2 CoordinateSystemConverted(
            this Vector2 origin,
            Rect rect,
            CoordinateSystemOrigin from,
            CoordinateSystemOrigin to)
        {
            if (from == to) return origin;

            // 1. 将 from 转换为归一化的坐标空间 (0,0 -> 1,1, 左下角为原点)
            Vector2 normalized;
            switch (from)
            {
                case CoordinateSystemOrigin.TopLeft:
                    normalized.x = (origin.x - rect.min.x) / rect.width;
                    normalized.y = (rect.max.y - origin.y) / rect.height;
                    break;
                case CoordinateSystemOrigin.TopRight:
                    normalized.x = (rect.max.x - origin.x) / rect.width;
                    normalized.y = (rect.max.y - origin.y) / rect.height;
                    break;
                case CoordinateSystemOrigin.BottomRight:
                    normalized.x = (rect.max.x - origin.x) / rect.width;
                    normalized.y = (origin.y - rect.min.y) / rect.height;
                    break;
                case CoordinateSystemOrigin.Center:
                    normalized.x = (origin.x - rect.center.x) / rect.width + 0.5f;
                    normalized.y = (origin.y - rect.center.y) / rect.height + 0.5f;
                    break;
                case CoordinateSystemOrigin.BottomLeft:
                default:
                    normalized.x = (origin.x - rect.min.x) / rect.width;
                    normalized.y = (origin.y - rect.min.y) / rect.height;
                    break;
            }

            // 2. 将归一化的坐标点转换为 to 下的点
            Vector2 target;
            switch (to)
            {
                case CoordinateSystemOrigin.TopLeft:
                    target.x = rect.min.x + normalized.x * rect.width;
                    target.y = rect.max.y - normalized.y * rect.height;
                    break;
                case CoordinateSystemOrigin.TopRight:
                    target.x = rect.max.x - normalized.x * rect.width;
                    target.y = rect.max.y - normalized.y * rect.height;
                    break;
                case CoordinateSystemOrigin.BottomRight:
                    target.x = rect.max.x - normalized.x * rect.width;
                    target.y = rect.min.y + normalized.y * rect.height;
                    break;
                case CoordinateSystemOrigin.Center:
                    target.x = rect.center.x + (normalized.x - 0.5f) * rect.width;
                    target.y = rect.center.y + (normalized.y - 0.5f) * rect.height;
                    break;
                case CoordinateSystemOrigin.BottomLeft:
                default:
                    target.x = rect.min.x + normalized.x * rect.width;
                    target.y = rect.min.y + normalized.y * rect.height;
                    break;
            }

            return target;
        }

        public readonly ref struct PointInCoordinateSystem // 这个只读结构体用于在链式调用中传递数据
        {
            private readonly Vector2                _originPoint;
            private readonly Rect                   _referenceRect;
            private readonly CoordinateSystemOrigin _fromSystem;

            public PointInCoordinateSystem(Vector2 originPoint, Rect referenceRect, CoordinateSystemOrigin fromSystem)
            {
                _originPoint   = originPoint;
                _referenceRect = referenceRect;
                _fromSystem    = fromSystem;
            }

            // 2. ...To
            private Vector2 To(CoordinateSystemOrigin toSystem) =>
                CoordinateSystemConverted(_originPoint, _referenceRect, _fromSystem, toSystem);

            public Vector2 ToTopLeft() => To(CoordinateSystemOrigin.TopLeft);

            public Vector2 ToTopRight() => To(CoordinateSystemOrigin.TopRight);

            public Vector2 ToBottomLeft() => To(CoordinateSystemOrigin.BottomLeft);

            public Vector2 ToBottomRight() => To(CoordinateSystemOrigin.BottomRight);

            public Vector2 ToCenter() => To(CoordinateSystemOrigin.Center);
        }
    }
}