using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var filter = world
                .Filter<EcsMovement>()
                .Inc<EcsMovementInput>()
                .End();
            
            var movements = world.GetPool<EcsMovement>();
            var movementInputs = world.GetPool<EcsMovementInput>();

            foreach (var e in filter)
            {
                ref var movement = ref movements.Get(e);
                ref var movementInput = ref movementInputs.Get(e);
                movement.SetDestination(movementInput.MovementDestination);
            }
        }
    }
}