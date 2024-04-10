using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace Client.Systems
{
    public static class EcsSystemEx
    {
        public static IEcsSystems AddSystemsDebugInfo([NotNull] this IEcsSystems systems)
        {
#if UNITY_EDITOR
            systems.Add(new Mitfart.LeoECSLite.UnityIntegration.EcsWorldDebugSystem());
#endif
            return systems;
        }
    }
}