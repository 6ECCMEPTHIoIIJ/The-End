using UnityEngine;

namespace Client.Mono
{
    public abstract class UpdateSlave : MonoBehaviour
    {
        public abstract void OnUpdate(float deltaTime);
    }
    
    public abstract class FixedUpdateSlave : MonoBehaviour
    {
        public abstract void OnFixedUpdate(float deltaTime);
    }
}