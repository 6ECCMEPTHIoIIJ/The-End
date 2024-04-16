using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using PlayerInput = Client.Mono.PlayerInput;

namespace Client.Components
{
    public class EcsPlayerInputConverter : ComponentConverter<EcsPlayerInput>
    {
    }

    [Serializable]
    public struct EcsPlayerInput
    {
        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }
    }

    public static class EcsPlayerInputEx
    {
        public static Vector3 GetMoveDestination(in this EcsPlayerInput playerInput) =>
            playerInput.PlayerInput.MoveDestination;

        public static Vector3 GetLookDestination(in this EcsPlayerInput playerInput) =>
            playerInput.PlayerInput.LookDestination;

        public static bool IsMainActionPerformed(in this EcsPlayerInput playerInput) =>
            playerInput.PlayerInput.MainActionPerformed;
        
        public static bool IsSecondaryActionPerformed(in this EcsPlayerInput playerInput) =>
            playerInput.PlayerInput.SecondaryActionPerformed;
    }
}