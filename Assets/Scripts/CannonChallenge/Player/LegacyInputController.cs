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
            //when left mouse is triggered
            if (Input.GetKeyUp(KeyCode.A))
            {
                OnMoveLeft();
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                OnMoveRight();
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                OnMoveUp();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                OnMoveDown();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                OnFire();
            }
        }

    }
}