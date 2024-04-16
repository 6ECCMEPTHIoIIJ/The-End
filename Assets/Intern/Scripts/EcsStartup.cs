using System.Diagnostics.CodeAnalysis;
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
                .Add(new EcsPlayerInputSystem())
                .Inject()
                .Init();

            _fixedUpdateSystems
                .Add(new EcsNavigationSystem())
                .Add(new EcsRotationSystem())
                .Inject()
                .Init();

            _drawGizmosSystems
#if UNITY_EDITOR
                .AddSystemsDebugInfo()
#endif
                .Inject()
                .Init();
        }

        private void Update() => UpdateSystems(_updateSystems);

        private void FixedUpdate() => UpdateSystems(_fixedUpdateSystems);

        private void OnDrawGizmos() => UpdateSystems(_drawGizmosSystems);

        private void OnDestroy()
        {
            DestroySystems(ref _drawGizmosSystems);
            DestroySystems(ref _fixedUpdateSystems);
            DestroySystems(ref _updateSystems);
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

    public static class EcsSystemEx
    {
        public static IEcsSystems AddSystemsDebugInfo([NotNull] this IEcsSystems systems)
        {
#if UNITY_EDITOR
            systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif
            return systems;
        }
    }
}