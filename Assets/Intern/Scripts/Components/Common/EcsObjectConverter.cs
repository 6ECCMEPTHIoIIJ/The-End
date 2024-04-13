using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components.Common
{
    public class EcsObjectConverter : ComponentConverter<EcsObject>
    {
    }

    [Serializable]
    public struct EcsObject
    {
        [field: SerializeField] public GameObject GameObject { get; private set; }
    }

    public static class EcsObjectHelper
    {
        public static int GetLayer(in this EcsObject ecsObject) => ecsObject.GameObject.layer;
        public static LayerMask GetLayerMask(in this EcsObject ecsObject) => 1 << ecsObject.GameObject.layer;

        public static void SetLayer(ref this EcsObject ecsObject, int layer) =>
            ecsObject.GameObject.layer = layer;
    }
}