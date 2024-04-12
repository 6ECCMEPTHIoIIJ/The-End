using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Components
{
    [DisallowMultipleComponent]
    public class MonoConverter<TComponent, T> : BaseConverter where TComponent : struct, IMonoComponent<T>
    {
        public override void Convert(EcsPackedEntityWithWorld entityWithWorld)
        {
            if (entityWithWorld.Unpack(out var world, out var entity))
            {
                ref var component = ref world.GetPool<TComponent>().Add(entity);
                component = new TComponent { Self = GetComponent<T>() };
            }
        }
    }
    
    public interface IMonoComponent<in T>
    {
        T Self { set; }
    }
}