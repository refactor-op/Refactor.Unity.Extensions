using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Refactor.Extensions
{
    public static class UnityEngineTransformExtensions
    {
        #region Hierarchy Management

        // Parent Setting
        public static T Parent<T>(this T self, Component parent) where T : Component
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }

        public static GameObject Parent(this GameObject self, Component parent)
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }

        public static T Parent<T>(this T self, GameObject parent) where T : Component
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }

        public static GameObject Parent(this GameObject self, GameObject parent)
        {
            self.transform.SetParent(parent?.transform);
            return self;
        }

        // Root Setting
        public static T AsRoot<T>(this T self) where T : Component
        {
            self.transform.SetParent(null);
            return self;
        }

        public static GameObject AsRoot(this GameObject self)
        {
            self.transform.SetParent(null);
            return self;
        }

        // Sibling Management
        public static T AsLastSibling<T>(this T self) where T : Component
        {
            self.transform.SetAsLastSibling();
            return self;
        }

        public static GameObject AsLastSibling(this GameObject self)
        {
            self.transform.SetAsLastSibling();
            return self;
        }

        public static T AsFirstSibling<T>(this T self) where T : Component
        {
            self.transform.SetAsFirstSibling();
            return self;
        }

        public static GameObject AsFirstSibling(this GameObject self)
        {
            self.transform.SetAsFirstSibling();
            return self;
        }

        public static T SiblingIndex<T>(this T self, int index) where T : Component
        {
            self.transform.SetSiblingIndex(index);
            return self;
        }

        public static GameObject SiblingIndex(this GameObject self, int index)
        {
            self.transform.SetSiblingIndex(index);
            return self;
        }

        // Children Management
        public static T DestroyChildren<T>(this T self) where T : Component
        {
            var childCount = self.transform.childCount;
            for (var i = childCount - 1; i >= 0; i--) Object.DestroyImmediate(self.transform.GetChild(i).gameObject);

            return self;
        }

        public static GameObject DestroyChildren(this GameObject self)
        {
            var childCount = self.transform.childCount;
            for (var i = childCount - 1; i >= 0; i--) Object.DestroyImmediate(self.transform.GetChild(i).gameObject);

            return self;
        }

        public static T DestroyChildrenWhere<T>(this T self, Func<Transform, bool> condition) where T : Component
        {
            var childCount = self.transform.childCount;
            for (var i = childCount - 1; i >= 0; i--)
            {
                var child = self.transform.GetChild(i);
                if (condition(child))
                    Object.DestroyImmediate(child.gameObject);
            }

            return self;
        }

        #endregion

        #region Transform Identity

        // Complete Identity Reset
        public static T LocalIdentity<T>(this T self) where T : Component
        {
            var transform = self.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale    = Vector3.one;
            return self;
        }

        public static GameObject LocalIdentity(this GameObject self)
        {
            var transform = self.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale    = Vector3.one;
            return self;
        }

        public static T Identity<T>(this T self) where T : Component
        {
            var transform = self.transform;
            transform.position   = Vector3.zero;
            transform.rotation   = Quaternion.identity;
            transform.localScale = Vector3.one;
            return self;
        }

        public static GameObject Identity(this GameObject self)
        {
            var transform = self.transform;
            transform.position   = Vector3.zero;
            transform.rotation   = Quaternion.identity;
            transform.localScale = Vector3.one;
            return self;
        }

        // Position Identity
        public static T LocalPositionIdentity<T>(this T self) where T : Component
        {
            self.transform.localPosition = Vector3.zero;
            return self;
        }

        public static GameObject LocalPositionIdentity(this GameObject self)
        {
            self.transform.localPosition = Vector3.zero;
            return self;
        }

        public static T PositionIdentity<T>(this T self) where T : Component
        {
            self.transform.position = Vector3.zero;
            return self;
        }

        public static GameObject PositionIdentity(this GameObject self)
        {
            self.transform.position = Vector3.zero;
            return self;
        }

        // Rotation Identity
        public static T LocalRotationIdentity<T>(this T self) where T : Component
        {
            self.transform.localRotation = Quaternion.identity;
            return self;
        }

        public static GameObject LocalRotationIdentity(this GameObject self)
        {
            self.transform.localRotation = Quaternion.identity;
            return self;
        }

        public static T RotationIdentity<T>(this T self) where T : Component
        {
            self.transform.rotation = Quaternion.identity;
            return self;
        }

        public static GameObject RotationIdentity(this GameObject self)
        {
            self.transform.rotation = Quaternion.identity;
            return self;
        }

        // Scale Identity
        public static T LocalScaleIdentity<T>(this T self) where T : Component
        {
            self.transform.localScale = Vector3.one;
            return self;
        }

        public static GameObject LocalScaleIdentity(this GameObject self)
        {
            self.transform.localScale = Vector3.one;
            return self;
        }

        #endregion

        #region Local Position

        // Vector3 Overloads
        public static T LocalPosition<T>(this T self, Vector3 localPosition) where T : Component
        {
            self.transform.localPosition = localPosition;
            return self;
        }

        public static GameObject LocalPosition(this GameObject self, Vector3 localPosition)
        {
            self.transform.localPosition = localPosition;
            return self;
        }

        // XYZ Overloads
        public static T LocalPosition<T>(this T self, float x, float y, float z) where T : Component
        {
            self.transform.localPosition = new Vector3(x, y, z);
            return self;
        }

        public static GameObject LocalPosition(this GameObject self, float x, float y, float z)
        {
            self.transform.localPosition = new Vector3(x, y, z);
            return self;
        }

        // XY Overloads (preserves Z)
        public static T LocalPosition<T>(this T self, float x, float y) where T : Component
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.x         = x;
            localPosition.y         = y;
            transform.localPosition = localPosition;
            return self;
        }

        public static GameObject LocalPosition(this GameObject self, float x, float y)
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.x         = x;
            localPosition.y         = y;
            transform.localPosition = localPosition;
            return self;
        }

        // Component Setters
        public static T LocalPositionX<T>(this T self, float x) where T : Component
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.x         = x;
            transform.localPosition = localPosition;
            return self;
        }

        public static GameObject LocalPositionX(this GameObject self, float x)
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.x         = x;
            transform.localPosition = localPosition;
            return self;
        }

        public static T LocalPositionY<T>(this T self, float y) where T : Component
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.y         = y;
            transform.localPosition = localPosition;
            return self;
        }

        public static GameObject LocalPositionY(this GameObject self, float y)
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.y         = y;
            transform.localPosition = localPosition;
            return self;
        }

        public static T LocalPositionZ<T>(this T self, float z) where T : Component
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.z         = z;
            transform.localPosition = localPosition;
            return self;
        }

        public static GameObject LocalPositionZ(this GameObject self, float z)
        {
            var transform     = self.transform;
            var localPosition = transform.localPosition;
            localPosition.z         = z;
            transform.localPosition = localPosition;
            return self;
        }

        // 2D Position Helpers
        public static T LocalPosition2d<T>(this T self, Vector2 position) where T : Component =>
            self.LocalPosition(position.x, position.y);

        public static GameObject LocalPosition2d(this GameObject self, Vector2 position) =>
            self.LocalPosition(position.x, position.y);

        #endregion

        #region Local Rotation

        // Quaternion Overloads
        public static T LocalRotation<T>(this T self, Quaternion localRotation) where T : Component
        {
            self.transform.localRotation = localRotation;
            return self;
        }

        public static GameObject LocalRotation(this GameObject self, Quaternion localRotation)
        {
            self.transform.localRotation = localRotation;
            return self;
        }

        // Euler Angles Overloads
        public static T LocalEulerAngles<T>(this T self, Vector3 localEulerAngles) where T : Component
        {
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }

        public static GameObject LocalEulerAngles(this GameObject self, Vector3 localEulerAngles)
        {
            self.transform.localEulerAngles = localEulerAngles;
            return self;
        }

        // Euler Component Setters
        public static T LocalEulerAnglesZ<T>(this T self, float z) where T : Component
        {
            var angles = self.transform.localEulerAngles;
            angles.z = z;
            self.LocalEulerAngles(angles);
            return self;
        }

        public static GameObject LocalEulerAnglesZ(this GameObject self, float z)
        {
            var angles = self.transform.localEulerAngles;
            angles.z = z;
            self.LocalEulerAngles(angles);
            return self;
        }

        #endregion

        #region Local Scale

        // Vector3 Overloads
        public static T LocalScale<T>(this T self, Vector3 scale) where T : Component
        {
            self.transform.localScale = scale;
            return self;
        }

        public static GameObject LocalScale(this GameObject self, Vector3 scale)
        {
            self.transform.localScale = scale;
            return self;
        }

        // Scale Overloads
        public static T LocalScale<T>(this T self, float xyz) where T : Component
        {
            self.transform.localScale = Vector3.one * xyz;
            return self;
        }

        public static GameObject LocalScale(this GameObject self, float xyz)
        {
            self.transform.localScale = Vector3.one * xyz;
            return self;
        }

        // XYZ Overloads
        public static T LocalScale<T>(this T self, float x, float y, float z) where T : Component
        {
            self.transform.localScale = new Vector3(x, y, z);
            return self;
        }

        public static GameObject LocalScale(this GameObject self, float x, float y, float z)
        {
            self.transform.localScale = new Vector3(x, y, z);
            return self;
        }

        // XY Overloads (preserves Z)
        public static T LocalScale<T>(this T self, float x, float y) where T : Component
        {
            var localScale = self.transform.localScale;
            localScale.x              = x;
            localScale.y              = y;
            self.transform.localScale = localScale;
            return self;
        }

        public static GameObject LocalScale(this GameObject self, float x, float y)
        {
            var localScale = self.transform.localScale;
            localScale.x              = x;
            localScale.y              = y;
            self.transform.localScale = localScale;
            return self;
        }

        // Component Setters
        public static T LocalScaleX<T>(this T self, float x) where T : Component
        {
            var localScale = self.transform.localScale;
            localScale.x              = x;
            self.transform.localScale = localScale;
            return self;
        }

        public static GameObject LocalScaleX(this GameObject self, float x)
        {
            var localScale = self.transform.localScale;
            localScale.x              = x;
            self.transform.localScale = localScale;
            return self;
        }

        public static T LocalScaleY<T>(this T self, float y) where T : Component
        {
            var localScale = self.transform.localScale;
            localScale.y              = y;
            self.transform.localScale = localScale;
            return self;
        }

        public static GameObject LocalScaleY(this GameObject self, float y)
        {
            var localScale = self.transform.localScale;
            localScale.y              = y;
            self.transform.localScale = localScale;
            return self;
        }

        public static T LocalScaleZ<T>(this T self, float z) where T : Component
        {
            var localScale = self.transform.localScale;
            localScale.z              = z;
            self.transform.localScale = localScale;
            return self;
        }

        public static GameObject LocalScaleZ(this GameObject self, float z)
        {
            var localScale = self.transform.localScale;
            localScale.z              = z;
            self.transform.localScale = localScale;
            return self;
        }

        #endregion

        #region Position

        // Vector3 Overloads
        public static T Position<T>(this T self, Vector3 position) where T : Component
        {
            self.transform.position = position;
            return self;
        }

        public static GameObject Position(this GameObject self, Vector3 position)
        {
            self.transform.position = position;
            return self;
        }

        // XYZ Overloads
        public static T Position<T>(this T self, float x, float y, float z) where T : Component
        {
            self.transform.position = new Vector3(x, y, z);
            return self;
        }

        public static GameObject Position(this GameObject self, float x, float y, float z)
        {
            self.transform.position = new Vector3(x, y, z);
            return self;
        }

        // XY Overloads (preserves Z)
        public static T Position<T>(this T self, float x, float y) where T : Component
        {
            var position = self.transform.position;
            position.x              = x;
            position.y              = y;
            self.transform.position = position;
            return self;
        }

        public static GameObject Position(this GameObject self, float x, float y)
        {
            var position = self.transform.position;
            position.x              = x;
            position.y              = y;
            self.transform.position = position;
            return self;
        }

        // Component Setters
        public static T PositionX<T>(this T self, float x) where T : Component
        {
            var position = self.transform.position;
            position.x              = x;
            self.transform.position = position;
            return self;
        }

        public static GameObject PositionX(this GameObject self, float x)
        {
            var position = self.transform.position;
            position.x              = x;
            self.transform.position = position;
            return self;
        }

        public static T PositionY<T>(this T self, float y) where T : Component
        {
            var position = self.transform.position;
            position.y              = y;
            self.transform.position = position;
            return self;
        }

        public static GameObject PositionY(this GameObject self, float y)
        {
            var position = self.transform.position;
            position.y              = y;
            self.transform.position = position;
            return self;
        }

        public static T PositionZ<T>(this T self, float z) where T : Component
        {
            var position = self.transform.position;
            position.z              = z;
            self.transform.position = position;
            return self;
        }

        public static GameObject PositionZ(this GameObject self, float z)
        {
            var position = self.transform.position;
            position.z              = z;
            self.transform.position = position;
            return self;
        }

        // Functional Component Setters
        public static T PositionX<T>(this T self, Func<float, float> xSetter) where T : Component
        {
            var position = self.transform.position;
            position.x              = xSetter(position.x);
            self.transform.position = position;
            return self;
        }

        public static GameObject PositionX(this GameObject self, Func<float, float> xSetter)
        {
            var position = self.transform.position;
            position.x              = xSetter(position.x);
            self.transform.position = position;
            return self;
        }

        public static T PositionY<T>(this T self, Func<float, float> ySetter) where T : Component
        {
            var position = self.transform.position;
            position.y              = ySetter(position.y);
            self.transform.position = position;
            return self;
        }

        public static GameObject PositionY(this GameObject self, Func<float, float> ySetter)
        {
            var position = self.transform.position;
            position.y              = ySetter(position.y);
            self.transform.position = position;
            return self;
        }

        public static T PositionZ<T>(this T self, Func<float, float> zSetter) where T : Component
        {
            var position = self.transform.position;
            position.z              = zSetter(position.z);
            self.transform.position = position;
            return self;
        }

        public static GameObject PositionZ(this GameObject self, Func<float, float> zSetter)
        {
            var position = self.transform.position;
            position.z              = zSetter(position.z);
            self.transform.position = position;
            return self;
        }

        // 2D Position Helpers
        public static T Position2d<T>(this T self, Vector2 position) where T : Component =>
            self.Position(position.x, position.y);

        public static GameObject Position2d(this GameObject self, Vector2 position) =>
            self.Position(position.x, position.y);

        public static T Position2d<T>(this T self, float x, float y) where T : Component => self.Position(x, y);

        public static GameObject Position2d(this GameObject self, float x, float y) => self.Position(x, y);

        #endregion

        #region Rotation

        // Quaternion Overloads
        public static T Rotation<T>(this T self, Quaternion rotation) where T : Component
        {
            self.transform.rotation = rotation;
            return self;
        }

        public static GameObject Rotation(this GameObject self, Quaternion rotation)
        {
            self.transform.rotation = rotation;
            return self;
        }

        // Euler Angles Overloads
        public static T EulerAngles<T>(this T self, Vector3 eulerAngles) where T : Component
        {
            self.transform.eulerAngles = eulerAngles;
            return self;
        }

        public static GameObject EulerAngles(this GameObject self, Vector3 eulerAngles)
        {
            self.transform.eulerAngles = eulerAngles;
            return self;
        }

        // Euler Component Setters
        public static T EulerAnglesZ<T>(this T self, float z) where T : Component
        {
            var angles = self.transform.eulerAngles;
            angles.z = z;
            self.EulerAngles(angles);
            return self;
        }

        public static GameObject EulerAnglesZ(this GameObject self, float z)
        {
            var angles = self.transform.eulerAngles;
            angles.z = z;
            self.EulerAngles(angles);
            return self;
        }

        #endregion

        #region Position Utilities

        // 2D Position Getters
        public static Vector2 Position2d(this GameObject self) =>
            new(self.transform.position.x, self.transform.position.y);

        public static Vector2 Position2d(this Component self) =>
            new(self.transform.position.x, self.transform.position.y);

        public static Vector2 LocalPosition2d(this GameObject self) =>
            new(self.transform.localPosition.x, self.transform.localPosition.y);

        public static Vector2 LocalPosition2d(this Component self) =>
            new(self.transform.localPosition.x, self.transform.localPosition.y);

        // Position Synchronization - From Methods
        public static GameObject SyncPositionFrom(this GameObject self, GameObject from) =>
            self.Position(from.transform.position);

        public static T SyncPositionFrom<T>(this T self, GameObject from) where T : Component =>
            self.Position(from.transform.position);

        public static GameObject SyncPositionFrom(this GameObject self, Component from) =>
            self.Position(from.transform.position);

        public static T SyncPositionFrom<T>(this T self, Component from) where T : Component =>
            self.Position(from.transform.position);

        public static GameObject SyncPosition2dFrom(this GameObject self, GameObject from) =>
            self.Position2d(from.Position2d());

        public static T SyncPosition2dFrom<T>(this T self, GameObject from) where T : Component =>
            self.Position2d(from.Position2d());

        public static GameObject SyncPosition2dFrom(this GameObject self, Component from) =>
            self.Position2d(from.Position2d());

        public static T SyncPosition2dFrom<T>(this T self, Component from) where T : Component =>
            self.Position2d(from.Position2d());

        // Position Synchronization - To Methods
        public static GameObject SyncPositionTo(this GameObject self, GameObject to)
        {
            to.Position(self.transform.position);
            return self;
        }

        public static GameObject SyncPositionTo(this GameObject self, Component to)
        {
            to.Position(self.transform.position);
            return self;
        }

        public static T SyncPositionTo<T>(this T self, GameObject to) where T : Component
        {
            to.Position(self.transform.position);
            return self;
        }

        public static T SyncPositionTo<T>(this T self, Component to) where T : Component
        {
            to.Position(self.transform.position);
            return self;
        }

        public static GameObject SyncPosition2dTo(this GameObject self, GameObject to)
        {
            to.Position2d(self.Position2d());
            return self;
        }

        public static GameObject SyncPosition2dTo(this GameObject self, Component to)
        {
            to.Position2d(self.Position2d());
            return self;
        }

        public static T SyncPosition2dTo<T>(this T self, GameObject to) where T : Component
        {
            to.Position2d(self.Position2d());
            return self;
        }

        public static T SyncPosition2dTo<T>(this T self, Component to) where T : Component
        {
            to.Position2d(self.Position2d());
            return self;
        }

        // Distance and Direction Utilities
        public static float DistanceTo(this GameObject self, GameObject target) =>
            Vector3.Distance(self.transform.position, target.transform.position);

        public static float DistanceTo(this Component self, Component target) =>
            Vector3.Distance(self.transform.position, target.transform.position);

        public static float DistanceTo(this GameObject self, Component target) =>
            Vector3.Distance(self.transform.position, target.transform.position);

        public static float DistanceTo<T>(this T self, GameObject target) where T : Component =>
            Vector3.Distance(self.transform.position, target.transform.position);

        public static float Distance2dTo(this GameObject self, GameObject target) =>
            Vector2.Distance(self.Position2d(), target.Position2d());

        public static float Distance2dTo(this Component self, Component target) =>
            Vector2.Distance(self.Position2d(), target.Position2d());

        public static Vector3 DirectionTo(this GameObject self, GameObject target) =>
            (target.transform.position - self.transform.position).normalized;

        public static Vector3 DirectionTo(this Component self, Component target) =>
            (target.transform.position - self.transform.position).normalized;

        public static Vector2 Direction2dTo(this GameObject self, GameObject target) =>
            (target.Position2d() - self.Position2d()).normalized;

        public static Vector2 Direction2dTo(this Component self, Component target) =>
            (target.Position2d() - self.Position2d()).normalized;

        #endregion

        #region Rotation Utilities

        // Look At Methods
        public static GameObject LookAt(this GameObject self, GameObject target)
        {
            self.transform.LookAt(target.transform);
            return self;
        }

        public static T LookAt<T>(this T self, GameObject target) where T : Component
        {
            self.transform.LookAt(target.transform);
            return self;
        }

        public static GameObject LookAt(this GameObject self, Component target)
        {
            self.transform.LookAt(target.transform);
            return self;
        }

        public static T LookAt<T>(this T self, Component target) where T : Component
        {
            self.transform.LookAt(target.transform);
            return self;
        }

        public static GameObject LookAt(this GameObject self, Vector3 worldPosition)
        {
            self.transform.LookAt(worldPosition);
            return self;
        }

        public static T LookAt<T>(this T self, Vector3 worldPosition) where T : Component
        {
            self.transform.LookAt(worldPosition);
            return self;
        }

        // 2D Look At (Z-axis rotation)
        public static GameObject LookAt2d(this GameObject self, GameObject target)
        {
            var direction = target.Position2d() - self.Position2d();
            var angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return self.EulerAnglesZ(angle);
        }

        public static T LookAt2d<T>(this T self, GameObject target) where T : Component
        {
            var direction = target.Position2d() - self.Position2d();
            var angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return self.EulerAnglesZ(angle);
        }

        public static GameObject LookAt2d(this GameObject self, Vector2 worldPosition)
        {
            var direction = worldPosition - self.Position2d();
            var angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return self.EulerAnglesZ(angle);
        }

        public static T LookAt2d<T>(this T self, Vector2 worldPosition) where T : Component
        {
            var direction = worldPosition - self.Position2d();
            var angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return self.EulerAnglesZ(angle);
        }

        #endregion
    }
}