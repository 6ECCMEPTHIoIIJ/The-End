using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Components
{
    public class NewPlayerMovementInputReader : PlayerMovementInputReaderBase
    {
        private const float InputSmoothingTime = 0.15f;
        private const float InputTolerance = 0.05f;

        // ReSharper disable once InconsistentNaming
        [SerializeField] private InputActionAsset _inputActions;

        private InputAction _action;
        private Vector2 _input;
        private Vector2 _smoothedInput;

        public override Vector2 ReadInput() => _input;

        private void Awake()
        {
            var playerInput = gameObject.AddComponent<PlayerInput>();
            playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            playerInput.actions = _inputActions;
            _action = playerInput.actions["Move"];
        }

        private void Update()
        {
            var rawInput = _action.ReadValue<Vector2>();
            _smoothedInput = Vector2.MoveTowards(_smoothedInput, rawInput, 1f / InputSmoothingTime * Time.deltaTime);
            _input = _smoothedInput.magnitude < InputTolerance ? Vector2.zero : _smoothedInput;
        }

        private void OnEnable() => _action.Enable();

        private void OnDisable() => _action.Disable();
    }
}