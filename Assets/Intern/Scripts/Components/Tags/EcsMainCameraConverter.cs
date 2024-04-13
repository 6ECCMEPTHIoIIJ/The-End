using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    [RequireComponent(typeof(Camera))]
    public class EcsMainCameraConverter : ComponentConverter<EcsMainCameraTag>
    {
    }
    
    public struct EcsMainCameraTag
    {
    }
}