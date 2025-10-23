using UnityEngine;

namespace Refactor.Extensions
{
    public static class UnityEngineComponentExtensions
    {
        public static TComponent GetOrAddComponent<TComponent>(this Component self) where TComponent : Component =>
            self.TryGetComponent<TComponent>(out var comp) ? comp : self.gameObject.AddComponent<TComponent>();

        public static T RemoveComponent<T, TComponent>(this T self) where T : Component where TComponent : Component
        {
            if (self.TryGetComponent<TComponent>(out var comp)) Object.DestroyImmediate(comp);
            return self;
        }

        #region GameObject Op

        public static T Active<T>(this T self, bool active) where T : Component
        {
            self.gameObject.SetActive(active);
            return self;
        }

        public static T Show<T>(this T self) where T : Component
        {
            self.gameObject.SetActive(true);
            return self;
        }

        public static T Hide<T>(this T self) where T : Component
        {
            self.gameObject.SetActive(false);
            return self;
        }

        public static T Layer<T>(this T self, int layer) where T : Component
        {
            self.gameObject.layer = layer;
            return self;
        }

        public static T Layer<T>(this T self, string layerName) where T : Component
        {
            self.gameObject.layer = LayerMask.NameToLayer(layerName);
            return self;
        }

        public static T Tag<T>(this T self, string tag) where T : Component
        {
            self.gameObject.tag = tag;
            return self;
        }

        public static T Name<T>(this T self, string name) where T : Component
        {
            self.gameObject.name = name;
            return self;
        }

        #endregion
    }
}