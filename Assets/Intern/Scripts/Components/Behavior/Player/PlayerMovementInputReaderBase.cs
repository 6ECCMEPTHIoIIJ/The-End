using UnityEngine;

namespace Client.Components
{
    [DisallowMultipleComponent]
    public abstract class PlayerMovementInputReaderBase : MonoBehaviour
    {
        public abstract Vector2 ReadInput();
    }
}