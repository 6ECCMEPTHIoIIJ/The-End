using System;
using UnityEngine;

namespace Client.Components
{
    [RequireComponent(typeof(PlayerMovementInputReaderBase))]
    public class EcsPlayerMovementInputReader :
        MonoConverter<PlayerMovementInputReaderComponent, PlayerMovementInputReaderBase>
    {
    }

    [Serializable]
    public struct PlayerMovementInputReaderComponent : IMonoComponent<PlayerMovementInputReaderBase>
    {
        [field: Header("REFERENCES")]
        [field: SerializeField]
        [field: ReadOnly]
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