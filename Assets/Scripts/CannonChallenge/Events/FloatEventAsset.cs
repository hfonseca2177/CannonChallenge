using UnityEngine;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Serializable event that sends a float value
    /// </summary>
    [CreateAssetMenu(menuName = "CannonChallenge/Events/Float Event Asset")]
    public class FloatEventAsset: GameEventAsset<float>
    {
    }
}