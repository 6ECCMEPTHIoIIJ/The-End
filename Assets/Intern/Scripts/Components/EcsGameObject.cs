using System;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Components
{
    public class EcsGameObject : BaseConverter
    {
        public struct Component
        {
            public GameObject Value { get; init; }
        }

        public override void Convert(EcsPackedEntityWithWorld entityWithWorld)
        {
            if (entityWithWorld.Unpack(out var world, out var entity))
            {
                ref var component = ref world.GetPool<Component>().Add(entity);
                component = new Component { Value = gameObject };
            }
        }
    }
}