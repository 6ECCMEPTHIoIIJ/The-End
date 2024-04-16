using UnityEngine;

namespace Client.Mono
{
    public sealed class OldActionPlayerInput : ActionPlayerInput
    {
        public override void OnUpdate(float deltaTime)
        {
            MainActionPerformed = Input.GetButtonDown("Fire1");
            SecondaryActionPerformed = Input.GetButtonDown("Fire2");
        }
    }
}