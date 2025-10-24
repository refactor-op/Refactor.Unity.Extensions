using System;
using UnityEngine;

namespace Refactor.Unity.Extensions.Mathematics.Vector
{
    public readonly ref struct Swizzler
    {
        private readonly Vector4                              _origin;
        private readonly (float x, float y, float z, float w) _built;
        private readonly int                                  _count;

        private Swizzler(in Vector4 origin, (float, float, float, float) built, int count)
        {
            _origin = origin;
            _built  = built;
            _count  = count;
        }

        public Swizzler(in Vector4 origin)
        {
            _origin = origin;
            _built  = default; // (0, 0, 0, 0).
            _count  = 0;
        }

        public Swizzler X() => new(_origin, Append(_origin.x), _count + 1);
        public Swizzler Y() => new(_origin, Append(_origin.y), _count + 1);
        public Swizzler Z() => new(_origin, Append(_origin.z), _count + 1);
        public Swizzler W() => new(_origin, Append(_origin.w), _count + 1);
        public Swizzler O() => new(_origin, Append(0f), _count + 1);
        public Swizzler I() => new(_origin, Append(1f), _count + 1);

        private (float, float, float, float) Append(float value) =>
            _count switch
            {
                0 => (value, 0, 0, 0),
                1 => (_built.x, value, 0, 0),
                2 => (_built.x, _built.y, value, 0),
                3 => (_built.x, _built.y, _built.z, value),
                _ => throw new InvalidOperationException("More than 4d.")
            };

        public static implicit operator Vector2(Swizzler swizzler) => 
            new(swizzler._built.x, swizzler._built.y);
        public static implicit operator Vector3(Swizzler swizzler) =>
            new(swizzler._built.x, swizzler._built.y, swizzler._built.z);
        public static implicit operator Vector4(Swizzler swizzler) =>
            new(swizzler._built.x, swizzler._built.y, swizzler._built.z, swizzler._built.w);
    }
    
    public readonly ref struct SwizzlerInt
    {
        private readonly Vector3Int            _origin;
        private readonly (int x, int y, int z) _built;
        private readonly int                   _count;

        private SwizzlerInt(in Vector3Int origin, (int, int, int) built, int count)
        {
            _origin = origin;
            _built  = built;
            _count  = count;
        }

        public SwizzlerInt(in Vector3Int origin)
        {
            _origin = origin;
            _built  = default; // (0, 0, 0).
            _count  = 0;
        }

        public SwizzlerInt X() => new(_origin, Append(_origin.x), _count + 1);
        public SwizzlerInt Y() => new(_origin, Append(_origin.y), _count + 1);
        public SwizzlerInt Z() => new(_origin, Append(_origin.z), _count + 1);
        public SwizzlerInt O() => new(_origin, Append(0), _count + 1);
        public SwizzlerInt I() => new(_origin, Append(1), _count + 1);

        private (int, int, int) Append(int value) =>
            _count switch
            {
                0 => (value, 0, 0),
                1 => (_built.x, value, 0),
                2 => (_built.x, _built.y, value),
                _ => throw new InvalidOperationException("More than 4d.")
            };

        public static implicit operator Vector2Int(SwizzlerInt swizzler) => 
            new(swizzler._built.x, swizzler._built.y);
        public static implicit operator Vector3Int(SwizzlerInt swizzler) =>
            new(swizzler._built.x, swizzler._built.y, swizzler._built.z);
    }

    public static partial class VectorExtensions
    {
        public static Swizzler Swizzle(this in Vector2 self) => new(self);
        public static Swizzler Swizzle(this in Vector3 self) => new(self);
        public static Swizzler Swizzle(this in Vector4 self) => new(self);
        
        public static SwizzlerInt Swizzle(this in Vector2Int self) => new(self.AsVector3Int());
        public static SwizzlerInt Swizzle(this in Vector3Int self) => new(self);
    }
}