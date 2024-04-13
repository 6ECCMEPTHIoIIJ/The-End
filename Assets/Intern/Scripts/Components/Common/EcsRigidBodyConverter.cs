using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components.Common
{
    public class EcsRigidBodyConverter : ComponentConverter<EcsRigidBody>
    {
    }

    [Serializable]
    public struct EcsRigidBody
    {
        [field: SerializeField] public Rigidbody RigidBody { get; private set; }
    }
    
    public static class EcsRigidBodyHelper
    {
        public static Vector3 GetVelocity(in this EcsRigidBody ecsRigidBody) => ecsRigidBody.RigidBody.velocity;
        
        public static void SetVelocity(ref this EcsRigidBody ecsRigidBody, Vector3 velocity) => ecsRigidBody.RigidBody.velocity = velocity;
    }
}