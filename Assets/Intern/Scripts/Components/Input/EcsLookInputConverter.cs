using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    public class EcsLookInputConverter : ComponentConverter<EcsLookInput>
    {
    }
    
    public struct EcsLookInput
    {
        public Vector3 LookDestination { get; set; }
    }
}