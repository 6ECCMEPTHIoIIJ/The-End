using UnityEngine;

namespace Client.Mono
{
    [DisallowMultipleComponent]
    public abstract class MovePlayerInput : UpdateSlave
    {
        public Vector3 MoveDestination { get; protected set; }
    }
}