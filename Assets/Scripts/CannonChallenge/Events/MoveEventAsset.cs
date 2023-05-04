using UnityEngine;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Serializable event that sends a Move Vector2 value
    /// </summary>
    [CreateAssetMenu(menuName = "CannonChallenge/Events/Move Event Asset")]
    public class MoveEventAsset : GameEventAsset<Vector2>
    {        
    }
}