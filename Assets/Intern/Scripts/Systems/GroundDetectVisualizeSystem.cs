using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class GroundDetectVisualizeSystem : IEcsRunSystem
    {
        private readonly EcsPoolInject<EcsGroundDetect.Component> _groundDetectPoolInject = default;
        private readonly EcsFilterInject<Inc<EcsGroundDetect.Component>> _groundDetectFilterInject = default;
        private EcsPool<EcsGroundDetect.Component> GroundDetectPool => _groundDetectPoolInject.Value;
        private EcsFilter GroundDetectFilter => _groundDetectFilterInject.Value;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var e in GroundDetectFilter)
            {
                ref var groundDetect= ref GroundDetectPool.Get(e);
                Gizmos.color = Color.gray;
                Gizmos.DrawWireSphere(groundDetect.Origin.position, groundDetect.Radius);
            }
        }
    }
}