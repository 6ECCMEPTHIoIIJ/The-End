using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsMovementInputConverter : ComponentConverter<EcsMovementInput>
    {
    }

    public struct EcsMovementInput
    {
        public Vector3 MovementDestination { get; set; }
    }
}