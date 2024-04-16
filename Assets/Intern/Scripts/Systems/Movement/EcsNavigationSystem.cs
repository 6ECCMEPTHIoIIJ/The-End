using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public class EcsNavigationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var navigationFilter = world
                .Filter<EcsNavigable>()
                .Inc<EcsMoveInput>()
                .End();

            var navMeshAgents = world.GetPool<EcsNavigable>();
            var moveInputs = world.GetPool<EcsMoveInput>();

            foreach (var entity in navigationFilter)
            {
                ref var navMeshAgent = ref navMeshAgents.Get(entity);
                ref var moveInput = ref moveInputs.Get(entity);

                if (Vector3.Distance(moveInput.MoveDestination, navMeshAgent.GetPosition())
                    > navMeshAgent.GetStoppingDistance())
                {
                    navMeshAgent.SetDestination(moveInput.MoveDestination);
                }
            }
        }
    }
}