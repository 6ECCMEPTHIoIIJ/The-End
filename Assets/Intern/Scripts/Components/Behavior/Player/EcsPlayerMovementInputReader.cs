using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsPlayerMovementInputReader : ComponentConverter<PlayerMovementInputReaderComponent>
    {
    }

    [Serializable]
    public struct PlayerMovementInputReaderComponent
    {
        [field: Header("DATA")]
        [field: SerializeField]
        public PlayerMovementInputReaderBase Self { private get; set; }

        public Vector2 Input =>
#if UNITY_EDITOR
            _input =
#endif
                Self.ReadInput();

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private Vector2 _input;
#endif
    }
}