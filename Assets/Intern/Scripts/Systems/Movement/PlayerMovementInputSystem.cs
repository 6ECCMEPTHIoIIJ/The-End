using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public partial class PlayerMovementInputSystem : IEcsRunSystem
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

    public partial class PlayerMovementInputSystem
    {
        public class Visualizer : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {
                var world = systems.GetWorld();
                var movablePlayersFilter = world
                    .Filter<EcsMovementInput>()
                    .Inc<EcsPlayerMovementInputReader>()
                    .End();
                var movementInputs = world.GetPool<EcsMovementInput>();

                foreach (var e in movablePlayersFilter)
                {
                    ref var movementInput = ref movementInputs.Get(e);
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(movementInput.MovementDestination, 0.3f);
                }
            }
        }
    }
}