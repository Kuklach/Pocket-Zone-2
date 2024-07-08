using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : CharacterController
    {

        private bool _isMoveInputPerforming;
        private InputAction _move;
        private PlayerInputActions _playerInputActions;

        internal new void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _playerInputActions = new PlayerInputActions();
        }

        internal void OnEnable()
        {
            _move = _playerInputActions.Player.Move;
            _move.Enable();
        }

        internal void OnDisable()
        {
            _move.Disable();
        }

        protected override void DefineInput()
        {
            MoveInput = _move.ReadValue<Vector2>();
        }
    }
}