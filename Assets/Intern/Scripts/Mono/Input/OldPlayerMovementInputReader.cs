using UnityEngine;

namespace Client.Mono
{
    public class OldPlayerMovementInputReader : PlayerMovementInputReaderBase
    {
        public override Vector2 ReadInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
    }
}