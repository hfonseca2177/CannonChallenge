using UnityEngine;
using UnityEngine.Events;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Base Serializable Game Event
    /// </summary>
    public abstract class GameEventAsset<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private bool _isDebugEnabled;
#endif
        public UnityEvent<T> OnInvoked;

        public void Invoke(T param)
        {
#if UNITY_EDITOR
            if(_isDebugEnabled) Debug.Log($" Object:{name} event invoked: {param}", this);
#endif
            OnInvoked.Invoke(param);
        }
    }
}
