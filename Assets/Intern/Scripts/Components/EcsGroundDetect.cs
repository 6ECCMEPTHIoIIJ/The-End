using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.Serialization;

namespace Client.Components
{
    public class EcsGroundDetect : ComponentConverter<EcsGroundDetect.Component>
    {
        [Serializable]
        public struct Component
        {
            [FormerlySerializedAs("origin")] [SerializeField]
            private Transform _origin;

            [FormerlySerializedAs("radius")] [SerializeField]
            private float _radius;

            [FormerlySerializedAs("groundLayer")] [SerializeField]
            private LayerMask _groundLayer;

            [SerializeField] private float _maxSlopeAngle;


            public Transform Origin => _origin;
            public float Radius => _radius;
            public LayerMask GroundLayer => _groundLayer;
            public float MaxSlopeAngle => _maxSlopeAngle;

            public void Deconstruct(out Transform origin, out float radius, out LayerMask groundLayer,
                out float maxSlopeAngle)
            {
                origin = _origin;
                radius = _radius;
                groundLayer = _groundLayer;
                maxSlopeAngle = _maxSlopeAngle;
            }

            public struct DetectedPoint
            {
                public Vector3 Point { get; set; }
                public Vector3 Normal { get; set; }
            }
        }
    }
}