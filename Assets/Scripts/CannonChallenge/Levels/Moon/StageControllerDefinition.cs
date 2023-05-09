using UnityEngine;

namespace CannonChallenge.Levels.Moon
{
    /// <summary>
    /// Stage Controller data definition
    /// </summary>
    [CreateAssetMenu(fileName = "StageControllerDefinition", menuName = "CannonChallenge/Level/Stage Controller Definition")]
    public class StageControllerDefinition : ScriptableObject
    {
        [Header("Options")]
        public int NumberOfStages;
        public int Duration;
        public float ExplosiveChance;
        [Header("Random generate amount of targets available in the platform")]
        public int MinNumBarrels;
        public int MaxNumBarrels;
    }
}