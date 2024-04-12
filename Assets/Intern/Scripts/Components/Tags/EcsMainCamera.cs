using AB_Utility.FromSceneToEntityConverter;
using UnityEngine;

namespace Client.Components
{
    [RequireComponent(typeof(Camera))]
    public class EcsMainCamera : ComponentConverter<MainCameraTag>
    {
    }
    
    public struct MainCameraTag
    {
    }
}