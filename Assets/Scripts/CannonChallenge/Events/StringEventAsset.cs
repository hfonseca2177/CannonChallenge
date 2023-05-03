using UnityEngine;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Serializable event that sends a string value
    /// </summary>
    [CreateAssetMenu(menuName = "CannonChallenge/Events/String Event Asset")]
    public class StringEventAsset : GameEventAsset<string>
    {
    }
}