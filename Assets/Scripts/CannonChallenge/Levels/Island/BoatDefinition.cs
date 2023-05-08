using UnityEngine;

namespace CannonChallenge.Levels.Island
{
    /// <summary>
    /// Boat data definition
    /// </summary>
    [CreateAssetMenu(fileName = "BoatDefinition", menuName = "CannonChallenge/Level/Boat Definition")]
    public class BoatDefinition : ScriptableObject
    {
        [Header("Random generate amount of targets available in the boat")]
        public int MinNumBarrels;
        public int MaxNumBarrels;
        [Header("Random generate speed from this range")]
        public float MinSpeed;
        public float MaxSpeed;
        
    }
}