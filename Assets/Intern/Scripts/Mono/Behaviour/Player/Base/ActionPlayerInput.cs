using UnityEngine;

namespace Client.Mono
{
    [DisallowMultipleComponent]
    public abstract class ActionPlayerInput : UpdateSlave
    {
        public bool MainActionPerformed { get; protected set; }
        public bool SecondaryActionPerformed { get; protected set; }
    }
}