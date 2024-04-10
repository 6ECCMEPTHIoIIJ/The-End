using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsMovementInput : ComponentConverter<EcsMovementInput.Component>
    {
        public struct Component
        {
            public Vector2 Input { get; set; }  
        }
    }
}