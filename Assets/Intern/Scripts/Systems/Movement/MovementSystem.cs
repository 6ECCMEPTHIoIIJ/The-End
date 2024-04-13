using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public partial class MovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world
                .Filter<EcsTransform>()
                .Inc<EcsMovement>()
                .Inc<EcsMovementInput>()
                .End();
            
            var transforms = world.GetPool<EcsTransform>();
            var movements = world.GetPool<EcsMovement>();
            var movementInputs = world.GetPool<EcsMovementInput>();

            foreach (var e in filter)
            {
                ref var transform = ref transforms.Get(e);
                ref var movement = ref movements.Get(e);
                ref var movementInput = ref movementInputs.Get(e); 
                movement.MoveTransformToDestination(ref transform, movementInput.MovementDestination);
            }
        }
    }
}