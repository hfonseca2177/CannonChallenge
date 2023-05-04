using UnityEngine;
using UnityEngine.InputSystem;

namespace CannonChallenge.Player
{
    /// <summary>
    /// New Input system implementation for player input controller
    /// Mapped by Input Name
    /// </summary>
    public class InputController : BaseInputController
    {
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        
        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _moveAction = _playerInput.actions["Move"];
        }

        public override void OnMove()
        {
            ReadInput();
        }

        /*private void Update()
        {
            if (_moveAction.WasPressedThisFrame())
            {
                ReadInput();
            }
        }*/

        private void ReadInput()
        {
            Vector2 input = _moveAction.ReadValue<Vector2>();
            _onMoveNotify.Invoke(input);
        }
    }

}