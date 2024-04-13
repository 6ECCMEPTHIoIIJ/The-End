using System;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Components
{
    public class EcsGameObject : ComponentConverter<GameObjectComponent>
    {
    }

    [Serializable]
    public struct GameObjectComponent
    {
        [field: Header("DATA")]
        [field: SerializeField]
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