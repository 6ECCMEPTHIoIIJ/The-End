using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components.Common
{
    public class EcsTransformConverter : ComponentConverter<EcsTransform>
    {
    }

    [Serializable]
    public struct EcsTransform
    {
        [field: SerializeField] public Transform Transform { get; private set; }
    }

    public static class EcsTransformHelper
    {
        public static Vector3 GetPosition(in this EcsTransform ecsTransform) => ecsTransform.Transform.position;
        public static Quaternion GetRotation(in this EcsTransform ecsTransform) => ecsTransform.Transform.rotation;
        public static Vector3 GetForward(in this EcsTransform ecsTransform) => ecsTransform.Transform.forward;
        public static Vector3 GetRight(in this EcsTransform ecsTransform) => ecsTransform.Transform.right;
        public static Vector3 GetUp(in this EcsTransform ecsTransform) => ecsTransform.Transform.up;
        
        public static void SetPosition(ref this EcsTransform ecsTransform, Vector3 position) => ecsTransform.Transform.position = position;
        public static void SetRotation(ref this EcsTransform ecsTransform, Quaternion rotation) => ecsTransform.Transform.rotation = rotation;
        public static void SetForward(ref this EcsTransform ecsTransform, Vector3 forward) => ecsTransform.Transform.forward = forward;
        public static void SetRight(ref this EcsTransform ecsTransform, Vector3 right) => ecsTransform.Transform.right = right;
        public static void SetUp(ref this EcsTransform ecsTransform, Vector3 up) => ecsTransform.Transform.up = up;
    }
}