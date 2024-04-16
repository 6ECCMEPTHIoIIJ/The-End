using System;
using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Components
{
    public class EcsNavigableConverter : ComponentConverter<EcsNavigable>
    {
    }

    [Serializable]
    public struct EcsNavigable
    {
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
    }

    public static class EcsNavMeshAgentEx
    {
        public static float GetStoppingDistance(in this EcsNavigable ecsNavigable)
        {
            return ecsNavigable.NavMeshAgent.stoppingDistance;
        }
        
        public static Vector3 GetPosition(in this EcsNavigable ecsNavigable)
        {
            return ecsNavigable.NavMeshAgent.nextPosition;
        }
        
        public static void SetDestination(in this EcsNavigable ecsNavigable, Vector3 destination)
        {
            ecsNavigable.NavMeshAgent.SetDestination(destination);
        }
    }
}