using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Mono.Movement
{
    public class RotatableAgent : FixedUpdateSlave
    {
        [FormerlySerializedAs("FreezeXRotation")] [SerializeField]
        private bool freezeXRotation;

        [FormerlySerializedAs("FreezeYRotation")] [SerializeField]
        private bool freezeYRotation;

        [FormerlySerializedAs("FreezeZRotation")] [SerializeField]
        private bool freezeZRotation;

        [FormerlySerializedAs("AngularSpeed")] [SerializeField]
        private float angularSpeed = 180f;

        private Vector3 _lookDestination;

        public override void OnFixedUpdate(float deltaTime)
        {
            var targetRotation = Quaternion.LookRotation(_lookDestination - transform.position);
            var originAngles = transform.rotation.eulerAngles;

            var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * deltaTime);
            var freezeAngles = Quaternion.Euler(
                freezeXRotation ? originAngles.x : rotation.eulerAngles.x,
                freezeYRotation ? originAngles.y : rotation.eulerAngles.y,
                freezeZRotation ? originAngles.z : rotation.eulerAngles.z
            );
            transform.rotation = freezeAngles;
        }

        public void SetLookDestination(Vector3 lookDestination)
        {
            _lookDestination = lookDestination;
        }
    }
}