using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Components
{
    public class EcsMovementConverter : ComponentConverter<EcsMovement>
    {
        
    }
    
    [Serializable]
    public struct EcsMovement
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    }
    
    public static class EcsMovementHelper
    {
        public static void SetDestination(in this EcsMovement ecsMovement, Vector3 destination) =>
            ecsMovement.NavMeshAgent.SetDestination(destination);
    }
}