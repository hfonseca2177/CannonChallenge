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
        [SerializeField] protected VoidEventAsset _onMoveLeftNotify;
        [SerializeField] protected VoidEventAsset _onMoveRightNotify;
        [SerializeField] protected VoidEventAsset _onMoveUpNotify;
        [SerializeField] protected VoidEventAsset _onMoveDownNotify;
        [SerializeField] protected VoidEventAsset _onFireNotify;
        
        public virtual void OnMoveLeft()
        {
            _onMoveLeftNotify.Invoke();
        }

        public virtual void OnMoveRight()
        {
            _onMoveRightNotify.Invoke();
        }

        public virtual void OnMoveUp()
        {
            _onMoveUpNotify.Invoke();
        }

        public virtual void OnMoveDown()
        {
            _onMoveDownNotify.Invoke();
        }

        public virtual void OnFire()
        {
            _onFireNotify.Invoke();
        }
    }
}