using AB_Utility.FromSceneToEntityConverter;
using Client.Systems;
using JetBrains.Annotations;
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

        private void Start()
        {
            _world = new EcsWorld();
            _updateSystems = _world.AddSystems();
            _fixedUpdateSystems = _world.AddSystems();
        }

        private void Update() => UpdateSystems(_updateSystems);

        private void FixedUpdate() => UpdateSystems(_fixedUpdateSystems);

        private void OnDestroy()
        {
            DestroySystems(ref _updateSystems);
            DestroySystems(ref _fixedUpdateSystems);
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