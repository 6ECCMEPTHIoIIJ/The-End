using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsPlayerMovementInputReader : ComponentConverter<EcsPlayerMovementInputReader.Component>
    {
        public Vector2 Input { get; private set; }

        public void OnInput(InputAction.CallbackContext context)
        {
            Input = context.ReadValue<Vector2>().normalized;
        }
        
        [Serializable]
        public struct Component
        {
            [FormerlySerializedAs("value")] [SerializeField]
            private EcsPlayerMovementInputReader _value;

            public EcsPlayerMovementInputReader Value => _value;
        }
    }
}