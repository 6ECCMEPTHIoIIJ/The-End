using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsGroundDetect : ComponentConverter<GroundDetectComponent>
    {
    }

    [Serializable]
    public struct GroundDetectComponent
    {
        [field: Header("DATA")]
        [field: SerializeField] public Transform Origin { private get; set; }
        [field: SerializeField] public float Radius { get; set; }
        [field: SerializeField] public LayerMask GroundLayer { get; set; }

        public Vector3 OriginPosition
        {
            get
            {
                return
#if UNITY_EDITOR
                    _originPosition =
#endif
                        Origin.position;
            }
        }

#if UNITY_EDITOR
        [Header("DEBUG")] [SerializeField] [ReadOnly]
        // ReSharper disable once NotAccessedField.Local
        // ReSharper disable once InconsistentNaming
        private Vector3 _originPosition;
#endif
    }

    [Serializable]
    public struct GroundDetectedPointComponent
    {
        [field: Header("REFERENCES")]
        [field: SerializeField]
        [field: ReadOnly]
        public Vector3 Point { get; set; }

        [field: SerializeField]
        [field: ReadOnly]
        public Vector3 Normal { get; set; }
    }
}