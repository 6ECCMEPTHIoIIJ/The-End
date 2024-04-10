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

            _updateSystems.ConvertScene().AddSystemsDebugInfo()
                .Inject()
                .Init();
            _fixedUpdateSystems
                .Inject()
                .Init();
            _drawGizmosSystems
                .Add(new GroundDetectVisualizeSystem())
                .Inject()
                .Init();
        }

        private void Update() => UpdateSystems(_updateSystems);

        private void FixedUpdate() => UpdateSystems(_fixedUpdateSystems);
        
        private void OnDrawGizmos() => UpdateSystems(_drawGizmosSystems);

        private void OnDestroy()
        {
            DestroySystems(ref _updateSystems);
            DestroySystems(ref _fixedUpdateSystems);
            DestroySystems(ref _drawGizmosSystems);
            DestroyWorld(ref _world);
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