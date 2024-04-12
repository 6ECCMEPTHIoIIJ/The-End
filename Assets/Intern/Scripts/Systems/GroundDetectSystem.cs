using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public partial class GroundDetectSystem : IEcsRunSystem
    {
        private readonly Collider[] _groundColliders = new Collider[4];

        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var groundDetectFilter = world.Filter<GameObjectComponent>().Inc<GroundDetectComponent>().End();
            var groundDetects = world.GetPool<GroundDetectComponent>();
            var owners = world.GetPool<GameObjectComponent>();
            var groundDetectedPoints = world.GetPool<GroundDetectedPointComponent>();

            foreach (var e in groundDetectFilter)
            {
                ref var groundDetect = ref groundDetects.Get(e);
                ref var gameObject = ref owners.Get(e);
                var groundCollidersCount =
                    Physics.OverlapSphereNonAlloc(groundDetect.OriginPosition, groundDetect.Radius, _groundColliders,
                        groundDetect.GroundLayer & ~gameObject.Layer);
                for (var i = 0; i < groundCollidersCount; i++)
                {
                    var groundCollider = _groundColliders[i];
                    var closestPoint = groundCollider.ClosestPoint(groundDetect.OriginPosition);
                    var checkRay = new Ray(groundDetect.OriginPosition, closestPoint - groundDetect.OriginPosition);
                    if (groundCollider.Raycast(checkRay, out var hit, groundDetect.Radius))
                    {
                        ref var groundDetectedPoint = ref groundDetectedPoints.Has(e)
                            ? ref groundDetectedPoints.Get(e)
                            : ref groundDetectedPoints.Add(e);
                        groundDetectedPoint.Point = hit.point;
                        groundDetectedPoint.Normal = hit.normal;
                        return;
                    }
                }

                if (groundDetectedPoints.Has(e))
                {
                    groundDetectedPoints.Del(e);
                }
            }
        }
    }

    public partial class GroundDetectSystem
    {
        public class Visualizer : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {
                var world = systems.GetWorld();
                var groundDetectFilter = world.Filter<GroundDetectComponent>().End();
                var groundDetects = world.GetPool<GroundDetectComponent>();
                var groundDetectedPoints = world.GetPool<GroundDetectedPointComponent>();

                foreach (var e in groundDetectFilter)
                {
                    ref var groundDetect = ref groundDetects.Get(e);
                    var groundDetected = groundDetectedPoints.Has(e);
                    if (groundDetected)
                    {
                        ref var groundDetectedPoint = ref groundDetectedPoints.Get(e);
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawRay(groundDetectedPoint.Point, groundDetectedPoint.Normal);
                    }

                    Gizmos.color = groundDetected ? Color.cyan : Color.gray;
                    Gizmos.DrawWireSphere(groundDetect.OriginPosition, groundDetect.Radius);
                }
            }
        }
    }
}