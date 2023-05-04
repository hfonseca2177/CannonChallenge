using UnityEngine;

namespace CannonChallenge.Player
{
    /// <summary>
    /// Old Input system implementation for player input controller
    /// </summary>
    public class LegacyInputController : BaseInputController
    {
        private void Update()
        {
            OnMove();
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnFire();
            }
        }

        public override void OnMove()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            
            _onMoveNotify.Invoke(new Vector2(horizontalInput, verticalInput));
        }
    }
}