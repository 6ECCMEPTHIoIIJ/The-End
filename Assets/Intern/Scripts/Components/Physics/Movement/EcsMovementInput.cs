using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsMovementInput : ComponentConverter<MovementInputComponent>
    {
    }

    [Serializable]
    public struct MovementInputComponent
    {
        [field: Header("REFERENCES")]
        [field: SerializeField]
        [field: ReadOnly]
        public Vector3 Input { get; set; }
    }
}