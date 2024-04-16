using UnityEngine;

namespace Client.Mono
{
    [DisallowMultipleComponent]
    public abstract class LookPlayerInput : UpdateSlave
    {
        public Vector3 LookDestination { get; protected set; }
    }
}