using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsMovement : ComponentConverter<MovementComponent>
    {
    }

    [Serializable]
    public struct MovementComponent
    {
        [field: Header("DATA")]
        [field: SerializeField]
        public float MaxSpeed { get; set; }

        [field: SerializeField] public float Acceleration { get; set; }
        [field: SerializeField] public float Deceleration { get; set; }

        [field: Header("REFERENCES")]
        [field: SerializeField]
        [field: ReadOnly]
        public Vector3 CurrentDirection { get; set; }

        public Vector3 CurrentVelocity =>
#if UNITY_EDITOR
            _currentVelocity =
#endif
                CurrentDirection * MaxSpeed;

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        private Vector3 _currentVelocity;
#endif
    }
}