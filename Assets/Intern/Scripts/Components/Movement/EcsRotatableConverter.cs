using System;
using AB_Utility.FromSceneToEntityConverter;
using Client.Mono.Movement;
using UnityEngine;

namespace Client.Components
{
    public class EcsRotatableConverter : ComponentConverter<EcsRotatable>
    {
    }

    [Serializable]
    public struct EcsRotatable
    {
        [field: SerializeField] public RotatableAgent RotatableAgent { get; private set; }
    }

    public static class EcsRotatableEx
    {
        public static void SetLookDestination(in this EcsRotatable ecsRotatable, Vector3 lookDestination)
        {
            ecsRotatable.RotatableAgent.SetLookDestination(lookDestination);
        }
    }
}