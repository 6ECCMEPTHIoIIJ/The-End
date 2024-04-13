using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public class PlayerMovementInputSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var playerReadableInputFilter = world
                .Filter<EcsTransform>()
                .Inc<EcsMovementInput>()
                .Inc<EcsPlayerMovementInputReader>()
                .End();
            var mainCameraFilter = world
                .Filter<EcsTransform>()
                .Inc<EcsMainCameraTag>()
                .End();

            var transforms = world.GetPool<EcsTransform>();
            var movementInputs = world.GetPool<EcsMovementInput>();
            var inputReaders = world.GetPool<EcsPlayerMovementInputReader>();

            foreach (var i in playerReadableInputFilter)
            {
                ref var transform = ref transforms.Get(i);
                ref var playerMovementInputReader = ref inputReaders.Get(i);
                ref var movementInput = ref movementInputs.Get(i);
                foreach (var c in mainCameraFilter)
                {
                    ref var mainCameraTransform = ref transforms.Get(c);
                    movementInput.MovementDestination = transform.GetPosition() +
                                                        playerMovementInputReader.GetInputRotatedToCamera(
                                                            mainCameraTransform.GetRotation());
                }
            }
        }
    }
}