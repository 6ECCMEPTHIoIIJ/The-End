using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Client.Mono
{
    public sealed class OldMoveByArrowsPlayerInput : MovePlayerInput
    {
        private const float InputReactivityTime = 0.2f;
        private const float MaxMoveStep = 0.9f;

        [SerializeField] private Camera mainCamera;

        [FormerlySerializedAs("playerTransform")] [SerializeField]
        private NavMeshAgent player;

        private Vector3 _curDirection = Vector3.zero;

        public override void OnUpdate(float deltaTime)
        {
            var cameraYRotation = mainCamera.transform.rotation.eulerAngles.y;
            var nextDirection = Quaternion.AngleAxis(cameraYRotation, Vector3.up)
                                * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            var reactivitySpeed = deltaTime / InputReactivityTime;
            _curDirection = Vector3.RotateTowards(_curDirection, nextDirection, reactivitySpeed,
                reactivitySpeed);
            MoveDestination = player.nextPosition + _curDirection * MaxMoveStep;
        }
    }
}