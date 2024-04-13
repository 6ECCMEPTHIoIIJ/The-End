using AB_Utility.FromSceneToEntityConverter;
using Client.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _drawGizmosSystems;

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _drawGizmosSystems = new EcsSystems(_world);

            _updateSystems.ConvertScene()
#if UNITY_EDITOR
                .AddSystemsDebugInfo()
#endif
                .Add(new PlayerMovementInputSystem())
                .Inject()
                .Init();
            
            _fixedUpdateSystems
                .Add(new GroundDetectSystem())
                .Add(new GroundWalkingSystem())
                .Add(new RigidBodyMovementSystem())
                .Inject()
                .Init();
            
            _drawGizmosSystems
                .Add(new GroundDetectSystem.Visualizer())
                .Add(new PlayerMovementInputSystem.Visualizer())
                .Add(new GroundWalkingSystem.Visualizer())
                .Add(new RigidBodyMovementSystem.Visualizer())
                .Inject()
                .Init();
        }

        private void Update() => UpdateSystems(_updateSystems);

        private void FixedUpdate() => UpdateSystems(_fixedUpdateSystems);

        private void OnDrawGizmos() => UpdateSystems(_drawGizmosSystems);

        private void OnDestroy()
        {
            DestroyWorld(ref _world);
            DestroySystems(ref _updateSystems);
            DestroySystems(ref _fixedUpdateSystems);
            DestroySystems(ref _drawGizmosSystems);
        }

        private static void UpdateSystems(IEcsSystems systems) => systems?.Run();

        private static void DestroyWorld(ref EcsWorld world)
        {
            world?.Destroy();
            world = null;
        }

        private static void DestroySystems(ref IEcsSystems systems)
        {
            systems?.Destroy();
            systems = null;
        }
    }
}