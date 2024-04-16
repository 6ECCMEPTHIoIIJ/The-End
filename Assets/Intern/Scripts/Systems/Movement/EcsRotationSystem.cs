using Client.Components;
using Leopotam.EcsLite;

namespace Client.Systems
{
    public class EcsRotationSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var bodyRotationFilter = world
                .Filter<EcsRotatable>()
                .Inc<EcsLookInput>()
                .End();

            var rotates = world.GetPool<EcsRotatable>();
            var lookInputs = world.GetPool<EcsLookInput>();

            foreach (var entity in bodyRotationFilter)
            {
                ref var rotatable = ref rotates.Get(entity);
                ref var lookInput = ref lookInputs.Get(entity);

                rotatable.SetLookDestination(lookInput.LookDestination);
            }
        }
    }
}