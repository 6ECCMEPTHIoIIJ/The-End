using System;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace Client.Components
{
    public class EcsTransform : ComponentConverter<TransformComponent>
    {
    }


    [Serializable]
    public struct TransformComponent
    {
        [field: Header("DATA")]
        [field: SerializeField]
        public Transform Self { private get; set; }

        public Vector3 Position
        {
            get =>
#if UNITY_EDITOR
                _position =
#endif
                    Self.position;
            set => Self.position = value;
        }

        public Quaternion Rotation
        {
            get =>
#if UNITY_EDITOR
                _rotation =
#endif
                    Self.rotation;
            set => Self.rotation = value;
        }

        public Vector3 Forward
        {
            get =>
#if UNITY_EDITOR
                _forward =
#endif
                    Self.forward;
            set =>
                Self.forward = value;
        }

        public Vector3 Right
        {
            get =>
#if UNITY_EDITOR
                _right =
#endif
                    Self.right;
            set => Self.right = value;
        }

        public Vector3 Up
        {
            get =>
#if UNITY_EDITOR
                _up =
#endif
                    Self.up;
            set => Self.up = value;
        }

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        private Vector3 _position;

        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        [SerializeField] [ReadOnly] private Quaternion _rotation;

        [Header("---")]

        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        [SerializeField]
        [ReadOnly]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private Vector3 _forward;

        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        [SerializeField] [ReadOnly] private Vector3 _right;

        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        [SerializeField] [ReadOnly] private Vector3 _up;
#endif
    }
}