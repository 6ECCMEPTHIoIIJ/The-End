﻿using UnityEngine;

namespace Client.Components
{
    public class OldPlayerMovementInputReader : PlayerMovementInputReaderBase
    {
        public override Vector2 ReadInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
    }
}