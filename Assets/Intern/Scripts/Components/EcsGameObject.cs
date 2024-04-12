using System;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Components
{
    [RequireComponent(typeof(GameObject))]
    public class EcsGameObject : BaseConverter
    {
        public override void Convert(EcsPackedEntityWithWorld entityWithWorld)
        {
            if (entityWithWorld.Unpack(out var world, out var entity))
            {
                ref var component = ref world.GetPool<GameObjectComponent>().Add(entity);
                component = new GameObjectComponent { Self = gameObject };
            }
        }
    }

    [Serializable]
    public struct GameObjectComponent : IMonoComponent<GameObject>
    {
        [field: Header("REFERENCES")]
        [field: SerializeField]
        [field: ReadOnly]
        public GameObject Self { get; set; }

        public LayerMask Layer
        {
            get =>
#if UNITY_EDITOR
                _layer =
#endif
                    1 << Self.layer;
            set => Self.layer = (value >> 1) & 1;
        }

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        private LayerMask _layer;
#endif
    }
}