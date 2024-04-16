using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Mono
{
    [DisallowMultipleComponent]
    public sealed class PlayerInput : MonoBehaviour
    {
        [FormerlySerializedAs("_lookInput")] [SerializeField]
        private LookPlayerInput lookInput;

        [FormerlySerializedAs("_moveInput")] [SerializeField]
        private MovePlayerInput moveInput;

        [FormerlySerializedAs("_actionInput")] [SerializeField]
        private ActionPlayerInput actionInput;

        public Vector3 LookDestination => lookInput.LookDestination;
        public Vector3 MoveDestination => moveInput.MoveDestination;
        public bool MainActionPerformed => actionInput.MainActionPerformed;
        public bool SecondaryActionPerformed => actionInput.SecondaryActionPerformed;
    }
}