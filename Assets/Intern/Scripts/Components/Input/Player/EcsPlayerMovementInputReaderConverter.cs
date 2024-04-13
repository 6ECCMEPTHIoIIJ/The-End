using System;
using AB_Utility.FromSceneToEntityConverter;
using Client.Mono;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsPlayerMovementInputReaderConverter : ComponentConverter<EcsPlayerMovementInputReader>
    {
    }

    [Serializable]
    public struct EcsPlayerMovementInputReader
    {
        [field: SerializeField] public PlayerMovementInputReaderBase InputReader { get; private set; }
    }

    public static class EcsPlayerMovementInputReaderHelper
    {
        public static Vector2 GetRawInput(in this EcsPlayerMovementInputReader ecsPlayerMovementInputReader) =>
            ecsPlayerMovementInputReader.InputReader.ReadInput();

        public static Vector3 GetInputRotatedToCamera(in this EcsPlayerMovementInputReader ecsPlayerMovementInputReader,
            Quaternion cameraRotation)
        {
            var rawInput = ecsPlayerMovementInputReader.GetRawInput();
            var input = new Vector3(rawInput.x, 0, rawInput.y);
            var rotatedInput = Quaternion.Euler(0, cameraRotation.eulerAngles.y, 0) * input;
            return rotatedInput;
        }
    }
}