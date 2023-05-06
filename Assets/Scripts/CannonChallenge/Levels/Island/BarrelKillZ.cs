using CannonChallenge.Events;
using UnityEngine;

namespace CannonChallenge.Levels.Island
{
    /// <summary>
    /// Trigger barrel destroy
    /// </summary>
    public class BarrelKillZ : MonoBehaviour
    {
        [Tooltip("when barrel is destroyed")] 
        [SerializeField] private GameObjectEventAsset _onBarrelReleaseNotify;
    
        private void OnTriggerEnter(Collider other)
        {
            _onBarrelReleaseNotify.Invoke(other.gameObject);
        }
    }
}