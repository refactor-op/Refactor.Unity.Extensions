using System.Runtime.CompilerServices;
using UnityEngine;

namespace Refactor.Extensions
{
    public readonly ref struct InstantiateBuilder<T> where T : Object
    {
        private readonly T          _prefab;
        private readonly Vector3    _position;
        private readonly Quaternion _rotation;
        private readonly Transform  _parent;
        private readonly bool       _worldPositionStays;
        private readonly string     _name;
        private readonly int        _layer;
        private readonly string     _tag;
        private readonly bool       _active;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal InstantiateBuilder(T prefab)
        {
            _prefab             = prefab;
            _position           = Vector3.zero;
            _rotation           = Quaternion.identity;
            _parent             = null;
            _worldPositionStays = true;
            _name               = null;
            _layer              = -1;
            _tag                = null;
            _active             = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private InstantiateBuilder(
            T prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent,
            bool worldPositionStays,
            string name,
            int layer,
            string tag,
            bool active)
        {
            _prefab             = prefab;
            _position           = position;
            _rotation           = rotation;
            _parent             = parent;
            _worldPositionStays = worldPositionStays;
            _name               = name;
            _layer              = layer;
            _tag                = tag;
            _active             = active;
        }

        /// <summary>
        ///     设置位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithPosition(float x = 0, float y = 0, float z = 0) =>
            new(_prefab,
                new Vector3(x, y, z),
                _rotation,
                _parent,
                _worldPositionStays,
                _name,
                _layer,
                _tag,
                _active);

        /// <summary>
        ///     设置位置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithPosition(Vector3 position) =>
            new(_prefab,
                position,
                _rotation,
                _parent,
                _worldPositionStays,
                _name,
                _layer,
                _tag,
                _active);

        /// <summary>
        ///     设置旋转
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithRotation(float x = 0, float y = 0, float z = 0) =>
            new(_prefab,
                _position,
                Quaternion.Euler(x, y, z),
                _parent,
                _worldPositionStays,
                _name,
                _layer,
                _tag,
                _active);

        /// <summary>
        ///     设置旋转
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithRotation(Quaternion rotation) =>
            new(_prefab,
                _position,
                rotation,
                _parent,
                _worldPositionStays,
                _name,
                _layer,
                _tag,
                _active);

        /// <summary>
        ///     设置父对象
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithParent(Transform parent, bool worldPositionStays = true) =>
            new(_prefab,
                _position,
                _rotation,
                parent,
                worldPositionStays,
                _name,
                _layer,
                _tag,
                _active);

        /// <summary>
        ///     设置名称
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithName(string name) =>
            new(_prefab,
                _position,
                _rotation,
                _parent,
                _worldPositionStays,
                name,
                _layer,
                _tag,
                _active);

        /// <summary>
        ///     设置层级，支持简化参数签名 (int layer = 0)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithLayer(int layer = 0) =>
            new(_prefab,
                _position,
                _rotation,
                _parent,
                _worldPositionStays,
                _name,
                layer,
                _tag,
                _active);

        /// <summary>
        ///     设置层级（通过名称）
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithLayer(string layerName) => WithLayer(LayerMask.NameToLayer(layerName));

        /// <summary>
        ///     设置标签
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithTag(string tag) =>
            new(_prefab,
                _position,
                _rotation,
                _parent,
                _worldPositionStays,
                _name,
                _layer,
                tag,
                _active);

        /// <summary>
        ///     设置激活状态，支持简化参数签名 (bool active = true)
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public InstantiateBuilder<T> WithActive(bool active = true) =>
            new(_prefab,
                _position,
                _rotation,
                _parent,
                _worldPositionStays,
                _name,
                _layer,
                _tag,
                active);

        /// <summary>
        ///     执行实例化并应用所有设置
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Build()
        {
            var instance = Object.Instantiate(_prefab);
            if (!instance) return instance;

            var gameObject = instance switch
            {
                GameObject go  => go,
                Component comp => comp.gameObject,
                _              => null
            };
            if (!gameObject) return instance;

            if (_parent)
            {
                gameObject.transform.SetParent(_parent, _worldPositionStays);
            }
            else
            {
                gameObject.transform.position = _position;
                gameObject.transform.rotation = _rotation;
            }

            if (!string.IsNullOrEmpty(_name)) gameObject.name = _name;
            if (_layer >= 0) gameObject.layer                 = _layer;
            if (!string.IsNullOrEmpty(_tag)) gameObject.tag   = _tag;
            if (!_active) gameObject.SetActive(false);

            return instance;
        }

        /// <summary>
        ///     隐式转换为实例化结果
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator T(InstantiateBuilder<T> builder) => builder.Build();
    }

    public static class InstantiateBuilderExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static InstantiateBuilder<T> WithBuilder<T>(this T self) where T : Object => new(self);
    }
}