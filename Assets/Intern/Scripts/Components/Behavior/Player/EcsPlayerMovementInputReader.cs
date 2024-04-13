using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsPlayerMovementInputReader : ComponentConverter<PlayerMovementInputReaderComponent>
    {
    }

    [Serializable]
    public struct PlayerMovementInputReaderComponent
    {
        // ReSharper disable once InconsistentNaming
        [FormerlySerializedAs("self")] [Header("DATA")] [SerializeField]
        private PlayerMovementInputReaderBase _self;

        public Vector2 Input =>
#if UNITY_EDITOR
            _input =
#endif
                _self.ReadInput();

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private Vector2 _input;
#endif
    }
}