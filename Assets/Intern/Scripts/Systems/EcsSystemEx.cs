using AB_Utility.FromSceneToEntityConverter;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client.Systems
{
    public static class EcsSystemEx
    {
        public static IEcsSystems AddSystems([NotNull] this EcsWorld world, params IEcsSystem[] systems)
        {
            var systemGroup = new EcsSystems(world);
            systemGroup.ConvertScene();
            foreach (var system in systems)
            {
                systemGroup.Add(system);
            }
        
            systemGroup.AddSystemsDebugInfo().Inject().Init();
            
            return systemGroup;
        }
        
        private static IEcsSystems AddSystemsDebugInfo([NotNull] this IEcsSystems systems)
        {
#if UNITY_EDITOR
            systems.Add(new Mitfart.LeoECSLite.UnityIntegration.EcsWorldDebugSystem());
#endif
            return systems;
        }
    }
}