using Client.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client.Systems
{
    public class GroundDetectSystem : IEcsRunSystem
    {
        private readonly Collider[] _groundColliders = new Collider[4];

        private readonly EcsPoolInject<EcsGameObject.Component> _gameObjectPoolInject = default;
        private readonly EcsPoolInject<GroundDetectedPointComponent> _groundDetectedPointPoolInject = default;
        private readonly EcsPoolInject<EcsGroundDetect.Component> _groundDetectPoolInject = default;

        private readonly EcsFilterInject<Inc<EcsGroundDetect.Component>> _groundDetectFilterInject = default;

        private EcsPool<EcsGameObject.Component> GameObjectPool => _gameObjectPoolInject.Value;
        private EcsPool<GroundDetectedPointComponent> GroundDetectedPointPool => _groundDetectedPointPoolInject.Value;
        private EcsPool<EcsGroundDetect.Component> GroundDetectPool => _groundDetectPoolInject.Value;
        private EcsFilter GroundDetectFilter => _groundDetectFilterInject.Value;

        public void Run(IEcsSystems systems)
        {
            foreach (var e in GroundDetectFilter)
            {
                ref var groundDetect = ref GroundDetectPool.Get(e);
                ref var gameObject = ref GameObjectPool.Get(e);
                var groundCollidersCount =
                    Physics.OverlapSphereNonAlloc(groundDetect.Origin.position, groundDetect.Radius, _groundColliders,
                        groundDetect.GroundLayer & ~(1 << gameObject.Value.layer));
                for (var i = 0; i < groundCollidersCount; i++)
                {
                    var groundCollider = _groundColliders[i];
                    var closestPoint = groundCollider.ClosestPoint(groundDetect.Origin.position);
                    var checkRay = new Ray(groundDetect.Origin.position, closestPoint - groundDetect.Origin.position);
                    if (groundCollider.Raycast(checkRay, out var hit, groundDetect.Radius)
                        && Vector3.Angle(hit.normal, Vector3.up) <= groundDetect.MaxSlopeAngle)
                    {
                        ref var groundDetectedPoint = ref GroundDetectedPointPool.Has(e)
                            ? ref GroundDetectedPointPool.Get(e)
                            : ref GroundDetectedPointPool.Add(e);
                        groundDetectedPoint = new GroundDetectedPointComponent
                        {
                            Point = hit.point,
                            Normal = hit.normal
                        };
                        return;
                    }
                }

                if (GroundDetectedPointPool.Has(e))
                {
                    GroundDetectedPointPool.Del(e);
                }
            }
        }

        public class Visualizer : IEcsRunSystem
        {
            private readonly EcsPoolInject<GroundDetectedPointComponent> _groundDetectedPointPoolInject = default;
            private readonly EcsPoolInject<EcsGroundDetect.Component> _groundDetectPoolInject = default;
            private readonly EcsFilterInject<Inc<EcsGroundDetect.Component>> _groundDetectFilterInject = default;

            private EcsPool<GroundDetectedPointComponent> GroundDetectedPointPool =>
                _groundDetectedPointPoolInject.Value;

            private EcsPool<EcsGroundDetect.Component> GroundDetectPool => _groundDetectPoolInject.Value;
            private EcsFilter GroundDetectFilter => _groundDetectFilterInject.Value;

            public void Run(IEcsSystems systems)
            {
                foreach (var e in GroundDetectFilter)
                {
                    ref var groundDetect = ref GroundDetectPool.Get(e);
                    var groundDetected = GroundDetectedPointPool.Has(e);
                    if (groundDetected)
                    {
                        ref var groundDetectedPoint = ref GroundDetectedPointPool.Get(e);
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawRay(groundDetectedPoint.Point, groundDetectedPoint.Normal);
                    }

                    Gizmos.color = groundDetected ? Color.cyan : Color.gray;
                    Gizmos.DrawWireSphere(groundDetect.Origin.position, groundDetect.Radius);
                }
            }
        }
    }
}