using Client.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Systems
{
    public class RigidBodyMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var rigidBodyFilter = world.Filter<RigidBodyComponent>().End();
            var rigidBodies = world.GetPool<RigidBodyComponent>();
            
            foreach (var e in rigidBodyFilter)
            {
                ref var rigidBody = ref rigidBodies.Get(e);
                rigidBody.Lock();
            }
        }
        
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var movableFilter = world
                .Filter<RigidBodyComponent>()
                .Inc<MovementComponent>()
                .End();

            var movements = world.GetPool<MovementComponent>();
            var rigidBodies = world.GetPool<RigidBodyComponent>();
            foreach (var e in movableFilter)
            {
                ref var movement = ref movements.Get(e);
                ref var rigidBody = ref rigidBodies.Get(e);
                var acceleration = rigidBody.Velocity.magnitude > movement.CurrentVelocity.magnitude
                                   || Vector3.Angle(rigidBody.Velocity, movement.CurrentDirection) > 90f
                    ? movement.Deceleration
                    : movement.Acceleration;
                rigidBody.Velocity = Vector3.MoveTowards(rigidBody.Velocity, movement.CurrentVelocity,
                    acceleration * Time.fixedDeltaTime);
            }
        }

        public class Visualizer : IEcsRunSystem
        {
            public void Run(IEcsSystems systems)
            {
                var world = systems.GetWorld();
                var movableFilter = world
                    .Filter<TransformComponent>()
                    .Inc<RigidBodyComponent>()
                    .End();
                var rigidBodies = world.GetPool<RigidBodyComponent>();
                var transforms = world.GetPool<TransformComponent>();
                foreach (var e in movableFilter)
                {
                    ref var rigidBody = ref rigidBodies.Get(e);
                    ref var transform = ref transforms.Get(e);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawRay(transform.Position, rigidBody.Velocity);
                }
            }
        }
    }
}