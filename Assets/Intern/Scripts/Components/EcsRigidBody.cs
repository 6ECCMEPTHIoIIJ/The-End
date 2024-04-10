using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsRigidBody : ComponentConverter<EcsRigidBody.Component>
    {
        [Serializable]
        public struct Component
        {
            [FormerlySerializedAs("value")] [SerializeField] private Rigidbody _value;

            public Rigidbody Value => _value;
        }
    }
}