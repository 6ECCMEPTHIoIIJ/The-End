using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsMovementInputConverter : ComponentConverter<EcsMoveInput>
    {
    }

    public struct EcsMoveInput
    {
        public Vector3 MoveDestination { get; set; }
    }
}