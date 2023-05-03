using UnityEngine;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Serializable event that sends a int value
    /// </summary>
    [CreateAssetMenu(menuName = "CannonChallenge/Events/Int Event Asset")]
    public class IntEventAsset: GameEventAsset<int>
    {
    }
}