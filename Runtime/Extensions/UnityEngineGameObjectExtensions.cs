using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Refactor.Extensions
{
    public static class UnityEngineGameObjectExtensions
    {
        public static GameObject Active(this GameObject self, bool active)
        {
            self.SetActive(active);
            return self;
        }

        public static GameObject Layer(this GameObject self, int layer)
        {
            self.layer = layer;
            return self;
        }

        public static GameObject Layer(this GameObject self, string layerName)
        {
            self.layer = LayerMask.NameToLayer(layerName);
            return self;
        }

        public static bool IsInLayerMask(this GameObject self, LayerMask layerMask)
        {
            var objLayerMask = 1 << self.layer;
            return (layerMask.value & objLayerMask) == objLayerMask;
        }

        public static GameObject Tag(this GameObject self, string tag)
        {
            self.tag = tag;
            return self;
        }

        public static GameObject Name(this GameObject self, string name)
        {
            self.name = name;
            return self;
        }

        #region Component Op

        public static T GetOrAddComponent<T>(this GameObject self) where T : Component =>
            self.TryGetComponent<T>(out var comp) ? comp : self.AddComponent<T>();

        public static GameObject RemoveComponent<T>(this GameObject self) where T : Component
        {
            if (self.TryGetComponent<T>(out var comp)) Object.DestroyImmediate(comp);
            return self;
        }

        #endregion

        #region Hierarchy

        public static GameObject DestroyChildren(this GameObject self)
        {
            var childCount = self.transform.childCount;
            for (var i = childCount - 1; i >= 0; i--) Object.DestroyImmediate(self.transform.GetChild(i).gameObject);

            return self;
        }

        public static GameObject DestroyChildrenWhere(this GameObject self, Func<Transform, bool> condition)
        {
            var childCount = self.transform.childCount;
            for (var i = childCount - 1; i >= 0; i--)
            {
                var child = self.transform.GetChild(i);
                if (condition(child)) Object.DestroyImmediate(child.gameObject);
            }

            return self;
        }
        
        public static T DontDestroyOnLoad<T>(this T selfObj) where T : UnityEngine.Object
        {
            UnityEngine.Object.DontDestroyOnLoad(selfObj);
            return selfObj;
        }
        
        #endregion
    }
}