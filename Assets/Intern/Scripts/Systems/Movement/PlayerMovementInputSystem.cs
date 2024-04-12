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
            var movablePlayersFilter = world
                .Filter<TransformComponent>()
                .Inc<PlayerMovementInputReaderComponent>()
                .Inc<MovementInputComponent>()
                .Inc<PlayerTag>()
                .End();
            var mainCameraFilter = world.Filter<TransformComponent>().Inc<MainCameraTag>().End();
            var movementInputs = world.GetPool<MovementInputComponent>();
            var inputReaders = world.GetPool<PlayerMovementInputReaderComponent>();
            var transforms = world.GetPool<TransformComponent>();

            foreach (var e in movablePlayersFilter)
            {
                ref var playerMovementInputReader = ref inputReaders.Get(e);
                ref var playerTransform = ref transforms.Get(e);
                ref var movementInput = ref movementInputs.Get(e);
                foreach (var c in mainCameraFilter)
                {
                    ref var mainCameraTransform = ref transforms.Get(c);
                    var cameraRotation = mainCameraTransform.Rotation;
                    var rawInput = playerMovementInputReader.Input;
                    var playerUp = playerTransform.Up;
                    movementInput.Input =
                        Vector3.ProjectOnPlane(cameraRotation * new Vector3(rawInput.x, 0, rawInput.y), playerUp);
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
                    .Filter<TransformComponent>()
                    .Inc<MovementInputComponent>()
                    .Inc<PlayerTag>()
                    .End();
                var movementInputs = world.GetPool<MovementInputComponent>();
                var transforms = world.GetPool<TransformComponent>();

                foreach (var e in movablePlayersFilter)
                {
                    ref var playerTransform = ref transforms.Get(e);
                    ref var movementInput = ref movementInputs.Get(e);
                    Debug.DrawRay(playerTransform.Position, movementInput.Input, Color.red);
                }
            }
        }
    }
}