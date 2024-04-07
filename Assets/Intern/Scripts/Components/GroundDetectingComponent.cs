using UnityEngine;

namespace Client.Components
{
    public struct GroundDetectingComponent
    {
        public float MaxSlopeAngle { get; set; }
        public bool IsTouchingGround { get; set; }
        public Vector3 GroundNormal { get; set; }
    }
}