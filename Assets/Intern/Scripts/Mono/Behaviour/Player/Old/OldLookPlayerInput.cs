using System;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Mono
{
    public sealed class OldLookPlayerInput : LookPlayerInput
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private NavMeshAgent navMeshAgent;

        public override void OnUpdate(float deltaTime)
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                LookDestination = hit.point;
            }
            else if (new Plane(Vector3.up, navMeshAgent.nextPosition) is var plane
                     && plane.Raycast(ray, out var distance))
            {
                LookDestination = ray.GetPoint(distance);
            }
            else
            {
                throw new Exception("Camera is parallel to the ground.");
            }
        }
    }
}