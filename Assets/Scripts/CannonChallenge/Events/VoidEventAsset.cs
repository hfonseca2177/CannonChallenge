using UnityEngine;
using UnityEngine.Events;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Serializable event with no parameters
    /// </summary>
    [CreateAssetMenu(menuName = "CannonChallenge/Events/Void Event Asset")]
    public class VoidEventAsset : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private bool _isDebugEnabled;
#endif
        public UnityEvent OnInvoked;

        public void Invoke()
        {
#if UNITY_EDITOR
            if(_isDebugEnabled) Debug.Log($" Object:{name} event invoked", this);
#endif
            OnInvoked.Invoke();
        }
    }
}