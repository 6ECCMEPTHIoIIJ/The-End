using UnityEngine;

namespace Client.Mono
{
    public class OldMoveByCursorPlayerInput : MovePlayerInput
    {
        [SerializeField] private Camera mainCamera;
        
        public override void OnUpdate(float deltaTime)
        {
            if (!Input.GetButton("Fire1"))
            {
                return;
            }
            
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                MoveDestination = hit.point;
            }
           
        }
    }
}