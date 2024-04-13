using UnityEngine;

[DisallowMultipleComponent]
public abstract class PlayerMovementInputReaderBase : MonoBehaviour
{
    public abstract Vector2 ReadInput();
}