using UnityEngine;

namespace CannonChallenge.Events
{
    /// <summary>
    /// Serializable event that sends a game object as parameter
    /// </summary>
    [CreateAssetMenu(menuName = "CannonChallenge/Events/Game Object Event Asset")]
    public class GameObjectEventAsset: GameEventAsset<GameObject>
    {
    }
}