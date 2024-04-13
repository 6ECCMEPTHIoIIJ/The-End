using System;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Components
{
    public class EcsRigidBody : ComponentConverter<RigidBodyComponent>
    {
    }

    [Serializable]
    public struct RigidBodyComponent
    {
        [field: Header("DATA")]
        [field: SerializeField]
        public Rigidbody Self { private get; set; }

        public Vector3 Velocity
        {
            get =>
#if UNITY_EDITOR
                _velocity =
#endif
                    Self.velocity;
            set => Self.velocity = value;
        }

        public void Lock()
        {
            Self.freezeRotation = true;
            Self.useGravity = false;
            Velocity = Vector3.zero;
        }

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        private Vector3 _velocity;
#endif
    }
}