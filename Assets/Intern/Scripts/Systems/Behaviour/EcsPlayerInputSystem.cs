using Client.Components;
using Leopotam.EcsLite;

namespace Client.Systems
{
    public class EcsPlayerInputSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var playerInputFilter = world
                .Filter<EcsPlayerInput>()
                .Inc<EcsMoveInput>()
                .Inc<EcsLookInput>()
                .Inc<EcsActionInput>()
                .End();

            
            var playerInputs = world.GetPool<EcsPlayerInput>();
            var moveInputs = world.GetPool<EcsMoveInput>();
            var lookInputs = world.GetPool<EcsLookInput>();
            var actionInputs = world.GetPool<EcsActionInput>();

            foreach (var entity in playerInputFilter)
            {
                ref var playerInput = ref playerInputs.Get(entity);
                ref var moveInput = ref moveInputs.Get(entity);
                ref var lookInput = ref lookInputs.Get(entity);
                ref var actionInput = ref actionInputs.Get(entity);

                moveInput.MoveDestination = playerInput.GetMoveDestination();
                lookInput.LookDestination = playerInput.GetLookDestination();
                actionInput.MainActionPerformed = playerInput.IsMainActionPerformed();
                actionInput.SecondaryActionPerformed = playerInput.IsSecondaryActionPerformed();
            }
        }
    }
}