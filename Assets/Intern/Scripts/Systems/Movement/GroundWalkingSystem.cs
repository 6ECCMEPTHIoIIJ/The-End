using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public partial class GroundWalkingSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world
                .Filter<MovementComponent>()
                .Inc<MovementInputComponent>()
                .Inc<GroundWalkingTag>()
                .End();
            var movements = world.GetPool<MovementComponent>();
            var movementInputs = world.GetPool<MovementInputComponent>();
            var groundDetectedPoints = world.GetPool<GroundDetectedPointComponent>();

            foreach (var e in filter)
            {
                ref var movement = ref movements.Get(e);
                ref var movementInput = ref movementInputs.Get(e);
                movement.CurrentDirection = groundDetectedPoints.Has(e)
                    ? Vector3.ProjectOnPlane(movementInput.Input, groundDetectedPoints.Get(e).Normal).normalized
                    : Vector3.zero;
            }
        }
    }

    public partial class GroundWalkingSystem
    {
        public class Visualizer : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {
                var world = systems.GetWorld();
                var filter = world
                    .Filter<TransformComponent>()
                    .Inc<MovementComponent>()
                    .Inc<GroundWalkingTag>()
                    .End();
                var transforms = world.GetPool<TransformComponent>();
                var movements = world.GetPool<MovementComponent>();

                foreach (var e in filter)
                {
                    ref var transform = ref transforms.Get(e);
                    ref var movement = ref movements.Get(e);
                    Debug.DrawRay(transform.Position, movement.CurrentDirection, Color.yellow);
                }
            }
        }
    }
}