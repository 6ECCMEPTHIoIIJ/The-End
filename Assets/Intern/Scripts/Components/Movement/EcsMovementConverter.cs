using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Components
{
    public class EcsMovementConverter : ComponentConverter<EcsMovement>
    {
    }

    [Serializable]
    public struct EcsMovement
    {
        public float Speed { get; set; }

        [field: SerializeField] public float MaxSpeed { get; private set; }

        [field: SerializeField] public float Acceleration { get; private set; }
    }

    public static class EcsMovementHelper
    {
        private static int NavMeshWalkableArea => 1 << NavMesh.GetAreaFromName("Walkable");
        private const float MaxSampleDistance = 1f;
        private const float MinMoveDistance = 0.01f;
        private const float MinSqrMoveDistance = MinMoveDistance * MinMoveDistance;

        public static float GetPositionIncrement(in this EcsMovement ecsMovement) =>
            ecsMovement.Speed * Time.fixedDeltaTime;

        public static float GetSpeedIncrement(in this EcsMovement ecsMovement) =>
            ecsMovement.Acceleration * Time.fixedDeltaTime;

        public static void Accelerate(ref this EcsMovement ecsMovement) =>
            ecsMovement.Speed = ecsMovement.Speed < ecsMovement.MaxSpeed
                ? Mathf.MoveTowards(ecsMovement.Speed, ecsMovement.MaxSpeed,
                    ecsMovement.GetSpeedIncrement())
                : ecsMovement.MaxSpeed;

        public static void MoveTransformToDestination(ref this EcsMovement ecsMovement, ref EcsTransform ecsTransform,
            Vector3 destination)
        {
            if (NavMesh.SamplePosition(destination, out var hit, MaxSampleDistance, NavMeshWalkableArea)
                && (hit.position - ecsTransform.GetPosition()).sqrMagnitude > MinSqrMoveDistance)
            {
                ecsMovement.Accelerate();
                ecsTransform.SetPosition(
                    Vector3.MoveTowards(ecsTransform.GetPosition(), hit.position, ecsMovement.GetPositionIncrement()));
            }
            else
            {
                ecsMovement.Speed = 0;
            }
        }
    }
}