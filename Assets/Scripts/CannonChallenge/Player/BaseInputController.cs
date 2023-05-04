using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Player
{
    /// <summary>
    /// Base Input controller - abstract code between input controller implementations
    /// </summary>
    public abstract class BaseInputController : MonoBehaviour, IPlayerInput
    {
        [Header("Events")]
        [SerializeField] protected MoveEventAsset _onMoveNotify;
        [SerializeField] protected VoidEventAsset _onFireNotify;

        public virtual void OnMove()
        {
            _onMoveNotify.Invoke(Vector2.zero);
        }
        public virtual void OnFire()
        {
            _onFireNotify.Invoke();
        }
    }
}