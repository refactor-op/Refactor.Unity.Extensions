using UnityEngine;

namespace Refactor.Extensions
{
    public static class UnityEngineObjectExtensions
    {
        #region DontDestroyOnLoad

        public static T DontDestroyOnLoad<T>(this T self) where T : Object
        {
            if (self) Object.DontDestroyOnLoad(self);
            return self;
        }

        #endregion

        #region Instantiation

        public static T Instantiate<T>(this T self) where T : Object => Object.Instantiate(self);

        public static T Instantiate<T>(this T self, Transform parent) where T : Object =>
            Object.Instantiate(self, parent);

        public static T Instantiate<T>(this T self, Transform parent, bool worldPositionStays) where T : Object =>
            Object.Instantiate(self, parent, worldPositionStays);

        public static T Instantiate<T>(this T self, Vector3 position, Quaternion rotation) where T : Object =>
            Object.Instantiate(self, position, rotation);

        public static T Instantiate<T>(this T self, Vector3 position, Quaternion rotation, Transform parent)
            where T : Object =>
            Object.Instantiate(self, position, rotation, parent);

        #endregion

        #region Destruction

        public static T DestroySelf<T>(this T self) where T : Object
        {
            if (self) Object.Destroy(self);
            return self;
        }

        public static T DestroySelf<T>(this T self, float delay) where T : Object
        {
            if (self) Object.Destroy(self, delay);
            return self;
        }

        public static T DestroyImmediate<T>(this T self) where T : Object
        {
            if (self) Object.DestroyImmediate(self);
            return self;
        }

        public static T DestroyImmediate<T>(this T self, bool allowDestroyingAssets) where T : Object
        {
            if (self) Object.DestroyImmediate(self, allowDestroyingAssets);
            return self;
        }

        #endregion
    }
}