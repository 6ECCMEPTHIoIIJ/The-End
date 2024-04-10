using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsTransform : ComponentConverter<EcsTransform.Component>
    {
        [Serializable]
        public struct Component
        {
            [FormerlySerializedAs("value")] [SerializeField] private Transform _value;

            public Transform Value => _value;
        }
    }
}