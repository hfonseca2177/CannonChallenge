using UnityEngine;

namespace CannonChallenge.Systems.Score
{
    /// <summary>
    /// Reference for the Level
    /// </summary>
    [CreateAssetMenu(fileName = "LevelReference", menuName = "CannonChallenge/Score/Level Reference")]
    public class LevelReference : ScriptableObject
    {
        public int LevelIndex;
        public string LevelName;
    }
}
